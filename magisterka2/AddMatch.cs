using DiscordRPC;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace magisterka2
{
    internal class AddMatch
    {
        
        public async Task AddRootObjectAsync()
        {

            foreach (var rank in Enum.GetValues(typeof(Rank)))
            {
                foreach (var division in Enum.GetValues(typeof(Division)))
                {

                    var list = await GetPlayersInElo(rank.ToString(), division.ToString());

                    Console.WriteLine(list.Count);
                    //return;
                    var list2 = await GetMatchIdsAsync(list);
                    string filePath = $@"C:\magisterka\dane\{rank.ToString()}_{division.ToString()}.txt";
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                    foreach (var item in list2)
                    {
                        var ret = await GetMatchAsync(item);
                        await File.AppendAllTextAsync(filePath, ret + Environment.NewLine);
                        Console.WriteLine($"Dodano match ID: {item} do pliku {filePath}.");
                    }
                    await Task.Delay(6 * 30000);
                }
            }
            //var rootObject = await GetMatchAsync("EUN1_3777110123");

            /*using (var context = new GameDbContext())
            {

                context.Database.EnsureCreated();
                await context.RootObjects.AddAsync(rootObject);

                // Zapisanie zmian w bazie danych
                await context.SaveChangesAsync();


            }*/
        }


        public static string Api_Key;

        public async Task<List<string>> GetPlayersInElo(string rank, string division)
        {
            FixApi();
            string api_url = $"https://eun1.api.riotgames.com/lol/league/v4/entries/RANKED_SOLO_5x5/{rank}/{division}?page=1&api_key={Api_Key}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Send GET request asynchronously
                    HttpResponseMessage response = await client.GetAsync(api_url);

                    // Check if the response was successful
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        JArray players = JArray.Parse(jsonResponse);
                        List<string> playerheads = new List<string>();
                        foreach (JObject row in players)
                        //JObject row = (JObject)players[0];
                        {
                            playerheads.Add(row["puuid"]?.ToString());
                        }

                        return playerheads;
                    }
                    else
                    {
                        // Handle unsuccessful response
                        Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}Elo Request failed with status code: {response.StatusCode}");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., network issues)
                    Console.WriteLine($"Request failed with exception: {ex.Message}");
                    return null;
                }
            }
        }

        public static async Task<List<string>> GetMatchIdsAsync(List<string> puuids)
        {
            List<string> matchIds = new List<string>();

            foreach (string puuid in puuids)
            {
                while (true)
                {
                    string apiUrl = $"https://europe.api.riotgames.com/lol/match/v5/matches/by-puuid/{puuid}/ids?queue=420&start=0&count=20&api_key={Api_Key}";

                    try
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            HttpResponseMessage response = await client.GetAsync(apiUrl);

                            if (response.IsSuccessStatusCode)
                            {
                                string responseBody = await response.Content.ReadAsStringAsync();
                                List<string> ids = JsonConvert.DeserializeObject<List<string>>(responseBody);
                                matchIds.AddRange(ids);
                                Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}:{ids.Count} : Added");
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}:{response.StatusCode} : czekam");
                                FixApi();
                                await Task.Delay(4 * 30000); // 30 sekund
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        FixApi();
                        await Task.Delay(4 * 30000);
                    }
                }
            }

            return matchIds;
        }
        public static async Task<string> GetMatchAsync(string matchid)
        {
            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}:Searching for match with ID: {matchid}");



            using (HttpClient client = new HttpClient())
            {
                while (true)
                {
                    try
                    {
                        string apiUrl = $"https://europe.api.riotgames.com/lol/match/v5/matches/{matchid}?api_key={Api_Key}";
                        HttpResponseMessage response = await client.GetAsync(apiUrl);

                        if (response.IsSuccessStatusCode)
                        {
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}:Successfully got: {matchid}");
                            return jsonResponse;
                        }
                        else
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}:Match Request failed with status code: {response.StatusCode}. Retrying in 30 seconds...");
                            FixApi();
                            await Task.Delay(4 * 30000); // 30 seconds
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Request failed with exception: {ex.Message}. Retrying in 30 seconds...");
                        FixApi();
                        await Task.Delay(4 * 30000); // 30 seconds
                    }
                }
            }
        }
        static string[] klucze;
        static int Current_Key = 0;
        public static void FixApi()
        {
            string sciezka = @"C:\tournament\ApiKey.txt";
            try
            {
                if (File.Exists(sciezka))
                {
                    klucze = File.ReadAllLines(sciezka); // Wczytanie wszystkich linii do tablicy stringów
                    foreach (string linia in klucze)
                    {
                        Console.WriteLine("Linia: " + linia);
                    }

                    // Jeśli np. chcesz przypisać pierwszą linię jako klucz API:
                    if (klucze.Length > 0)
                    {
                        Api_Key = klucze[0];
                        Console.WriteLine("Klucz API: " + Api_Key);
                        Current_Key++;
                        if(Current_Key>= klucze.Length) { Current_Key = 0; }    
                    }
                }
                else
                {
                    Console.WriteLine("Plik nie istnieje: " + sciezka);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas odczytu pliku: " + ex.Message);
            }
        }



        public List<Primary> GetDataFromJson(string json)
        {


            var Output = new List<Primary>();
            Rootobject Rootobj = JsonConvert.DeserializeObject<Rootobject>(json);

            if (Rootobj == null) return Output;

            foreach (var particilan in Rootobj.info.participants)
            {
                //Console.WriteLine($"Przerabiam {Rootobj.metadata.matchId} {particilan.participantId}");

                Objectives objectives = Rootobj.info.teams.First(x => x.teamId == particilan.teamId).objectives;

                var stat = new Primary(Rootobj.metadata.matchId, particilan, particilan.challenges, objectives);


                Output.Add(stat);
            }
            Console.WriteLine($"Wynik {Output.Count}");
            return Output;
        }

        public void GetMatchJson()
        {
            using var db = new GameDbContext();
            db.Database.EnsureCreated();
            int i = 0;
            foreach (var line in File.ReadLines(@"C:\magisterka\dane\BRONZE_I.txt"))
            {

                var output = GetDataFromJson(line);

                foreach (var item in output)
                {
                    bool exists = db.Primary.Any(p => p.matchId == item.matchId && p.participantId == item.participantId);
                    if (!exists)
                    {
                        db.Primary.Add(item);
                    }
                }
                Console.WriteLine($"Zokończono linie {i++}");
                db.SaveChanges();

            }
        }

        public void GetMatchJson2(string file)
        {
            using var db = new GameDbContext();
            db.Database.EnsureCreated();
            var allExistingKeys = new HashSet<(string matchId, int participantId)>(
    db.Primary.Select(p => new { p.matchId, p.participantId })
              .AsEnumerable()
              .Select(x => (x.matchId, x.participantId))
);

            // BUFOR NA NOWE ELEMENTY
            List<Primary> buffer = new();
            int batchSize = 2000;
            int i = 0;

            foreach (string line in File.ReadLines(file))
            {
                var output = GetDataFromJson(line);

                foreach (var item in output)
                {
                    var key = (item.matchId, item.participantId);

                    if (!allExistingKeys.Contains(key))
                    {
                        buffer.Add(item);
                        allExistingKeys.Add(key); // dodaj też do cache'u żeby nie dodać 2x w buforze
                    }

                    if (buffer.Count >= batchSize)
                    {
                        db.Primary.AddRange(buffer);
                        db.SaveChanges();
                        buffer.Clear();
                        Console.WriteLine($"Zapisano {batchSize} rekordów (linia {i})");
                    }
                }

                Console.WriteLine($"Zakończono linię {i++}");
            }

            // zapisz to co zostało
            if (buffer.Count > 0)
            {
                db.Primary.AddRange(buffer);
                db.SaveChanges();
                Console.WriteLine($"Zapisano ostatnie {buffer.Count} rekordów");
            }
        }

        public void IterateThrouFolder()
        {
            string folderPath = @"C:\magisterka\dane";

            // Pobierz wszystkie pliki w folderze (możesz dodać filtr np. "*.txt")
            string[] files = Directory.GetFiles(folderPath);

            foreach (string file in files)
            {
                Console.WriteLine($"Przetwarzam Plik {file}");

                // Przykład: czytanie pierwszej linii
                GetMatchJson2(file);
            }
        }

        public static void AnalyzePrimaryFields(GameDbContext db)
        {
            var data = db.Primary.ToList(); // 1. Pobierz dane z bazy

            var numericProps = typeof(Primary).GetProperties()
                .Where(p => p.PropertyType == typeof(int) ||
                            p.PropertyType == typeof(double) ||
                            p.PropertyType == typeof(float) ||
                            p.PropertyType == typeof(long) ||
                            p.PropertyType == typeof(decimal))
                .ToList();

            foreach (var prop in numericProps)
            {
                var values = data
                    .Select(item => prop.GetValue(item))
                    .Where(v => v != null)
                    .Select(v => Convert.ToDouble(v))
                    .ToList();

                if (values.Count == 0)
                    continue;

                double average = values.Average();
                double stddev = Math.Sqrt(values.Select(v => Math.Pow(v - average, 2)).Average());
                double min = values.Min();
                double max = values.Max();

                Console.WriteLine($"Pole: {prop.Name}");
                Console.WriteLine($"  Średnia: {average:F2}");
                Console.WriteLine($"  Odchylenie standardowe: {stddev:F2}");
                Console.WriteLine($"  Min: {min}");
                Console.WriteLine($"  Max: {max}");
                Console.WriteLine();
            }
        }

        public static void AnalyzePrimaryFieldsToFile(GameDbContext db, string filePath)
        {
            var data = db.Primary.ToList();

            var numericProps = typeof(Primary).GetProperties()
                .Where(p => p.PropertyType == typeof(int) ||
                            p.PropertyType == typeof(double) ||
                            p.PropertyType == typeof(float) ||
                            p.PropertyType == typeof(long) ||
                            p.PropertyType == typeof(decimal))
                .ToList();

            var sb = new StringBuilder();


            foreach (var prop in numericProps)
            {
                var values = data
                    .Select(item => prop.GetValue(item))
                    .Where(v => v != null)
                    .Select(v => Convert.ToDouble(v))
                    .ToList();

                if (values.Count == 0)
                    continue;

                double average = values.Average();
                double stddev = Math.Sqrt(values.Select(v => Math.Pow(v - average, 2)).Average());
                double min = values.Min();
                double max = values.Max();

                var result = new
                {
                    Field = prop.Name,
                    Average = average,
                    StdDev = stddev,
                    Min = min,
                    Max = max
                };

                string jsonLine = JsonConvert.SerializeObject(result);
                sb.Append(jsonLine + Environment.NewLine);
                //sb.AppendText("sciezka.json", jsonLine + Environment.NewLine);

                Console.WriteLine($"Zapisano {prop.Name}");
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
            Console.WriteLine($"Statystyki zapisane do pliku: {filePath}");
        }

        public static void AnalyzeSecondaryFieldsByRankToFile(GameDbContext db, string filePath)
        {
            var data = db.Secondary.ToList();

            var numericProps = typeof(Secondary).GetProperties()
                .Where(p => p.PropertyType == typeof(int) ||
                            p.PropertyType == typeof(double) ||
                            p.PropertyType == typeof(float) ||
                            p.PropertyType == typeof(long) ||
                            p.PropertyType == typeof(decimal))
                .ToList();

            var sb = new StringBuilder();

            // Grupujemy dane po Rank (zakładam, że to np. enum lub string)
            var groups = data.GroupBy(d => d.rank);

            foreach (var group in groups)
            {
                string rankName = group.Key.ToString();
                int groupCount = group.Count();
                foreach (var prop in numericProps)
                {
                    var values = group
                        .Select(item => prop.GetValue(item))
                        .Where(v => v != null)
                        .Select(v => Convert.ToDouble(v))
                        .ToList();

                    if (values.Count == 0)
                        continue;

                    double average = values.Average();
                    double stddev = Math.Sqrt(values.Select(v => Math.Pow(v - average, 2)).Average());
                    double min = values.Min();
                    double max = values.Max();

                    var result = new
                    {
                        Rank = rankName,
                        Field = prop.Name,
                        Count = values.Count, // <- liczba elementów w tej konkretnej analizowanej kolumnie
                        GroupSize = groupCount, // <- liczba rekordów w tej randze
                        Average = average,
                        StdDev = stddev,
                        Min = min,
                        Max = max
                    };

                    string jsonLine = JsonConvert.SerializeObject(result);
                    sb.AppendLine(jsonLine);

                    Console.WriteLine($"Zapisano statystyki dla rangi {rankName}, pola {prop.Name}");
                }
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
            Console.WriteLine($"Statystyki według rangi zapisane do pliku: {filePath}");
        }

        public async Task<Rank> FindRankByPUUID(string puuid, int i , int max)
        {
            //FixApi();
            
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    while (true)
                    {
                        string api_url = $"https://eun1.api.riotgames.com/lol/league/v4/entries/by-puuid/{puuid}?api_key={Api_Key}";
                        // Send GET request asynchronously
                        HttpResponseMessage response = await client.GetAsync(api_url);

                        // Check if the response was successful
                        if (response.IsSuccessStatusCode)
                        {
                            string jsonResponse = await response.Content.ReadAsStringAsync();
                            JArray players = JArray.Parse(jsonResponse);
                            foreach (var queue in players)
                            {
                                if (queue["queueType"]?.ToString() == "RANKED_SOLO_5x5")
                                {
                                    string tier = queue["tier"]?.ToString();
                                    if (Enum.TryParse(queue["tier"]?.ToString().ToUpper(), out Rank result))
                                    {
                                        return result;
                                    }
                                    else return Rank.NONE;

                                }
                            }
                            return Rank.NONE;

                        }
                        else
                        {
                            Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")}:Match Request failed with status code: {response.StatusCode}. Retrying in 2 minutes...");
                            FixApi();
                            Discord.SetRichPresence("Rangi Graczy", "Czekam", i, max);
                            await Task.Delay(4 * 30000); // 2 minut
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., network issues)
                    Console.WriteLine($"Request failed with exception: {ex.Message}");
                    return Rank.NONE;
                }
            }
            }





        public async Task GetSecondaryData(GameDbContext db)
        {
            var allExistingKeys = new HashSet<(string matchId, string puuid)>(
    db.Secondary.Select(p => new { p.matchId, p.puuid })
    .AsEnumerable()
    .Select(x => (x.matchId, x.puuid))
    );
            int max = db.Primary.Count();
            // BUFOR NA NOWE ELEMENTY
            List<Secondary> buffer = new();
            int batchSize = 2000;
            int i = 0;

            var playerRanksDict = await db.PlayerRanks
                .ToDictionaryAsync(x => x.puuid, x => x.Rank);

            await foreach (var data in db.Primary.AsAsyncEnumerable())
            {
                var key = (data.matchId, data.puuid);

                if (!allExistingKeys.Contains(key))
                {
                    var rank = playerRanksDict.TryGetValue(data.puuid, out var r) ? r : Rank.NONE;
                    buffer.Add(new Secondary(rank, data));
                    allExistingKeys.Add(key);
                    i++;
                    Discord.SetRichPresence("Secondary Data", "Przetwarzam", i, max);
                }

                if (buffer.Count >= batchSize)
                {
                    db.Secondary.AddRange(buffer);
                    db.SaveChanges();
                    buffer.Clear();
                    Console.WriteLine($"Zapisano {batchSize} rekordów (linia {i})");
                }
            }

        }

        public async Task GetSecondaryData2(GameDbContext db)
        {
            var allExistingKeys = new HashSet<(string matchId, string puuid)>(
                db.Secondary.AsNoTracking()
                    .Select(p => new { p.matchId, p.puuid })
                    .AsEnumerable()
                    .Select(x => (x.matchId, x.puuid))
            );

            int batchSize = 2000;
            int processed = 0;
            int processed_new = 0;

            var playerRanksDict = await db.PlayerRanks
                .ToDictionaryAsync(x => x.puuid, x => x.Rank);

            string lastMatchId = null;
            string lastPuuid = null;
            int count = await db.Primary.AsNoTracking().CountAsync();
            int count_secondary = allExistingKeys.Count();
            Discord.SetRichPresenceUpdate("Secondary Data", "Przetwarzam", processed, count); // total możesz wyliczyć osobno

            var lastKey = await db.Secondary
    .OrderByDescending(x => x.matchId)
    .ThenByDescending(x => x.puuid)
    .Select(x => new { x.matchId, x.puuid })
    .FirstOrDefaultAsync();
            lastMatchId = lastKey.matchId;
            lastPuuid = lastKey.puuid;
            while (true)
            {

                Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} Rozpoczynam querry Secondary count: {count_secondary} ProcessedCount {processed} Procdessed new: {processed_new}+");
                var query = db.Primary.AsNoTracking().AsQueryable();

                if (lastMatchId != null && lastPuuid != null)
                {
                    query = query.Where(x =>
                        string.Compare(x.matchId, lastMatchId) > 0 ||
                        (x.matchId == lastMatchId && string.Compare(x.puuid, lastPuuid) > 0));
                }

                var batch = await query.AsNoTracking()
                    .OrderBy(x => x.matchId)
                    .ThenBy(x => x.puuid)
                    .Take(batchSize)
                    .ToListAsync();

                if (batch.Count == 0)
                    break;
                Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} Zakończono querry bufferuje batch {processed} : {processed_new}+");
                List<Secondary> buffer = new();

                foreach (var data in batch)
                {
                    var key = (data.matchId, data.puuid);

                    if (!allExistingKeys.Contains(key))
                    {
                        var rank = playerRanksDict.TryGetValue(data.puuid, out var r) ? r : Rank.NONE;
                        buffer.Add(new Secondary(rank, data));
                        allExistingKeys.Add(key);
                        processed_new++;
                        
                    }
                    processed++;
                    // Zaktualizuj ostatni klucz do paginacji
                    lastMatchId = data.matchId;
                    lastPuuid = data.puuid;
                }
                Discord.SetRichPresenceUpdate("Secondary Data", "Przetwarzam", processed, count-count_secondary); // total możesz wyliczyć osobno
                if (buffer.Any())
                {
                    db.Secondary.AddRange(buffer);
                    await db.SaveChangesAsync();
                    Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} Zapisano {buffer.Count} rekordów (łącznie {processed})");
                    db.ChangeTracker.Clear();
                }
            }
        }

    }





}


    public enum Rank
    {
        NONE,
        IRON,
        BRONZE,
        SILVER,
        GOLD,
        PLATINUM,
        EMERALD,
        DIAMOND,
        MASTER,
        GRANDMASTER,
        CHALLENGER

    }

public static class RankExtensions
{
    public static string GetDisplayName(this Rank rank)
    {
        return rank switch
        {
            Rank.IRON => "0Iron",
            Rank.BRONZE => "1Bronze",
            Rank.SILVER => "2Silver",
            Rank.GOLD => "3Gold",
            Rank.PLATINUM => "4Platinum",
            Rank.EMERALD => "5Emerald",
            Rank.DIAMOND => "6Diamond",
            Rank.MASTER => "7Master",
            Rank.GRANDMASTER => "8Grandmaster",
            Rank.CHALLENGER => "9Challenger",
            _ => "None"
        };
    }

    public static string GetDisplayNameReduced(this Rank rank)
    {
        return rank switch
        {
            Rank.IRON => "Averrage",
            Rank.BRONZE => "Averrage",
            Rank.SILVER => "Averrage",
            Rank.GOLD => "Averrage",
            Rank.PLATINUM => "Skilled",
            Rank.EMERALD => "Skilled",
            Rank.DIAMOND => "Skilled",
            Rank.MASTER => "Elite",
            Rank.GRANDMASTER => "Elite",
            Rank.CHALLENGER => "Elite",
            _ => "None"
        };
    }
}
public enum Division
    {
        I,
        II,
        III,
        IV

    }

