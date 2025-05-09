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

                    var list = await GetPlayersInElo(rank.ToString(),division.ToString());

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

        public static void FixApi()
        {
            string sciezka = @"C:\tournament\ApiKey.txt";
            try
            {
                if (File.Exists(sciezka))
                {
                    Api_Key = File.ReadAllText(sciezka);
                    Console.WriteLine("Klucz API: " + Api_Key);
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

        public enum Rank
        {
            //IRON,
            //BRONZE,
            //SILVER,
            //GOLD,
            PLATINUM,
            EMERALD,
            DIAMOND

        }
        public enum Division
        {
            I,
            II,
            III,
            IV

        }

    }
}
