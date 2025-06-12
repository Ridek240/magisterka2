using System;


using Smile;
using Smile.Learning;
using magisterka2old;
//using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace magisterka2
{
    public class Bayes
    {

        public static void Tutorial10(string name)
        {
            Console.WriteLine("Starting Tutorial10...");
            DataSet ds = new DataSet();
            try
            {
                ds.ReadFile($"{name}.csv");
            }
            catch (SmileException e)
            {
                Console.WriteLine($"Dataset load failed{e.Message}");
                return;
            }
            Console.WriteLine("Dataset has {0} variables (columns) and {1} records (rows)",
            ds.VariableCount, ds.RecordCount);
            
            /*
            for (int i = 0; i < ds.VariableCount; i++)
            {
                if(!ds.IsDiscrete(i))
                {
                    Console.WriteLine(ds.GetVariableId(i));
                    
                    ds.Discretize(i, DiscretizationAlgorithmType.UniformWidth, 2, ds.GetVariableId(i));
                }
            }
            
            BayesianSearch bayesSearch = new BayesianSearch();
            bayesSearch.IterationCount = 50;
            bayesSearch.RandSeed = 9876543;
            Network net1;
            try
            {
                net1 = bayesSearch.Learn(ds);
            }
            catch (SmileException e)
            {
                Console.WriteLine($"Bayesian Search failed{e.Message}");
                return;
            }
            Console.WriteLine("1st Bayesian Search finished, structure score: {0}",
            bayesSearch.LastScore);
            net1.WriteFile("tutorial10-bs1.xdsl");
            Network net2;
            bayesSearch.RandSeed = 3456789;
            try
            {
                net2 = bayesSearch.Learn(ds);
            }
            catch (SmileException e)
            {
                Console.WriteLine($"Bayesian Search failed{e.Message}");
                return;
            }
            Console.WriteLine("2nd Bayesian Search finished, structure score: {0}",
            bayesSearch.LastScore);
            net2.WriteFile("tutorial10-bs2.xdsl");
            /*int idxAge = ds.FindVariable("Age");
            int idxProfession = ds.FindVariable("Profession");
            int idxCreditWorthiness = ds.FindVariable("CreditWorthiness");
            if (idxAge < 0 || idxProfession < 0 || idxCreditWorthiness < 0)
            {
                Console.WriteLine("Can't find dataset variables for background knowledge");
                Console.WriteLine("The loaded file may not be Credit10k.csv");
                return;
            }
            BkKnowledge backgroundKnowledge = new BkKnowledge();
            backgroundKnowledge.MatchData(ds);
            backgroundKnowledge.AddForbiddenArc(idxAge, idxCreditWorthiness);
            backgroundKnowledge.AddForcedArc(idxAge, idxProfession);
            bayesSearch.BkKnowledge = backgroundKnowledge;
            Network net3;
            try
            {
                net3 = bayesSearch.Learn(ds);
            }
            catch (SmileException)
            {
                Console.WriteLine("Bayesian Search failed");
                return;
            }
            Console.WriteLine("3rd Bayesian Search finished, structure score: {0}",
            bayesSearch.LastScore);
            net3.WriteFile("tutorial10-bs3.xdsl");*/
            Network net4 = null;
            TAN tan = new TAN();
            tan.RandSeed = 777999;
            tan.ClassVariableId = "Rank";
            try
            {
                net4 = tan.Learn(ds);
            }
            catch (SmileException)
            {
                Console.WriteLine("TAN failed");
            }
            Console.WriteLine("Tree-augmented Naive Bayes finished");
            if(net4!=null)
                net4.WriteFile("tutorial10-tan.xdsl");
            PC pc = new PC();
            Pattern pattern;
            try
            {
                pattern = pc.Learn(ds);
            }
            catch (SmileException)
            {
                Console.WriteLine("PC failed");
                return;
            }
            Network net5 = pattern.MakeNetwork(ds);
            Console.WriteLine("PC finished, proceeding to parameter learning");
            net5.WriteFile("tutorial10-pc.xdsl");
            EM em = new EM();
            DataMatch[] matching;
            try
            {
                matching = ds.MatchNetwork(net5);
            }
            catch (SmileException)
            {
                Console.WriteLine("Can't automatically match network with dataset");
                return;
            }
            em.UniformizeParameters = false;
            em.RandomizeParameters = false;
            em.EqSampleSize = 0;
            try
            {
                em.Learn(ds, net5, matching);
            }
            catch (SmileException)
            {
                Console.WriteLine("EM failed");
                return;
            }
            Console.WriteLine("EM finished");
            net5.WriteFile("tutorial10-pc-em.xdsl");
            Console.WriteLine("Tutorial10 complete");
        }

        public static void ConvestToCSV<T>(DbSet<T> dbSet) where T: class
        {
            var type = typeof(T);
            Console.WriteLine("Typ elementu w DbSet: " + type.FullName);
            var properties = type.GetProperties().Where(p => p.PropertyType != typeof(string));
            var headers = string.Join(",", properties.Select(p => p.Name));
            var csvLines = new List<string>();
            csvLines.Add(headers);

            foreach (var item in dbSet)
            {
                
                //var values = properties.Select(p => p.GetValue(item)?.ToString()?.Replace(",", " ") ?? "");


                var values = properties.Select(p =>
                {
                    var value = p.GetValue(item);

                    if (value == null) return "";

                    // Obsługa bool
                    /*if (p.PropertyType == typeof(bool))
                        return (bool)value ? "1" : "0";

                    // Obsługa enum
                    if (p.PropertyType.IsEnum)
                        return ((int)value).ToString();
                    */
                    // Pomijanie stringów (one nie powinny się tu znaleźć, ale na wszelki wypadek)
                    if (p.PropertyType == typeof(string))
                        return null; // albo return null;

                    // Domyślnie: konwersja na string + zabezpieczenie przecinka
                    return value.ToString().Replace(",", "");
                });
                csvLines.Add(string.Join(",", values));
            }

            File.WriteAllLines($"{type.Name}_exported.csv", csvLines);
            csvLines.Clear();
            csvLines = null;
            GC.Collect();        // wymuszenie GC
            GC.WaitForPendingFinalizers();
        }


        public static void ConvestToCSV<T>(DbSet<T> dbSet, int bins = 5) where T : class
        {
            var type = typeof(T);
            Console.WriteLine("Typ elementu w DbSet: " + type.FullName);
            var properties = type.GetProperties().Where(p => p.PropertyType != typeof(string)).ToList();

            // Pobieramy wszystkie elementy do listy, żeby można było zebrać wartości do dyskretyzacji
            var allItems = dbSet.ToList();

            // Przygotowujemy mapę property -> dyskretyzowane wartości (dla typów numerycznych)
            var discretizedColumns = new Dictionary<string, int[]>();

            foreach (var prop in properties)
            {
                if (IsNumericType(prop.PropertyType))
                {
                    // Pobieramy wszystkie wartości danej kolumny (zabezpieczając przed null)
                    var values = allItems
                        .Select(item =>
                        {
                            var val = prop.GetValue(item);
                            if (val == null) return double.NaN;

                            // Konwersja na double (obsługa różnych typów numerycznych)
                            return Convert.ToDouble(val);
                        })
                        .Where(v => !double.IsNaN(v))
                        .ToArray();

                    if (values.Length > 0)
                    {
                        // Dyskretyzujemy wartości
                        var discreteValues = Discretize(values, bins);

                        // Musimy dopasować dyskretne wartości do wszystkich elementów (w tym null czy NaN dajemy -1)
                        var allDiscrete = allItems.Select(item =>
                        {
                            var val = prop.GetValue(item);
                            if (val == null) return -1;

                            double dval = Convert.ToDouble(val);
                            int idx = Array.IndexOf(values, dval);
                            return idx >= 0 ? discreteValues[idx] : -1;
                        }).ToArray();

                        discretizedColumns[prop.Name] = allDiscrete;
                    }
                    else
                    {
                        // Brak wartości, wpisujemy same -1
                        discretizedColumns[prop.Name] = Enumerable.Repeat(-1, allItems.Count).ToArray();
                    }
                }
            }

            // Tworzymy nagłówki CSV
            var headers = string.Join(",", properties.Select(p => p.Name));
            var csvLines = new List<string> { headers };

            for (int i = 0; i < allItems.Count; i++)
            {
                var lineValues = new List<string>();

                foreach (var prop in properties)
                {
                    if (IsNumericType(prop.PropertyType) && discretizedColumns.ContainsKey(prop.Name))
                    {
                        int discreteVal = discretizedColumns[prop.Name][i];
                        lineValues.Add(discreteVal >= 0 ? discreteVal.ToString() : ""); // -1 na puste pole
                    }
                    else
                    {
                        var val = prop.GetValue(allItems[i]);
                        lineValues.Add(val?.ToString().Replace(",", "") ?? "");
                    }
                }

                csvLines.Add(string.Join(",", lineValues));
            }

            File.WriteAllLines($"{type.Name}_exported.csv", csvLines);

            // Sprzątanie pamięci
            csvLines.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private static bool IsNumericType(Type type)
        {
            var numericTypes = new[]
            {
            typeof(byte), typeof(sbyte),
            typeof(short), typeof(ushort),
            typeof(int), typeof(uint),
            typeof(long), typeof(ulong),
            typeof(float), typeof(double), typeof(decimal)
        };
            return numericTypes.Contains(type) || (Nullable.GetUnderlyingType(type) != null && numericTypes.Contains(Nullable.GetUnderlyingType(type)));
        }

        public static int[] Discretize(double[] values, int bins)
        {
            double min = values.Min();
            double max = values.Max();
            double binSize = (max - min) / bins;

            int[] discreteValues = new int[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                int binIndex = (int)((values[i] - min) / binSize);
                if (binIndex == bins) binIndex--; // obsługa wartości równej max
                discreteValues[i] = binIndex;
            }

            return discreteValues;
        }
    }


}


