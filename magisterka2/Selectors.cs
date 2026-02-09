using Microsoft.Data.SqlClient.DataClassification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace magisterka2
{
    public static class Selectors
    {
        public static void PrintData(GameDbContext db, string delimeter)
        {
            var list = db.ValidationResults.Where(x => x.ValidationType == "KFOLD" && x.DataFile.Contains(delimeter) && !x.DataFile.Contains("suma_naive") && !x.DataFile.Contains("suma_tan") && !x.DataFile.Contains("evenBins")).ToList();

            foreach (var item in list)
            {
                Console.WriteLine($"{item.GlobalAccuracy}");
            }
        }

        public static void ConfusionMatrixPrtint(GameDbContext db, string delimeter)
        {
            /*&& !x.DataFile.Contains("suma_naive") && !x.DataFile.Contains("suma_tan") && !x.DataFile.Contains("evenBins")*/
            var list = db.ValidationResults.Where(x => x.ValidationType == "KFOLD" && x.DataFile.Contains(delimeter) /*&& !x.DataFile.Contains("-bs")  && !x.DataFile.Contains("suma_naive") && x.DataFile.Contains("suma_tan")*/).SelectMany(x => x.ConfusionMatrix).ToList();
            var summedList = list
    .GroupBy(x => new { x.TrueIndex, x.PredictedIndex })
    .Select(g => new ConfusionMatrixEntry
    {
        TrueIndex = g.Key.TrueIndex,
        PredictedIndex = g.Key.PredictedIndex,
        Count = g.Sum(x => x.Count)
    })
    .ToList();


            var columns = summedList
    .Select(x => x.TrueIndex)
            .Distinct()
    .OrderBy(x => x)
    .ToList();

            var rows = summedList
                .Select(x => x.PredictedIndex)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            var rangs = new List<int> {6,0,9,4,8,3,2,7,5,1 };
            //var rangs = new List<int> {0,2,1 };
            columns = rangs;
            rows = rangs;
            Console.Write("B \\ A\t");

            foreach (var col in columns)
            {
                Console.Write(col + "\t");
            }
            Console.WriteLine();

            foreach (var row in rows)
            {
                Console.Write(row + "\t");

                foreach (var col in columns)
                {
                    var value = summedList
                        .Where(x => x.TrueIndex == col && x.PredictedIndex == row)
                        .Select(x => x.Count)
                        .FirstOrDefault();

                    Console.Write(value + "\t");
                }

                Console.WriteLine();
            }

        }


    }
}
