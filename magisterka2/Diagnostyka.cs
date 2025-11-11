using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Smile;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations;

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

        public static void Calculations(GameDbContext db)
        {
            var sumaPerFile = db.DiagnosticResults
    .GroupBy(r => r.Node)
    .Select(g => new
    {
        Node = g.Key,
        Suma = g.Sum(x => x.MPCRank)
    }).OrderBy(r=>r.Suma)
    .ToList();
            foreach (var suma in sumaPerFile)
            {
                Console.WriteLine($"{suma.Node} {suma.Suma}");

            }
            Console.WriteLine("\n");
            sumaPerFile = db.DiagnosticResults
.GroupBy(r => r.Node)
.Select(g => new
{
Node = g.Key,
Suma = g.Sum(x => x.CERank)
}).OrderBy(r => r.Suma)
.ToList();
            foreach (var suma in sumaPerFile)
            {
                Console.WriteLine($"{suma.Node} {suma.Suma}");

            }
            Console.WriteLine("\n");
            sumaPerFile = db.DiagnosticResults
.GroupBy(r => r.Node)
.Select(g => new
{
Node = g.Key,
Suma = g.Sum(x => x.NCERank)
}).OrderBy(r => r.Suma)
.ToList();
            foreach (var suma in sumaPerFile)
            {
                Console.WriteLine($"{suma.Node} {suma.Suma}");

            }
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
}
