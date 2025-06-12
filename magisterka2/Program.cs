using magisterka2;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main()
    {
        Discord.Initialise();
        Stopwatch stopwatch = new Stopwatch();
        Licencing.SmileLicence();
        // Rozpoczynamy mierzenie czasu
        stopwatch.Start();
        AddMatch addMatch = new AddMatch();
        //using var db = new GameDbContext();
        //Bayes.ConvestToCSV(db.Primary);
        //Bayes.ConvestToCSV(db.Secondary,5);
        //db.Dispose();
        Bayes.Tutorial10("Secondary_exported");

        //db.Database.EnsureCreated();
        /*var values = db.PlayerRanks.ToList();

        foreach(var value in values) {
            Console.WriteLine($"{value.puuid} {value.Rank}");
        }*/
        //addMatch.IterateThrouFolder();
        //

        //fix database

        /*var to_fix = db.PlayerRanks.ToList();
        foreach(var to in to_fix)
        {
            var name = db.Primary.FirstOrDefault(x => x.puuid == to.puuid).riotIdGameName;
            var tag = db.Primary.FirstOrDefault(x => x.puuid == to.puuid).riotIdTagline;
            to.riotIdTagline = tag;
            to.riotIdGameName = name;
        }

        db.SaveChanges();
        */
        //db.Secondary.RemoveRange(db.Secondary);
        //db.SaveChanges();
        //Metoda(stopwatch, addMatch, db).GetAwaiter().GetResult(); 
        // Wyświetlamy czas w milisekundach
        /*
        addMatch.GetSecondaryData2(db).GetAwaiter().GetResult();
        Console.WriteLine($"Czas wykonania: {stopwatch.ElapsedMilliseconds} ms");
        AddMatch.AnalyzeSecondaryFieldsByRankToFile(db, @"C:\magisterka\Bazy\secondary_srednie2.json");
        stopwatch.Stop();
        Console.WriteLine($"Czas wykonania: {stopwatch.ElapsedMilliseconds} ms");
        /*foreach (var p in people)
        {
            Console.WriteLine($"ID: {p.matchId}, Name: {p.participantId}");
        }*/
        //addMatch.AddRootObjectAsync();
        //while (true) { }
    }



    private static async Task Metoda(Stopwatch stopwatch, AddMatch addMatch, GameDbContext db)
    {
        List<string> uniquePuuids = db.Primary.Select(p => p.puuid).Distinct().ToList();
        AddMatch.FixApi();
        Console.WriteLine($"Do przetworzenia {uniquePuuids.Count}");
        Console.WriteLine($"Przetworzono {db.PlayerRanks.Count()}");
        int max = uniquePuuids.Count;
        int i = 0;
        int batchSize = 100;
        var buffer = new List<PlayerRank>(batchSize);

        // Pobieramy puuid, które już są w bazie (możesz ograniczyć do tylko tych z uniquePuuids)
        var existingPuuids = new HashSet<string>(db.PlayerRanks
            .Where(pr => uniquePuuids.Contains(pr.puuid))
            .Select(pr => pr.puuid));

        foreach (var puuid in uniquePuuids)
        {
            Discord.SetRichPresenceUpdate("Rangi Graczy", "Przetwarzam", i, max - existingPuuids.Count);
            if (existingPuuids.Contains(puuid))
            {
                //Console.WriteLine($"puuid {puuid} już istnieje, pomijam");
                continue; // pomijamy już istniejące
            }
            //var name = db.Primary.FirstOrDefault(x => x.puuid == puuid).riotIdGameName;
            //var tag = db.Primary.FirstOrDefault(x => x.puuid == puuid).riotIdTagline;
            var rank = await addMatch.FindRankByPUUID(puuid, i, max);
            buffer.Add(new PlayerRank(puuid, rank,"",""));
            Console.WriteLine($"przetworzony {i++}");

            if (buffer.Count >= batchSize)
            {
                db.PlayerRanks.AddRange(buffer);
                await db.SaveChangesAsync();
                buffer.Clear();
            }
        }

        if (buffer.Count > 0)
        {
            db.PlayerRanks.AddRange(buffer);
            await db.SaveChangesAsync();
        }

        Console.WriteLine($"Czas wykonania: {stopwatch.ElapsedMilliseconds} ms");


        
    }
}