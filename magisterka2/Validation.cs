using Smile.Learning;
using Smile;
using System.Diagnostics;


namespace magisterka2
{
    public class Validation
    {
        public static GameDbContext db;
        public static void ValidateAll( string networkFile, string dataFile, int kFolds = 5)
        {

            Console.WriteLine($"Start {networkFile} {dataFile}");
            var ds = new DataSet();
            ds.ReadFile(dataFile);
            Console.WriteLine($"IT WORKS!!!");

            var net = new Network();
            net.ReadFile(networkFile);

            var matching = ds.MatchNetwork(net);
            int classNode = net.GetNode("rank");
            Console.WriteLine($"IT WORKS!!!");

            var validator = new Validator(net, ds, matching);
            validator.AddClassNode(classNode);

            EM em = new EM();

            //RunValidation("TEST", () => validator.Test(), validator, net, classNode, networkFile);
            RunValidation("KFOLD", () => validator.KFold(em, kFolds), validator, net, classNode, networkFile);
            //RunValidation("LOO", () => validator.LeaveOneOut(em), validator, net, classNode, dataFile);
        }


        private static void RunValidation(
            string type,
            Action validationAction,
            Validator validator,
            Network net,
            int classNode,
            string dataFile)
        {
            Console.WriteLine($"{type} Starts");

            var sw = Stopwatch.StartNew();
            validationAction();
            sw.Stop();

            int outcomeCount = net.GetOutcomeCount(classNode);

            //
            // OUTCOME RESULTS (LINQ)
            //
            var outcomeResults = Enumerable.Range(0, outcomeCount)
                .Select(i => new OutcomeResult
                {
                    OutcomeIndex = i,
                    OutcomeName = net.GetOutcomeId(classNode, i),
                    Accuracy = validator.GetAccuracy(classNode, i)
                })
                .ToList();

            //
            // GLOBAL ACCURACY
            //
            double globalAccuracy = outcomeResults.Average(o => o.Accuracy);


            //
            // CONFUSION MATRIX (LINQ + flatten)
            //
            int[][] matrix = ConvertToJagged(validator.GetConfusionMatrix(classNode));

            var confusionEntries =
                Enumerable.Range(0, outcomeCount)
                .SelectMany(trueIndex =>
                    Enumerable.Range(0, outcomeCount)
                        .Select(predIndex => new ConfusionMatrixEntry
                        {
                            TrueIndex = trueIndex,
                            PredictedIndex = predIndex,
                            Count = matrix[trueIndex][predIndex]
                        })
                )
                .ToList();


            //
            // ROC CURVE (dla każdego stanu)
            //
            var rocPoints = new List<RocPoint>();

            for (int i = 0; i < outcomeCount; i++)
            {
                var roc = validator.GetROC(classNode, i);

                // smile ROC zwraca listę punktów typu Curve.Point
                rocPoints = roc.x
                    .Select((fpr, index) => new RocPoint
                    {
                        OutcomeIndex = i,
                        Fpr = fpr,
                        Tpr = roc.y[index]
                    })
                    .ToList();
            }


            //
            // TWORZENIE OBIEKTU REZULTATU
            //
            var result = new ValidationResult
            {
                DataFile = dataFile,
                ValidationType = type,
                GlobalAccuracy = globalAccuracy,
                TimeMs = sw.ElapsedMilliseconds,
                OutcomeResults = outcomeResults,
                ConfusionMatrix = confusionEntries,
                RocPoints = rocPoints
            };

            db.ValidationResults.Add(result);
            db.SaveChanges();
        }
        public static int[][] ConvertToJagged(int[,] array2D)
        {
            int rows = array2D.GetLength(0);
            int cols = array2D.GetLength(1);

            int[][] jaggedArray = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                jaggedArray[i] = new int[cols];
                for (int j = 0; j < cols; j++)
                {
                    jaggedArray[i][j] = array2D[i, j];
                }
            }

            return jaggedArray;
        }

        public static void IterateFolders()
        {
            string folderPath = @"C:\magisterka\Bayes\NewTest\Etap3"; // podaj ścieżkę do folderu
            string fileExtension = "*.xdsl";
            if (Directory.Exists(folderPath))
            {
                // Pobieramy wszystkie foldery w danym katalogu
                string[] directories = Directory.GetDirectories(folderPath);

                // Wyświetlamy same nazwy folderów (bez pełnej ścieżki)
                foreach (string dir in directories)
                {
                    string[] files = Directory.GetFiles(dir, fileExtension);


                    foreach (string file in files)
                    {
                        ValidateAll(file, dir + ".csv");
                        //Diagnostics.Kuraw(file);
                        Console.WriteLine($"  Plik: {Path.GetFileName(file)}");
                    }
                }
            }
        }

        public static void IterateFolders2(GameDbContext db)
        {
            string folderPath = @"C:\magisterka\Bayes\NewTest\classy"; // podaj ścieżkę do folderu
            string fileExtension = "*.xdsl";
            if (Directory.Exists(folderPath))
            {
                /*// Pobieramy wszystkie foldery w danym katalogu
                string[] directories = Directory.GetDirectories(folderPath);
                
                // Wyświetlamy same nazwy folderów (bez pełnej ścieżki)
                foreach (string dir in directories)
                {*/
                db.ClassProfiles.RemoveRange(db.ClassProfiles);
                db.SaveChanges();
                    string[] files = Directory.GetFiles(folderPath, fileExtension);


                    foreach (string file in files)
                    {
                        //ValidateAll(file, dir + ".csv");
                        Diagnostics.BuildProfilesForAllClasses(db, file, "rank");
                        Console.WriteLine($"  Plik: {Path.GetFileName(file)}");
                    }
                
            }
        }

        public static void IterateFoldersLeanrn()
        {
            string folderPath = @"C:\magisterka\Bayes\NewTest\TestBayes"; // podaj ścieżkę do folderu
            string fileExtension = "*.csv";
            if (Directory.Exists(folderPath))
            {

                    string[] files = Directory.GetFiles(folderPath, fileExtension);


                    foreach (string file in files)
                    {
                        Bayes.BSTEST(file);
                        //ValidateAll(file, dir + ".csv");
                        //Console.WriteLine($"  Plik: {Path.GetFileName(file)}");
                    }
                
            }
        }

        public static void IterateFolders(string prefix)
        {
            
            string folderPath = @$"C:\magisterka\Bayes\NewTest\{prefix}"; // podaj ścieżkę do folderu
            string fileExtension = "*.xdsl";
            if (Directory.Exists(folderPath))
            {

                    string[] files = Directory.GetFiles(prefix, fileExtension);


                    foreach (string file in files)
                    {
                        ValidateAll(file, prefix + ".csv");
                        Console.WriteLine($"  Plik: {Path.GetFileName(file)}");
                    }
                }
            }
        }
    



    public class ValidationResult
    {
        public int Id { get; set; }
        public string DataFile { get; set; }
        public string ValidationType { get; set; } // TEST / KFOLD / LOO
        public double GlobalAccuracy { get; set; }
        public long TimeMs { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<OutcomeResult> OutcomeResults { get; set; } = new();
        public List<ConfusionMatrixEntry> ConfusionMatrix { get; set; } = new();
        public List<RocPoint> RocPoints { get; set; } = new();
    }

    public class OutcomeResult
    {
        public int Id { get; set; }
        public string OutcomeName { get; set; }
        public int OutcomeIndex { get; set; }
        public double Accuracy { get; set; }
    }

    public class ConfusionMatrixEntry
    {
        public int Id { get; set; }

        public int TrueIndex { get; set; }
        public int PredictedIndex { get; set; }
        public int Count { get; set; }
    }

    public class RocPoint
    {
        public int Id { get; set; }

        public int OutcomeIndex { get; set; }
        public double Fpr { get; set; } // False Positive Rate
        public double Tpr { get; set; } // True Positive Rate
    }

}