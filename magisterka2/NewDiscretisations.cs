using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static magisterka2.Dyskretyzacja;

namespace magisterka2
{
    public class NewDiscretisations
    {
        static string path = @"C:\magisterka\Bayes\NewTest\Etap3\";
        public static string DyscrtyzacjaReducedEqual<T>(DbSet<T> dbSet, int bins = 5, List<string> names = null, string name ="", int maxColumns = 220, string lastColumnName = "rank") where T : SecondaryBase
        {
            Console.WriteLine("Aktualny czas: " + DateTime.Now.ToString("HH:mm:ss"));
            var type = typeof(T);
            string filepath = path + @"\" + $"{type.Name}_{bins}{name}_evenBins_reduced";
            Console.WriteLine("Typ elementu w DbSet: " + type.FullName);
            Console.WriteLine($"Working for {maxColumns}");
            var properties = type.GetProperties()
                .Where(p => p.PropertyType != typeof(string) && p.PropertyType != typeof(bool))
                .ToList();

            var allItems = dbSet.Where(x => x.rank != Rank.NONE).ToList();
            List<BinObject2> Discretisation = new List<BinObject2>();
            int count = allItems.Count;

            foreach (var prop in properties)
            {
                if (IsNumericType(prop.PropertyType))
                {
                    if (prop.Name == "eligibleForProgression") continue;

                    var values = allItems
                        .Select(item => Convert.ToSingle(prop.GetValue(item)))
                        .OrderBy(x => x)
                        .ToList();

                    if (values.Count < bins)
                    {
                        Console.WriteLine($"Za mało danych do dyskretyzacji kolumny {prop.Name}");
                        continue;
                    }

                    List<float> boundaries = new List<float>();
                    for (int j = 1; j < bins; j++)
                    {
                        int index = (int)(j * values.Count / (float)bins);
                        boundaries.Add(values[index]);
                    }

                    // Dodaj min i max na końcach
                    boundaries.Insert(0, values.First());
                    boundaries.Add(values.Last());

                    for (int j = 1; j <= bins; j++)
                    {
                        if (boundaries[j - 1] >= boundaries[j])
                        {
                            boundaries[j] = boundaries[j - 1] + 0.01f;
                        }
                    }
                    Discretisation.Add(new BinObject2(prop.Name, boundaries));
                }
            }

            var writer = new StreamWriter(filepath+".csv");

            // --- WYBÓR I KOLEJNOŚĆ KOLUMN ---
            var allFields = type.GetProperties().Where(p => p.PropertyType != typeof(string)).ToList();
            var lastField = allFields.FirstOrDefault(p => p.Name.Equals(lastColumnName, StringComparison.OrdinalIgnoreCase));
            var limitedFields = new List<PropertyInfo>();
            allFields.RemoveAll(x => x.Name.Equals("eligibleForProgression", StringComparison.OrdinalIgnoreCase));
            foreach (var p in allFields)
            {
                if (p.Name.Equals(lastColumnName, StringComparison.OrdinalIgnoreCase)) continue;
                if (names != null && !names.Contains(p.Name)) continue;
                if (limitedFields.Count >= maxColumns) break;
                limitedFields.Add(p);
            }

            if (lastField != null)
                limitedFields.Add(lastField);

            writer.WriteLine(string.Join(",", limitedFields.Select(p => p.Name)));

            int i = 1;
            foreach (var element in dbSet)
            {
                if (element.rank == Rank.NONE) continue;
                Console.Write($"\r Dyskretyzacja {type.Name}: {i++}/{count}");
                List<string> line = new List<string>();

                foreach (var item in limitedFields)
                {
                    var value = item.GetValue(element);
                    var discret = Discretisation.FirstOrDefault(d => d.Name == item.Name);

                    if (discret != null)
                        line.Add(discret.CreateBin(Convert.ToSingle(value)));
                    else
                    {
                        if (value is Rank)
                            line.Add(RankExtensions.GetDisplayNameReduced((Rank)value));
                        else
                        {
                            line.Add(value?.ToString() ?? "");
                        }
                    }
                }

                writer.WriteLine(string.Join(",", line));
            }

            writer.Close();
            return filepath;
        }

        public static string DyscrtyzacjaNumberedEqual<T>(DbSet<T> dbSet, int bins = 5, List<string> names = null, string name = "", int maxColumns = 220, string lastColumnName = "rank") where T : SecondaryBase
        {
            Console.WriteLine("Aktualny czas: " + DateTime.Now.ToString("HH:mm:ss"));
            var type = typeof(T);
            string filepath = path + @"\" + $"{type.Name}_{bins}{name}_evenBins_numbered";
            Console.WriteLine("Typ elementu w DbSet: " + type.FullName);
            Console.WriteLine($"Working for {maxColumns}");
            var properties = type.GetProperties()
                .Where(p => p.PropertyType != typeof(string) && p.PropertyType != typeof(bool))
                .ToList();

            var allItems = dbSet.Where(x => x.rank != Rank.NONE).ToList();
            List<BinObject2> Discretisation = new List<BinObject2>();
            int count = allItems.Count;

            foreach (var prop in properties)
            {
                if (IsNumericType(prop.PropertyType))
                {
                    if (prop.Name == "eligibleForProgression") continue;
                    var values = allItems
                        .Select(item => Convert.ToSingle(prop.GetValue(item)))
                        .OrderBy(x => x)
                        .ToList();

                    if (values.Count < bins)
                    {
                        Console.WriteLine($"Za mało danych do dyskretyzacji kolumny {prop.Name}");
                        continue;
                    }

                    List<float> boundaries = new List<float>();
                    for (int j = 1; j < bins; j++)
                    {
                        int index = (int)(j * values.Count / (float)bins);
                        boundaries.Add(values[index]);
                    }

                    // Dodaj min i max na końcach
                    boundaries.Insert(0, values.First());
                    boundaries.Add(values.Last());
                    Discretisation.Add(new BinObject2(prop.Name, boundaries));
                }
            }

            var writer = new StreamWriter(filepath + ".csv");

            // --- WYBÓR I KOLEJNOŚĆ KOLUMN ---
            var allFields = type.GetProperties().Where(p => p.PropertyType != typeof(string)).ToList();
            var lastField = allFields.FirstOrDefault(p => p.Name.Equals(lastColumnName, StringComparison.OrdinalIgnoreCase));
            var limitedFields = new List<PropertyInfo>();

            allFields.RemoveAll(x => x.Name.Equals("eligibleForProgression", StringComparison.OrdinalIgnoreCase));
            foreach (var p in allFields)
            {
                if (p.Name.Equals(lastColumnName, StringComparison.OrdinalIgnoreCase)) continue;
                if (names != null && !names.Contains(p.Name)) continue;
                if (limitedFields.Count >= maxColumns) break;
                limitedFields.Add(p);
            }

            if (lastField != null)
                limitedFields.Add(lastField);

            writer.WriteLine(string.Join(",", limitedFields.Select(p => p.Name)));

            int i = 1;
            foreach (var element in dbSet)
            {
                if (element.rank == Rank.NONE) continue;
                Console.Write($"\r Dyskretyzacja {type.Name}: {i++}/{count}");
                List<string> line = new List<string>();

                foreach (var item in limitedFields)
                {
                    var value = item.GetValue(element);
                    var discret = Discretisation.FirstOrDefault(d => d.Name == item.Name);

                    if (discret != null)
                        line.Add(discret.CreateBin(Convert.ToSingle(value)));
                    else
                    {
                        if (value is Rank)
                            line.Add(RankExtensions.GetDisplayName((Rank)value));
                        else
                        {
                            line.Add(value?.ToString() ?? "");
                        }
                    }
                }

                writer.WriteLine(string.Join(",", line));
            }

            writer.Close();
            return filepath;
        }


        public static string DyscrtyzacjaReduced<T>(DbSet<T> dbSet, int bins = 5, List<string> names = null, string name = "", int maxColumns = 220, string lastColumnName = "rank") where T : SecondaryBase
        {
            Console.WriteLine("Aktualny czas: " + DateTime.Now.ToString("HH:mm:ss"));
            var type = typeof(T);
            string filepath = path + @"\" + $"{type.Name}_{bins}{name}_reduced";
            Console.WriteLine("Typ elementu w DbSet: " + type.FullName);
            Console.WriteLine($"Working for {maxColumns}");
            var properties = type.GetProperties()
                .Where(p => p.PropertyType != typeof(string) && p.PropertyType != typeof(bool))
                .ToList();

            var allItems = dbSet.ToList();
            List<BinObject> Discretisation = new List<BinObject>();
            int count = allItems.Count;

            foreach (var prop in properties)
            {
                if (IsNumericType(prop.PropertyType))
                {
                    double min = allItems.FindAll(x => x.rank != Rank.NONE).Min(item => Convert.ToDouble(prop.GetValue(item)));
                    double max = allItems.FindAll(x => x.rank != Rank.NONE).Max(item => Convert.ToDouble(prop.GetValue(item)));
                    double binSize = (max - min) / bins;
                    Discretisation.Add(new BinObject(prop.Name, (float)min, (float)max, (float)binSize));
                }
            }

            var writer = new StreamWriter(filepath+ ".csv");

            // --- WYBÓR I KOLEJNOŚĆ KOLUMN ---
            var allFields = type.GetProperties().Where(p => p.PropertyType != typeof(string)).ToList();

            // Znajdź ostatnią kolumnę po nazwie
            var lastField = allFields.FirstOrDefault(p => p.Name.Equals(lastColumnName, StringComparison.OrdinalIgnoreCase));
            if (lastField == null)
            {
                Console.WriteLine($"Nie znaleziono kolumny '{lastColumnName}' w {type.Name}. Zostanie pominięta.");
            }

            // Wybieramy kolumny do limitu, ale bez lastField
            var limitedFields = new List<PropertyInfo>();
            foreach (var p in allFields)
            {
                if (p.Name.Equals(lastColumnName, StringComparison.OrdinalIgnoreCase)) continue;
                if (names != null && !names.Contains(p.Name)) continue;
                if (limitedFields.Count >= maxColumns) break;
                limitedFields.Add(p);
            }

            // Dodaj lastField na koniec (jeśli istnieje)
            if (lastField != null)
                limitedFields.Add(lastField);

            // Nagłówki
            var headers = string.Join(",", limitedFields.Select(p => p.Name));
            writer.WriteLine(headers);

            int i = 1;
            foreach (var element in allItems)
            {
                if (element.rank == Rank.NONE) continue;
                Console.Write($"\r Dyskretyzacja {type.Name}: {i++}/{count}");
                List<string> line = new List<string>();

                foreach (var item in limitedFields)
                {
                    var value = item.GetValue(element);
                    var discret = Discretisation.FirstOrDefault(d => d.Name == item.Name);

                    if (discret != null)
                        line.Add(discret.CreateBin(Convert.ToSingle(value)));
                    else
                    {
                        if (value is Rank)
                            line.Add(RankExtensions.GetDisplayNameReduced((Rank)value));
                        else
                        {
                            line.Add(value?.ToString() ?? "");
                        }
                    }

                }

                writer.WriteLine(string.Join(",", line));
            }

            writer.Close();

            return filepath;
        }

        public static string DyscrtyzacjaNumbered<T>(DbSet<T> dbSet, int bins = 5, List<string> names =null, string name = "", int maxColumns = 220, string lastColumnName = "rank") where T : SecondaryBase
        {
            Console.WriteLine("Aktualny czas: " + DateTime.Now.ToString("HH:mm:ss"));
            var type = typeof(T);
            string filepath = path + @"\" + $"{type.Name}_{bins}{name}_numbered";
            Console.WriteLine("Typ elementu w DbSet: " + type.FullName);
            Console.WriteLine($"Working for {maxColumns}");
            var properties = type.GetProperties()
                .Where(p => p.PropertyType != typeof(string) && p.PropertyType != typeof(bool))
                .ToList();

            var allItems = dbSet.ToList();
            List<BinObject> Discretisation = new List<BinObject>();
            int count = allItems.Count;

            foreach (var prop in properties)
            {
                if (IsNumericType(prop.PropertyType))
                {
                    double min = allItems.FindAll(x => x.rank != Rank.NONE).Min(item => Convert.ToDouble(prop.GetValue(item)));
                    double max = allItems.FindAll(x => x.rank != Rank.NONE).Max(item => Convert.ToDouble(prop.GetValue(item)));
                    double binSize = (max - min) / bins;
                    Discretisation.Add(new BinObject(prop.Name, (float)min, (float)max, (float)binSize));
                }
            }

            var writer = new StreamWriter(filepath + ".csv");

            // --- WYBÓR I KOLEJNOŚĆ KOLUMN ---
            var allFields = type.GetProperties().Where(p => p.PropertyType != typeof(string)).ToList();

            // Znajdź ostatnią kolumnę po nazwie
            var lastField = allFields.FirstOrDefault(p => p.Name.Equals(lastColumnName, StringComparison.OrdinalIgnoreCase));
            if (lastField == null)
            {
                Console.WriteLine($"Nie znaleziono kolumny '{lastColumnName}' w {type.Name}. Zostanie pominięta.");
            }

            // Wybieramy kolumny do limitu, ale bez lastField
            var limitedFields = new List<PropertyInfo>();
            foreach (var p in allFields)
            {
                if (p.Name.Equals(lastColumnName, StringComparison.OrdinalIgnoreCase)) continue;
                if (names != null && !names.Contains(p.Name)) continue;
                if (limitedFields.Count >= maxColumns) break;
                limitedFields.Add(p);
            }

            // Dodaj lastField na koniec (jeśli istnieje)
            if (lastField != null)
                limitedFields.Add(lastField);

            // Nagłówki
            var headers = string.Join(",", limitedFields.Select(p => p.Name));
            writer.WriteLine(headers);

            int i = 1;
            foreach (var element in allItems)
            {
                if (element.rank == Rank.NONE) continue;
                Console.Write($"\r Dyskretyzacja {type.Name}: {i++}/{count}");
                List<string> line = new List<string>();

                foreach (var item in limitedFields)
                {
                    var value = item.GetValue(element);
                    var discret = Discretisation.FirstOrDefault(d => d.Name == item.Name);

                    if (discret != null)
                        line.Add(discret.CreateBin(Convert.ToSingle(value)));
                    else
                    {
                        if (value is Rank)
                            line.Add(RankExtensions.GetDisplayName((Rank)value));
                        else
                        {
                            line.Add(value?.ToString() ?? "");
                        }
                    }

                }

                writer.WriteLine(string.Join(",", line));
            }

            writer.Close();

            return filepath;
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
    }
}
