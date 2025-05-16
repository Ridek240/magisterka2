using magisterka2;
using System;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main()
    {
        Discord.Initialise();
        Stopwatch stopwatch = new Stopwatch();

        // Rozpoczynamy mierzenie czasu
        stopwatch.Start();
        AddMatch addMatch = new AddMatch();
        using var db = new GameDbContext();
        //db.Database.EnsureCreated();
        /*var values = db.PlayerRanks.ToList();

        foreach(var value in values) {
            Console.WriteLine($"{value.puuid} {value.Rank}");
        }*/
        //addMatch.IterateThrouFolder();
        //AddMatch.AnalyzePrimaryFieldsToFile(db, @"C:\magisterka\Bazy\srednie.json");

        Metoda(stopwatch, addMatch, db).GetAwaiter().GetResult(); 
        // Wyświetlamy czas w milisekundach
        stopwatch.Stop();
        Console.WriteLine($"Czas wykonania: {stopwatch.ElapsedMilliseconds} ms");
        /*foreach (var p in people)
        {
            Console.WriteLine($"ID: {p.matchId}, Name: {p.participantId}");
        }*/
        //addMatch.AddRootObjectAsync();
        while (true) { }
    }

    private static async Task Metoda(Stopwatch stopwatch, AddMatch addMatch, GameDbContext db)
    {
        List<string> uniquePuuids = db.Primary.Select(p => p.puuid).Distinct().ToList();
        AddMatch.FixApi();
        Console.WriteLine($"Do przetworzenia {uniquePuuids.Count}");
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
                Console.WriteLine($"puuid {puuid} już istnieje, pomijam");
                continue; // pomijamy już istniejące
            }

            var rank = await addMatch.FindRankByPUUID(puuid, i, max);
            buffer.Add(new PlayerRank(puuid, rank));
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


        await addMatch.GetSecondaryData(db);
    }
}