using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Smile;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using DiscordRPC.Message;

namespace magisterka2
{
    public class Diagnostics
    {
        static void Diagnostic()
        {
            /*
            //using var db = new DiagnosticDbContext();
            //db.Database.Migrate(); // automatycznie tworzy bazę i tabelę

            string modelPath = "model.xdsl";
            string targetNode = "Disease";

            // Wczytaj model
            Network net = new Network();
            net.ReadFile(modelPath);

            // Utwórz obiekt diagnostyczny
            DiagNetwork diag = new DiagNetwork(net);
            //iagSession diag = new DiagSession(diagNet);
            diag.SetTarget(targetNode);
            diag.
            // Ustaw wszystkie inne nody jako obserwowalne
            foreach (string node in net.GetAllNodeIds())
                diag.SetObservable(node, node != targetNode);
            diag.SingleFaultAlgorithm = DiagNetwork.SingleFaultAlgorithmType.NormCrossentropy;
            net.UpdateBeliefs();
            
            // Pobierz ranking diagnostyczny
            List<DiagObservation> ranking = diag.GetObservationRanking();

            Console.WriteLine($"Zapisuję wyniki dla: {modelPath} (target: {targetNode})");

            foreach (var obs in ranking)
            {
                var result = new DiagnosticResult
                {
                    FileName = System.IO.Path.GetFileName(modelPath),
                    Target = targetNode,
                    Node = obs.Id,
                    MPC = obs.GetMaxProbChange(),
                    CE = obs.GetCrossEntropy(),
                    NCE = obs.GetNormalizedCrossEntropy()
                };

                db.DiagnosticResults.Add(result);
            }

            db.SaveChanges();
            Console.WriteLine("✅ Wyniki zapisane w bazie danych EF Core!");
            */
        }




        public static List<DiagnosticResult> AnalyzeAndSaveModel_DiagNetwork(string modelPath, DbContext db,
                                                                     DiagNetwork.SingleFaultAlgorithmType singleFaultAlgorithm = DiagNetwork.SingleFaultAlgorithmType.ProbChange)
        {
            if (!File.Exists(modelPath))
                throw new FileNotFoundException("Model .xdsl not found", modelPath);

            //using var db = new DiagnosticDbContext();
            //db.Database.Migrate();

            var saved = new List<DiagnosticResult>();

            // Załaduj model
            using (var net = new Network())
            {
                net.ReadFile(modelPath);

                // --- (opcjonalnie) programowe ustawienie ról diagnostycznych ---
                // Jeśli Twój .xdsl nie ma ról: net.SetNodeDiagType(nodeId, Network.NodeDiagType.Observation/Fault/Aux)
                // i dla faultów: net.SetFaultOutcome(nodeId, outcomeId) oraz dla obserwacji: net.SetDiagRanked(nodeId, true)
                // (Patrz sekcja "Diagnostic roles" w dokumentacji.)
                //
                // Przykład (odkomentuj jeśli potrzebujesz ustawić role programowo):
                // foreach (var nodeId in net.GetAllNodeIds()) {
                //     // ustawić wszystkie jako observation oprócz wybranego fault (jeśli wiesz który)
                //     net.SetNodeDiagType(nodeId, Network.NodeDiagType.Observation);
                // }
                // net.SetNodeDiagType("Disease", Network.NodeDiagType.Fault);
                //
                // WAŻNE: najlepiej ustawić role w XDSL przed uruchomieniem analizy.

                // Utwórz obiekt diagnostyczny (sesję)
                using (var diag = new DiagNetwork(net))
                {
                    // Wybierz algorytm dla pojedynczego-faultowego rankingu
                    diag.SingleFaultAlgorithm = singleFaultAlgorithm;

                    // Zaktualizuj (diag.Update() zwraca DiagResults)
                    DiagResults diagResults = diag.Update();

                    // diagResults.faults => tablica FaultInfo (prawdopodobieństwa faultów)
                    // diagResults.observations => tablica ObservationInfo (measure, cost, infoGain, node)
                    // Jeśli chcesz ustawić pursued fault ręcznie:
                    // int mostLikely = diag.FindMostLikelyFault();
                    // diag.SetPursuedFault(mostLikely);
                    // diagResults = diag.Update();

                    // Zidentyfikuj 'target' (pursued fault) - pobieramy aktualnie ustawiony pursued fault index i jego nazwy
                    int pursuedIndex = diag.GetPursuedFault();
                    string targetDesc;
                    if (pursuedIndex >= 0 && pursuedIndex < diag.FaultCount)
                    {
                        string faultNodeId = diag.GetFaultNodeId(pursuedIndex);
                        string faultOutcomeId = diag.GetFaultOutcomeId(pursuedIndex);
                        targetDesc = $"{faultNodeId}={faultOutcomeId}";
                    }
                    else
                    {
                        // fallback: pusty lub "none"
                        targetDesc = "none";
                    }

                    // Przechodzimy po obserwacjach (ranking)
                    foreach (var obs in diagResults.Observations)
                    {
                        // ObservationInfo has: node (handle), measure, cost, infoGain
                        string nodeId = net.GetNodeId(obs.Node);
                        double measure = obs.Measure;
                        double cost = obs.Cost;
                        double infoGain = obs.InfoGain;
                        /*
                        var record = new DiagnosticResult
                        {
                            FileName = Path.GetFileName(modelPath),
                            Target = targetDesc,
                            Node = nodeId,
                            Measure = measure,
                            Cost = cost,
                            InfoGain = infoGain,
                            CreatedAt = DateTime.UtcNow
                        };

                        db.DiagnosticResults.Add(record);
                        saved.Add(record);*/
                    }

                    db.SaveChanges();
                } // diag disposed
            } // net disposed

            return saved;
        }

        public static void RankValues(GameDbContext db)
        {
            var grupy = db.DiagnosticResults
                .GroupBy(r => new { r.FileName, r.TargetValue }) // <- tu 2 kolumny
                .ToList();

            foreach (var grupa in grupy)
            {
                var posortowane = grupa
                    .OrderByDescending(r => Math.Abs(r.MPC))
                    .ToList();

                for (int i = 0; i < posortowane.Count; i++)
                {
                    posortowane[i].MPCRank = i + 1; // pozycja 1, 2, 3...
                }
                var posortowane2 = grupa
    .OrderByDescending(r => Math.Abs(r.CE))
    .ToList();

                for (int i = 0; i < posortowane2.Count; i++)
                {
                    posortowane2[i].CERank = i + 1; // pozycja 1, 2, 3...
                }
                var posortowane3 = grupa
    .OrderByDescending(r => Math.Abs(r.NCE))
    .ToList();

                for (int i = 0; i < posortowane3.Count; i++)
                {
                    posortowane3[i].NCERank = i + 1; // pozycja 1, 2, 3...
                }
            }
            db.SaveChanges ();
        }
        static string path = @"C:\magisterka\Bayes\NewTest\";
        public static void Calculations(GameDbContext db)
        {

            /*List<NodeSumDto> FileSumMPC = AllCodeAnalitics(db, x => x.MPCRank);
            List<NodeSumDto> FileSumCE = AllCodeAnalitics(db, x => x.CERank);
            List<NodeSumDto> FileSumNCE = AllCodeAnalitics(db, x => x.NCERank);
            */

            //List<NodeSumDto> suma_tan_MPC = GetDataByBayesType(db, "-tan", x => x.MPCRank);
            //List<NodeSumDto> suma_tan_CE = GetDataByBayesType(db, "-tan", x => x.CERank);
            List<NodeSumDto> suma_tan_NCE = GetDataByBayesType(db, "Secondary_averave_5_evenBins_reduced-tan", x => x.NCERank);

            /*
            List<NodeSumDto> suma_bs_MPC = GetDataByBayesType(db, "-bs", x => x.MPCRank);
            List<NodeSumDto> suma_naive_MPC = GetDataByBayesType(db, "-naive", x => x.MPCRank);            
            List<NodeSumDto> suma_bs_CE = GetDataByBayesType(db, "-bs", x => x.CERank);
            List<NodeSumDto> suma_naive_CE = GetDataByBayesType(db, "-naive", x => x.CERank);            
            List<NodeSumDto> suma_bs_NCE = GetDataByBayesType(db, "-bs", x => x.NCERank);
            List<NodeSumDto> suma_naive_NCE = GetDataByBayesType(db, "-naive", x => x.NCERank);
            */
/*            int maxLength = new[]
{
    FileSumMPC.Count, FileSumCE.Count, FileSumNCE.Count,
    suma_bs_MPC.Count, suma_tan_MPC.Count, suma_naive_MPC.Count,
    suma_bs_CE.Count, suma_tan_CE.Count, suma_naive_CE.Count,
    suma_bs_NCE.Count, suma_tan_NCE.Count, suma_naive_NCE.Count
}.Max();
            int columnWidth = 43;
            string output = "";
            //Console.WriteLine 
                output +=(
                $"{(nameof(FileSumMPC)).PadRight(columnWidth)}" +
                $"{(nameof(FileSumCE)).PadRight(columnWidth)}" +
                $"{(nameof(FileSumNCE)).PadRight(columnWidth)}" +
                $"{(nameof(suma_bs_MPC)).PadRight(columnWidth)}" +
                $"{(nameof(suma_tan_MPC)).PadRight(columnWidth)}" +
                $"{(nameof(suma_naive_MPC)).PadRight(columnWidth)}" +
                $"{(nameof(suma_bs_CE)).PadRight(columnWidth)}" +
                $"{(nameof(suma_tan_CE)).PadRight(columnWidth)}" +
                $"{(nameof(suma_naive_CE)).PadRight(columnWidth)}" +
                $"{(nameof(suma_bs_NCE)).PadRight(columnWidth)}" +
                $"{(nameof(suma_tan_NCE)).PadRight(columnWidth)}" +
                $"{(nameof(suma_naive_NCE)).PadRight(columnWidth)}\n" 
                );
            for (int i = 0; i < maxLength; i++)
            {
                //Console.WriteLine
                output += (
                    $"{(FileSumMPC.ElementAtOrDefault(i)?.ToString() ?? "").PadRight(columnWidth)}" +
                    $"{(FileSumCE.ElementAtOrDefault(i)?.ToString() ?? "").PadRight(columnWidth)}" +
                    $"{(FileSumNCE.ElementAtOrDefault(i)?.ToString() ?? "").PadRight(columnWidth)}" +
                    $"{(suma_bs_MPC.ElementAtOrDefault(i)?.ToString() ?? "").PadRight(columnWidth)}" +
                    $"{(suma_tan_MPC.ElementAtOrDefault(i)?.ToString() ?? "").PadRight(columnWidth)}" +
                    $"{(suma_naive_MPC.ElementAtOrDefault(i)?.ToString() ?? "").PadRight(columnWidth)}" +
                    $"{(suma_bs_CE.ElementAtOrDefault(i)?.ToString() ?? "").PadRight(columnWidth)}" +
                    $"{(suma_tan_CE.ElementAtOrDefault(i)?.ToString() ?? "").PadRight(columnWidth)}" +
                    $"{(suma_naive_CE.ElementAtOrDefault(i)?.ToString() ?? "").PadRight(columnWidth)}" +
                    $"{(suma_bs_NCE.ElementAtOrDefault(i)?.ToString() ?? "").PadRight(columnWidth)}" +
                    $"{(suma_tan_NCE.ElementAtOrDefault(i)?.ToString() ?? "").PadRight(columnWidth)}" +
                    $"{(suma_naive_NCE.ElementAtOrDefault(i)?.ToString() ?? "").PadRight(columnWidth)}\n"

                    );
            }
            File.WriteAllText($@"{path}\data.txt", output);
            Console.WriteLine(output);
            AnalizisByGroup(db, x => x.NCERank, "NCERank");
            AnalizisByGroup(db, x => x.CERank, "CERank");
            AnalizisByGroup(db, x => x.MPCRank, "MPCRank");
            AnalizisByGroupByFile(db, "-bs", x => x.NCERank, "NCERank");
            AnalizisByGroupByFile(db, "-tan", x => x.NCERank, "NCERank");
            AnalizisByGroupByFile(db, "-naive", x => x.NCERank, "NCERank");            
            AnalizisByGroupByFile(db, "-bs", x => x.CERank, "CERank");
            AnalizisByGroupByFile(db, "-tan", x => x.CERank, "CERank");
            AnalizisByGroupByFile(db, "-naive", x => x.CERank, "CERank");           
            AnalizisByGroupByFile(db, "-bs", x => x.MPCRank, "MPCRank");
            AnalizisByGroupByFile(db, "-tan", x => x.MPCRank, "MPCRank");
            AnalizisByGroupByFile(db, "-naive", x => x.MPCRank, "MPCRank");
*/
            /*Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaNumbered(db.Secondary, 5, suma_naive_NCE.Take(50).Select(x => x.Node).ToList(),name: "_suma_naive_NCE"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaNumbered(db.Secondary_Awerage, 5, suma_naive_NCE.Take(50).Select(x => x.Node).ToList(),name: "_suma_naive_NCE"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReduced(db.Secondary, 5, suma_naive_NCE.Take(50).Select(x => x.Node).ToList(),name: "_suma_naive_NCE"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReduced(db.Secondary_Awerage, 5, suma_naive_NCE.Take(50).Select(x => x.Node).ToList(),name: "_suma_naive_NCE"));
            */Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaNumberedEqual(db.Secondary, 5, suma_tan_NCE.Take(100).Select(x => x.Node).ToList(), name: "_nxt"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaNumberedEqual(db.Secondary_Awerage, 5, suma_tan_NCE.Take(100).Select(x => x.Node).ToList(), name: "_nxt"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary, 5, suma_tan_NCE.Take(100).Select(x => x.Node).ToList(), name: "_nxt"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary_Awerage, 5, suma_tan_NCE.Take(100).Select(x => x.Node).ToList(), name: "_nxt"));
            /*Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary, 5, FileSumMPC.Take(50).Select(x => x.Node).ToList(),name: "_FileSumMPC"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary, 5, FileSumCE.Take(50).Select(x => x.Node).ToList(),name: "_FileSumCE"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary, 5, FileSumNCE.Take(50).Select(x => x.Node).ToList(),name: "_FileSumNCE"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary, 5, suma_bs_MPC.Take(50).Select(x => x.Node).ToList(),name: "_suma_bs_MPC"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary, 5, suma_tan_MPC.Take(50).Select(x => x.Node).ToList(),name: "_suma_tan_MPC"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary, 5, suma_naive_MPC.Take(50).Select(x => x.Node).ToList(),name: "_suma_naive_MPC"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary, 5, suma_bs_CE.Take(50).Select(x => x.Node).ToList(),name: "_suma_bs_CE"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary, 5, suma_tan_CE.Take(50).Select(x => x.Node).ToList(),name: "_suma_tan_CE"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary, 5, suma_naive_CE.Take(50).Select(x => x.Node).ToList(),name: "_suma_naive_CE"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary, 5, suma_bs_NCE.Take(50).Select(x => x.Node).ToList(),name: "_suma_bs_NCE"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary, 5, suma_naive_NCE.Take(50).Select(x => x.Node).ToList(),name: "_suma_naive_NCE"));

            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary_Awerage, 5, FileSumMPC.Take(50).Select(x => x.Node).ToList(), name: "_FileSumMPC"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary_Awerage, 5, FileSumCE.Take(50).Select(x => x.Node).ToList(), name: "_FileSumCE"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary_Awerage, 5, FileSumNCE.Take(50).Select(x => x.Node).ToList(), name: "_FileSumNCE"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary_Awerage, 5, suma_bs_MPC.Take(50).Select(x => x.Node).ToList(), name: "_suma_bs_MPC"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary_Awerage, 5, suma_tan_MPC.Take(50).Select(x => x.Node).ToList(), name: "_suma_tan_MPC"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary_Awerage, 5, suma_naive_MPC.Take(50).Select(x => x.Node).ToList(), name: "_suma_naive_MPC"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary_Awerage, 5, suma_bs_CE.Take(50).Select(x => x.Node).ToList(), name: "_suma_bs_CE"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary_Awerage, 5, suma_tan_CE.Take(50).Select(x => x.Node).ToList(), name: "_suma_tan_CE"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary_Awerage, 5, suma_naive_CE.Take(50).Select(x => x.Node).ToList(), name: "_suma_naive_CE"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary_Awerage, 5, suma_bs_NCE.Take(50).Select(x => x.Node).ToList(), name: "_suma_bs_NCE"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary_Awerage, 5, suma_tan_NCE.Take(50).Select(x => x.Node).ToList(), name: "_suma_tan_NCE"));
            Bayes.Tutorial10(NewDiscretisations.DyscrtyzacjaReducedEqual(db.Secondary_Awerage, 5, suma_naive_NCE.Take(50).Select(x => x.Node).ToList(), name: "_suma_naive_NCE"));
            */
            //List<NodeSum2Dto> suma_bs_target_MPC = GetItemByGroup(db, "-bs", x => x.MPCRank);
        }

        private static void AnalizisByGroup(GameDbContext db, Func<DiagnosticResult, double> selector, string name)
        {
            int columnWidth = 43;
            var grupy = db.DiagnosticResults
    .GroupBy(r =>  r.TargetValue ) // <- tu 2 kolumny
    .ToList();
            Dictionary<string, List<NodeSumDto>> groupResults = new Dictionary<string, List<NodeSumDto>>();
            foreach(var group in grupy)
            {
                List<NodeSumDto> groupsum = AllCodeAnaliticsList(group, selector);
                Console.WriteLine(group.Key);
                groupResults.Add(group.Key, groupsum);
            }

            string output = "";
            List<int> maxsize = new List<int>();
            foreach(var group in groupResults)
            {
                output += group.Key.ToString().PadRight(columnWidth);
                maxsize.Add(group.Value.Count);
            }
                output += "\n";

            int maximum = maxsize.Max();

            for(int i = 0; i < maximum; i++)
            {
                foreach (var group in groupResults)
                {
                    output += (group.Value.ElementAtOrDefault(i)?.ToString() ?? "").PadRight(columnWidth);
                }
                    output += "\n";
            }
            File.WriteAllText($@"{path}\data2-{name}.txt", output);
        }
        private static void AnalizisByGroupByFile(GameDbContext db, string file, Func<DiagnosticResult, double> selector, string name)
        {
            int columnWidth = 43;
            var grupy = db.DiagnosticResults
    .GroupBy(r => r.TargetValue) // <- tu 2 kolumny
    .ToList();
            Dictionary<string, List<NodeSumDto>> groupResults = new Dictionary<string, List<NodeSumDto>>();
            foreach (var group in grupy)
            {
                List<NodeSumDto> groupsum = GetDataByBayesType(group, file, selector);
                Console.WriteLine(group.Key);
                groupResults.Add(group.Key, groupsum);
            }

            string output = "";
            List<int> maxsize = new List<int>();
            foreach (var group in groupResults)
            {
                output += group.Key.ToString().PadRight(columnWidth);
                maxsize.Add(group.Value.Count);
            }
            output += "\n";

            int maximum = maxsize.Max();

            for (int i = 0; i < maximum; i++)
            {
                foreach (var group in groupResults)
                {
                    output += (group.Value.ElementAtOrDefault(i)?.ToString() ?? "").PadRight(columnWidth);
                }
                output += "\n";
            }
            File.WriteAllText($@"{path}\data2{file}-{name}.txt", output);
        }
        private static List<NodeSumDto> AllCodeAnaliticsList(IGrouping<string, DiagnosticResult> db, Func<DiagnosticResult, double> selector)
        {
            return db
.GroupBy(r => r.Node).AsEnumerable()
.Select(g => new NodeSumDto
{
    Node = g.Key,
    Suma = g.Sum(selector)
}).OrderBy(r => r.Suma)
.ToList();
        }

        private static List<NodeSumDto> GetDataByBayesType(IGrouping<string, DiagnosticResult> db, string Ending, Func<DiagnosticResult, double> selector)
        {
            var result = db
                .Where(r => r.FileName.Contains(Ending))
                .AsEnumerable() // 🔹 przenosi do pamięci .NET
                .GroupBy(r => r.Node)
                .Select(g => new NodeSumDto
                {
                    Node = g.Key,
                    Suma = g.Sum(selector)
                })
                .OrderBy(r => r.Suma)
                .ToList();
            return result;
        }

        private static List<NodeSumDto> AllCodeAnalitics(GameDbContext db, Func<DiagnosticResult, double> selector)
        {
            return db.DiagnosticResults
.GroupBy(r => r.Node).AsEnumerable()
.Select(g => new NodeSumDto
{
    Node = g.Key,
    Suma = g.Sum(selector)
}).OrderBy(r => r.Suma)
.ToList();
        }

        private static List<NodeSumDto> GetDataByBayesType(GameDbContext db, string Ending, Func<DiagnosticResult, double> selector)
        {
            var result = db.DiagnosticResults
                .Where(r => r.FileName.Contains(Ending))
                .AsEnumerable() // 🔹 przenosi do pamięci .NET
                .GroupBy(r => r.Node)
                .Select(g => new NodeSumDto
                {
                    Node = g.Key,
                    Suma = g.Sum(selector)
                })
                .OrderBy(r => r.Suma)
                .ToList();
            return result;
        }

        private static List<NodeSum2Dto> GetItemByGroup(GameDbContext db, string Ending, Func<DiagnosticResult, double> selector)
        {
            var grupy = db.DiagnosticResults
    .Where(r => r.FileName != null && r.FileName.Contains(Ending))
    .GroupBy(r => r.TargetValue)
    .Select(g => new NodeSum2Dto
    {
        D = g.Key,
        Nodes = g.GroupBy(x => x.Node)
                 .Select(ng => new NodeSumDto
                 {
                     Node = ng.Key,
                     Suma = ng.Sum(selector)
                 })
                 .OrderByDescending(n => n.Suma)
                 .ToList()
    })
    .ToList();
                return grupy;
        }

        public static List<DiagnosticResult> AnalyzeAndSaveModel_Metrics(string modelPath, string targetNode, GameDbContext db)
        {
            //using var db = new DiagnosticDbContext();
            //db.Database.Migrate();

            var results = new List<DiagnosticResult>();

            using var net = new Network();
            net.ReadFile(modelPath);

            



            Console.WriteLine("=== All node IDs in model ===");
            var all = net.GetAllNodeIds();
            //foreach (var id in all)
            //    Console.WriteLine("  " + id);

            // 1) Sprawdź, czy target istnieje
            bool targetExists = Array.Exists(all, x => x.Equals(targetNode, StringComparison.Ordinal));
            if (!targetExists)
            {
                Console.WriteLine($"ERROR: Node '{targetNode}' does not exist in the model (check exact ID and casing).");
                return null;
            }

            net.SetNodeDiagType(targetNode, Network.NodeDiagType.Fault);




            string[] outcomes = net.GetOutcomeIds(targetNode);

            foreach (string outcome in outcomes)
            {
                //string faultOutcome = outcomes[0];
                net.SetFaultOutcome(targetNode, outcome, true);
            }

            foreach (string nodeId in net.GetAllNodeIds())
            {
                if (nodeId != targetNode)
                {
                    net.SetNodeDiagType(nodeId, Network.NodeDiagType.Observation);
                    net.SetRanked(nodeId, true);// (String nodeId, boolean ranked);
                }
                
            }


            /*foreach (string nodeId in net.GetAllNodeIds())
            {
                Console.WriteLine($"{nodeId} {net.GetNodeDiagType(nodeId)}");
            }*/
            // --- ustawienie celu (fault node) ---
            // Jeśli model ma role diagnostyczne, wystarczy ustawić pursued fault na ten węzeł

            using var diag = new DiagNetwork(net);

            
            int faultIndex = -1;
            for (int i = 0; i < diag.FaultCount; i++)
            {
                if (diag.GetFaultNodeId(i) == targetNode)
                {
                    faultIndex = i;
                    break;
                }
            }
            //diag.InstantiateObservation("soloKills", 0);
            if (faultIndex >= 0)
            {
                diag.SetPursuedFault(faultIndex);
            }

            // --- obliczamy wartości dla 3 algorytmów ---
            var metrics = new Dictionary<string, (double MPC, double CE, double NCE)>();

            foreach (var outcome in outcomes)
            {
                int outcomeId = diag.GetFaultIndex(targetNode, outcome);
                diag.SetPursuedFault(outcomeId);

                foreach (var algorithm in new[]
                {
            DiagNetwork.SingleFaultAlgorithmType.ProbChange,
            DiagNetwork.SingleFaultAlgorithmType.Crossentropy,
            DiagNetwork.SingleFaultAlgorithmType.NormCrossentropy
        })
                {
                    diag.SingleFaultAlgorithm = algorithm;
                    var res = diag.Update();
                    //PrintDiagResults(net, res);
                    foreach (var obs in res.Observations)
                    {
                        string nodeId = net.GetNodeName(obs.Node);

                        if (!metrics.ContainsKey(nodeId))
                            metrics[nodeId] = (0, 0, 0);

                        switch (algorithm)
                        {
                            case DiagNetwork.SingleFaultAlgorithmType.ProbChange:
                                metrics[nodeId] = (obs.Measure, metrics[nodeId].CE, metrics[nodeId].NCE);
                                break;
                            case DiagNetwork.SingleFaultAlgorithmType.Crossentropy:
                                metrics[nodeId] = (metrics[nodeId].MPC, obs.Measure, metrics[nodeId].NCE);
                                break;
                            case DiagNetwork.SingleFaultAlgorithmType.NormCrossentropy:
                                metrics[nodeId] = (metrics[nodeId].MPC, metrics[nodeId].CE, obs.Measure);
                                break;
                        }
                    }
                }

                // --- zapis do bazy ---
                foreach (var kvp in metrics)
                {
                    var record = new DiagnosticResult
                    {
                        FileName = Path.GetFileName(modelPath),
                        Target = targetNode,
                        TargetValue = outcome,
                        Node = kvp.Key,
                        MPC = kvp.Value.MPC,
                        CE = kvp.Value.CE,
                        NCE = kvp.Value.NCE,
                        CreatedAt = DateTime.UtcNow
                    };

                    db.DiagnosticResults.Add(record);
                    results.Add(record);
                }
            }

            db.SaveChanges();
            Console.WriteLine($"✅ Zapisano {results.Count} rekordów dla modelu {Path.GetFileName(modelPath)} (target: {targetNode})");

            return results;
        }

        public static void PrintDiagResults(Network net, DiagResults diagResults)
        {
            Console.WriteLine("Diag results start\n\nFaults:");
            Console.WriteLine("{0,-20} {1,-20} {2,-24} {3,-8}",
            "Node Id", "Outcome Id", "Probability", "Is Pursued");
            foreach (FaultInfo faultInfo in diagResults.Faults)
            {
                PrintFaultInfo(net, faultInfo);
            }
            Console.WriteLine("\nObservations:");
            Console.WriteLine("{0,-20} {1,-24} {2,-8} {3,-20}",
            "Node Id", "Measure", "Cost", "InfoGain");
            foreach (ObservationInfo observationInfo in diagResults.Observations)
            {
                PrintObservationInfo(net, observationInfo);
            }
            Console.WriteLine("\nDiag results end\n");
        }

        public static void PrintFaultInfo(Network net, FaultInfo fault)
        {
            Console.WriteLine("{0,-20} {1,-20} {2,-24} {3,-8}",
            net.GetNodeId(fault.Node), net.GetOutcomeId(fault.Node, fault.Outcome),
            fault.Probability, fault.IsPursued ? "Yes" : "No");
        }
        public static void PrintObservationInfo(Network net, ObservationInfo observation)
        {
            Console.WriteLine("{0,-20} {1,-24} {2,-8} {3,-20}",
            net.GetNodeId(observation.Node), observation.Measure,
            observation.Cost, observation.InfoGain);
        }

        public static void BuildProfilesForAllClasses(GameDbContext db, string name, string className)
        {

            var net = new Network();
            net.ReadFile(name);
            Console.WriteLine($"IT WORKS!!! + {name}");
            int classNode = net.GetNode(className);
            int classStateCount = net.GetOutcomeCount(classNode);

            for (int c = 0; c < classStateCount; c++)
            {
                string classState = net.GetOutcomeId(classNode, c);

                net.ClearAllEvidence();
                net.UpdateBeliefs();

                double baseProb = net.GetNodeValue(classNode)[c];

                var results = new List<(string Feature, string Value, double Prob, double Lift)>();

                int nodeCount = net.GetNodeCount();

                for (int n = 0; n < nodeCount; n++)
                {
                    string nodeId = net.GetNodeId(n);

                    if (nodeId == className)
                        continue;

                    /*if (net.GetNodeType(n) != Network.NodeType.CPT)
                        continue;
                    */
                    int outcomeCount = net.GetOutcomeCount(n);

                    double bestLift = double.MinValue;
                    string bestValue = "";
                    double bestProb = 0;

                    for (int i = 0; i < outcomeCount; i++)
                    {
                        string value = net.GetOutcomeId(n, i);

                        net.ClearAllEvidence();
                        net.SetEvidence(nodeId, value);
                        net.UpdateBeliefs();

                        double prob = net.GetNodeValue(classNode)[c];
                        double lift = prob / baseProb;

                        if (lift > bestLift)
                        {
                            bestLift = lift;
                            bestValue = value;
                            bestProb = prob;
                        }
                    }

                    results.Add((nodeId, bestValue, bestProb, bestLift));
                }

                var ordered = results.OrderByDescending(r => r.Lift);

                Console.WriteLine($"\n==============================");
                Console.WriteLine($"PROFIL dla {className} = {classState}");
                Console.WriteLine($"Bazowe P = {baseProb:F4}");
                Console.WriteLine($"==============================");

                foreach (var r in ordered)
                {

                    db.ClassProfiles.Add(
                    new ClassProfiles
                    {
                        Sorce = name,
                        Feature = r.Feature,
                        Class = classState,
                        Best = r.Value,
                        P = r.Prob,
                        BaseP = baseProb,
                        Lift = r.Lift,
                    });
                }

                db.SaveChanges();
            }
        }





    }

    public class ClassProfiles
    {
        public int Id { get; set; }
        public string Feature { get; set; }
        public string Sorce { get; set; }
        public string Class { get; set; }
        public string Best { get; set; }
        public double P { get; set; }
        public double BaseP { get; set; }
        public double Lift { get; set; }
    }
    public class DiagnosticResult
    {
        //[Key]
        public int Id { get; set; }

        public string FileName { get; set; } = string.Empty;
        public string Target { get; set; } = string.Empty;
        public string TargetValue {  get; set; } = string.Empty;
        public string Node { get; set; } = string.Empty;

        public double MPC { get; set; }
        public double CE { get; set; }
        public double NCE { get; set; }

        public int MPCRank { get; set; }
        public int CERank { get; set; }
        public int NCERank { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    public class NodeSumDto
    {
        public string Node { get; set; }
        public double Suma { get; set; }

        public override string ToString()
        {
            return Node.ToString();
        }
    }

    public class NodeSum2Dto
    {
        public string D { get; set; }
        public List<NodeSumDto> Nodes { get; set;}
    }


}
