using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace magisterka2
{
    public class Dyskretyzacja
    {
        
        static string path = @"C:\magisterka\Bayes\NewTest\Etap3";
        public static void dyskretzacjaTest2<T>(DbSet<T> dbSet, int bins = 5) where T : Secondary_averave
        {
            var type = typeof(T);
            Console.WriteLine("Typ elementu w DbSet: " + type.FullName);
            var properties = type.GetProperties().Where(p => p.PropertyType != typeof(string) && p.PropertyType != typeof(bool)).ToList();

            // Pobieramy wszystkie elementy do listy, żeby można było zebrać wartości do dyskretyzacji
            var allItems = dbSet.ToList();
            List<BinObject> Discretisation = new List<BinObject>();
            int count = allItems.Count;
            foreach ( var prop in properties) 
            {
                if(IsNumericType(prop.PropertyType))
                {
                    double min = allItems.FindAll(x => x.rank != Rank.NONE).Min(item => Convert.ToDouble(prop.GetValue(item)));
                    double max = allItems.FindAll(x => x.rank != Rank.NONE).Max(item => Convert.ToDouble(prop.GetValue(item)));
                    double binSize = (max - min) / bins;
                    Discretisation.Add(new BinObject(prop.Name, (float)min, (float)max, (float)binSize));
                }
            }

            var writer = new StreamWriter(path +@"\"+ $"{type.Name}_discretisation2.csv");
            var fields = type.GetProperties().Where(p => p.PropertyType != typeof(string)).ToList();

            var headers = string.Join(",", fields.Select(p => p.Name));
            writer.WriteLine(headers);
            int i = 1;
            foreach (var element in allItems)
            {
                if(element.rank == Rank.NONE) continue;
                Console.Write($"\r Dyskretyzacja {type.Name}:{i++}/{count}");
                List<string> line = new List<string>();

                foreach( var item in fields) 
                {
                    var value = item.GetValue(element);

                    var discret = Discretisation.FirstOrDefault(i => i.Name == item.Name);

                    if(discret != null)
                    {
                        line.Add(discret.CreateBin(Convert.ToSingle(value)));
                    }
                    else
                    {
                        line.Add(value.ToString());
                    }
                }
                    
                writer.WriteLine(string.Join(",", line));
                int a = 0;
            }
        }

        public static string dyskretzacjaTest<T>(DbSet<T> dbSet, int bins = 5, int maxColumns = 220, string lastColumnName = "rank") where T : SecondaryBase
        {
            var type = typeof(T);
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

            var writer = new StreamWriter(path + @"\" + $"{type.Name}_discretisation_bins_{bins}_amount{maxColumns}_reduced.csv");

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
                        if(value is Rank) 
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
            
            return $"{type.Name}_discretisation_bins_{bins}_amount{maxColumns}_reduced";
        }

        public static string dyskretzacjaTestTestLast<T>(DbSet<T> dbSet, int bins = 5, string lastColumnName = "rank") where T : Secondary_averave
        {
            var type = typeof(T);
            Console.WriteLine("Typ elementu w DbSet: " + type.FullName);

            var properties = type.GetProperties()
                .Where(p => p.PropertyType != typeof(string) && p.PropertyType != typeof(bool))
                .ToList();

            var allItems = dbSet.ToList();
            Console.WriteLine("Typ elementu w DbSet: " + allItems.Count);
            //throw new NotImplementedException();    
            List<BinObject> Discretisation = new List<BinObject>();
            int count = allItems.Count;

            // --- WYZNACZ BINNING DLA PÓL NUMERYCZNYCH ---
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

            var writer = new StreamWriter(path + @"\" + $"{type.Name}_discretisation_bins_{bins}_chosenColumns.csv");

            // --- WYBÓR KONKRETNYCH POL ---
            string[] selectedFieldNames = new string[]
            {
"summonerLevel",
"laneMinionsFirst10Minutes",
"totalTimeSpentDead",
"gameLength",
"timePlayed",
"enemyVisionPings",
"visionWardsBoughtInGame",
"wardTakedowns",
"wardTakedownsBefore20M",
"controlWardsPlaced",
"wardsKilled",
"champion",
"goldPerMinute",
"earliestBaron",
"onMyWayPings",
"deathsByEnemyChamps",
"visionScorePerMinute",
"totalMinionsKilled",
"detectorWardsPlaced",
"controlWardTimeCoverageInRiverOrEnemyHalf",
"deaths",
"commandPings",
"stealthWardsPlaced",
"multiTurretRiftHeraldCount",
"magicDamageTaken",
"killsOnOtherLanesEarlyJungleAsLaner",
"takedownsInEnemyFountain",
"atakhan_first",
"atakhan",
"consumablesPurchased",
"quickCleanse",
"getTakedownsInAllLanesEarlyJungleAsLaner",
"spell1Casts",
"abilityUses",
"spell3Casts",
"horde",
"killParticipation",
"totalDamageDealt",
"firstBloodAssist",
"firstBloodKill",
"skillshotsDodged",
"enemyMissingPings",
"magicDamageDealt",
"moreEnemyJungleThanOpponent",
"takedownsFirstXMinutes",
"itemsPurchased",
"visionScoreAdvantageLaneOpponent",
"twentyMinionsIn3SecondsCount",
"perfectDragonSoulsTaken",
"teamRiftHeraldKills"
            };

            // Pobierz właściwości tylko te, które są w powyższej liście
            var selectedFields = type.GetProperties()
                .Where(p => selectedFieldNames.Contains(p.Name, StringComparer.OrdinalIgnoreCase))
                .ToList();

            // Dodaj kolumnę końcową "rank", jeśli istnieje
            var lastField = type.GetProperty(lastColumnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (lastField != null)
                selectedFields.Add(lastField);

            // --- Nagłówki ---
            var headers = string.Join(",", selectedFields.Select(p => p.Name));
            writer.WriteLine(headers);

            // --- Dane ---
            int i = 1;
            foreach (var element in allItems)
            {
                if (element.rank == Rank.NONE) continue;
                Console.Write($"\r Dyskretyzacja {type.Name}: {i++}/{count}");
                List<string> line = new List<string>();

                foreach (var item in selectedFields)
                {
                    var value = item.GetValue(element);
                    var discret = Discretisation.FirstOrDefault(d => d.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));

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

            return $"{type.Name}_discretisation_bins_{bins}_chosenColumns";
        }

        public static string dyskretzacjaTestTestLastB<T>(DbSet<T> dbSet, int bins = 5, string lastColumnName = "rank") where T : Secondary
        {
            var type = typeof(T);
            Console.WriteLine("Typ elementu w DbSet: " + type.FullName);

            var properties = type.GetProperties()
                .Where(p => p.PropertyType != typeof(string) && p.PropertyType != typeof(bool))
                .ToList();

            var allItems = dbSet.ToList();
            Console.WriteLine("Typ elementu w DbSet: " + allItems.Count);
            //throw new NotImplementedException();    
            List<BinObject> Discretisation = new List<BinObject>();
            int count = allItems.Count;

            // --- WYZNACZ BINNING DLA PÓL NUMERYCZNYCH ---
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

            var writer = new StreamWriter(path + @"\" + $"{type.Name}_discretisation_bins_{bins}_chosenColumns.csv");

            // --- WYBÓR KONKRETNYCH POL ---
            string[] selectedFieldNames = new string[]
            {
"summonerLevel",
"laneMinionsFirst10Minutes",
"totalTimeSpentDead",
"gameLength",
"timePlayed",
"enemyVisionPings",
"visionWardsBoughtInGame",
"wardTakedowns",
"wardTakedownsBefore20M",
"controlWardsPlaced",
"wardsKilled",
"champion",
"goldPerMinute",
"earliestBaron",
"onMyWayPings",
"deathsByEnemyChamps",
"visionScorePerMinute",
"totalMinionsKilled",
"detectorWardsPlaced",
"controlWardTimeCoverageInRiverOrEnemyHalf",
"deaths",
"commandPings",
"stealthWardsPlaced",
"multiTurretRiftHeraldCount",
"magicDamageTaken",
"killsOnOtherLanesEarlyJungleAsLaner",
"takedownsInEnemyFountain",
"atakhan_first",
"atakhan",
"consumablesPurchased",
"quickCleanse",
"getTakedownsInAllLanesEarlyJungleAsLaner",
"spell1Casts",
"abilityUses",
"spell3Casts",
"horde",
"killParticipation",
"totalDamageDealt",
"firstBloodAssist",
"firstBloodKill",
"skillshotsDodged",
"enemyMissingPings",
"magicDamageDealt",
"moreEnemyJungleThanOpponent",
"takedownsFirstXMinutes",
"itemsPurchased",
"visionScoreAdvantageLaneOpponent",
"twentyMinionsIn3SecondsCount",
"perfectDragonSoulsTaken",
"teamRiftHeraldKills"
            };

            // Pobierz właściwości tylko te, które są w powyższej liście
            var selectedFields = type.GetProperties()
                .Where(p => selectedFieldNames.Contains(p.Name, StringComparer.OrdinalIgnoreCase))
                .ToList();

            // Dodaj kolumnę końcową "rank", jeśli istnieje
            var lastField = type.GetProperty(lastColumnName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (lastField != null)
                selectedFields.Add(lastField);

            // --- Nagłówki ---
            var headers = string.Join(",", selectedFields.Select(p => p.Name));
            writer.WriteLine(headers);

            // --- Dane ---
            int i = 1;
            foreach (var element in allItems)
            {
                if (element.rank == Rank.NONE) continue;
                Console.Write($"\r Dyskretyzacja {type.Name}: {i++}/{count}");
                List<string> line = new List<string>();

                foreach (var item in selectedFields)
                {
                    var value = item.GetValue(element);
                    var discret = Discretisation.FirstOrDefault(d => d.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase));

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

            return $"{type.Name}_discretisation_bins_{bins}_chosenColumns";
        }


        public class BinObject
        {
            public string Name;
            public float Min;
            public float Max;
            public float BinSize;

            public BinObject(string name, float min, float max, float binSize)
            {
                Name = name;
                Min = min;
                Max = max;
                BinSize = binSize;
            }

            public string CreateBin(float number)
            {
                int binIndex = (int)((number - Min) / BinSize);

                return $"{Name}_{binIndex * BinSize + Min:F2}_{(binIndex + 1) * BinSize + Min:F2}".Replace(",","_").Replace("-", "_");
            }
        }


        public static string dyskretzacjaTestBinEqualReduced<T>(DbSet<T> dbSet, int bins = 5, int maxColumns = 220, string lastColumnName = "rank") where T : Secondary
        {
            var type = typeof(T);
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

            var writer = new StreamWriter(path + @"\" + $"{type.Name}_discretisation_bins_{bins}_amount{maxColumns}_evenBins_reduced.csv");

            // --- WYBÓR I KOLEJNOŚĆ KOLUMN ---
            var allFields = type.GetProperties().Where(p => p.PropertyType != typeof(string)).ToList();
            var lastField = allFields.FirstOrDefault(p => p.Name.Equals(lastColumnName, StringComparison.OrdinalIgnoreCase));
            var limitedFields = new List<PropertyInfo>();

            foreach (var p in allFields)
            {
                if (p.Name.Equals(lastColumnName, StringComparison.OrdinalIgnoreCase)) continue;
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
            return $"{type.Name}_discretisation_bins_{bins}_amount{maxColumns}_evenBins_reduced";
        }

        public static string dyskretzacjaTestBinEqualNumbered<T>(DbSet<T> dbSet, int bins = 5, int maxColumns = 220, string lastColumnName = "rank") where T : Secondary
        {
            var type = typeof(T);
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

            var writer = new StreamWriter(path + @"\" + $"{type.Name}_discretisation_bins_{bins}_amount{maxColumns}_evenBins_number.csv");

            // --- WYBÓR I KOLEJNOŚĆ KOLUMN ---
            var allFields = type.GetProperties().Where(p => p.PropertyType != typeof(string)).ToList();
            var lastField = allFields.FirstOrDefault(p => p.Name.Equals(lastColumnName, StringComparison.OrdinalIgnoreCase));
            var limitedFields = new List<PropertyInfo>();

            foreach (var p in allFields)
            {
                if (p.Name.Equals(lastColumnName, StringComparison.OrdinalIgnoreCase)) continue;
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
            return $"{type.Name}_discretisation_bins_{bins}_amount{maxColumns}_evenBins_number";
        }

        public static string dyskretzacjaTestBinEqualReducedB<T>(DbSet<T> dbSet, int bins = 5, int maxColumns = 220, string lastColumnName = "rank") where T : Secondary_averave
        {
            var type = typeof(T);
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

                    for(int j = 1;j <= bins; j++)
                    {
                        if (boundaries[j - 1] >= boundaries[j])
                        {
                            boundaries[j] = boundaries[j - 1]+0.01f;
                        }
                    }
                    Discretisation.Add(new BinObject2(prop.Name, boundaries));
                }
            }

            var writer = new StreamWriter(path + @"\" + $"{type.Name}_discretisation_bins_{bins}_amount{maxColumns}_evenBins_reduced.csv");

            // --- WYBÓR I KOLEJNOŚĆ KOLUMN ---
            var allFields = type.GetProperties().Where(p => p.PropertyType != typeof(string)).ToList();
            var lastField = allFields.FirstOrDefault(p => p.Name.Equals(lastColumnName, StringComparison.OrdinalIgnoreCase));
            var limitedFields = new List<PropertyInfo>();

            foreach (var p in allFields)
            {
                if (p.Name.Equals(lastColumnName, StringComparison.OrdinalIgnoreCase)) continue;
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
            return $"{type.Name}_discretisation_bins_{bins}_amount{maxColumns}_evenBins_reduced";
        }

        public static string dyskretzacjaTestBinEqualNumberedB<T>(DbSet<T> dbSet, int bins = 5, int maxColumns = 220, string lastColumnName = "rank") where T : Secondary_averave
        {
            var type = typeof(T);
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

            var writer = new StreamWriter(path + @"\" + $"{type.Name}_discretisation_bins_{bins}_amount{maxColumns}_evenBins_number.csv");

            // --- WYBÓR I KOLEJNOŚĆ KOLUMN ---
            var allFields = type.GetProperties().Where(p => p.PropertyType != typeof(string)).ToList();
            var lastField = allFields.FirstOrDefault(p => p.Name.Equals(lastColumnName, StringComparison.OrdinalIgnoreCase));
            var limitedFields = new List<PropertyInfo>();

            foreach (var p in allFields)
            {
                if (p.Name.Equals(lastColumnName, StringComparison.OrdinalIgnoreCase)) continue;
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
            return $"{type.Name}_discretisation_bins_{bins}_amount{maxColumns}_evenBins_number";
        }
        // Nowa wersja BinObject
        public class BinObject2
        {
            public string Name;
            public List<float> Boundaries;

            public BinObject2(string name, List<float> boundaries)
            {
                Name = name;
                Boundaries = boundaries;
            }

            public string CreateBin(float number)
            {
                for (int i = 0; i < Boundaries.Count - 1; i++)
                {
                    if (number >= Boundaries[i] && number <= Boundaries[i + 1])
                    {
                        return $"{Name}_{Boundaries[i]:F2}_{Boundaries[i + 1]:F2}".Replace(",", "_").Replace("-", "_");
                    }
                }

                // fallback (np. gdy jest poza zakresem z powodu błędu zaokrągleń)
                return $"{Name}_unknown";
            }
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
