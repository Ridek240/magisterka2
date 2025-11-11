using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace magisterka2
{
    internal class Other
    {

        public static void Checkup<T>(DbSet<T> dbSet) where T : Secondary_averave
        {
            var wyniki = dbSet
    .GroupBy(p => p.puuid)
    .Select(g => new
    {
        Kategoria = g.Key,
        LiczbaWystapien = g.Count()
    })
    .OrderByDescending(x => x.LiczbaWystapien);

            /*
            foreach (var w in wyniki)
            {
                Console.WriteLine($"{w.Kategoria}: {w.LiczbaWystapien}");
            }
            */
            string sciezka = "wyniki.txt";

            // Zapis do pliku
            using (var writer = new StreamWriter(sciezka))
            {
                writer.WriteLine("Kategoria;LiczbaWystapien");
                foreach (var w in wyniki)
                {
                    writer.WriteLine($"{w.Kategoria};{w.LiczbaWystapien}");
                }
            }

            var statystyki = wyniki
    .GroupBy(x => x.LiczbaWystapien)
    .Select(g => new
    {
        LiczbaWystapien = g.Key,
        IleKategoriMaTyleWystapien = g.Count()
    })
    .OrderBy(x => x.LiczbaWystapien)
    .ToList();

            foreach (var w in statystyki)
            {
                Console.WriteLine($"{w.LiczbaWystapien}: {w.IleKategoriMaTyleWystapien}");
            }

            Console.WriteLine($"Zapisano wyniki do pliku: {Path.GetFullPath(sciezka)}");
        }


        public static void Awerage<T>(DbSet<T> dbSet, DbSet<Secondary_averave> dbSetout) where T : Secondary
        {
            //using var db = new MyDbContext();

            // Pobieramy właściwości liczbowo-logiczne
            var pola = typeof(Secondary)
                .GetProperties()
                .Where(p => p.PropertyType == typeof(int) ||
                            p.PropertyType == typeof(double) ||
                            p.PropertyType == typeof(float) ||
                            p.PropertyType == typeof(decimal) ||
                            p.PropertyType == typeof(bool) ||
                            p.PropertyType == typeof(Rank))
                .ToList();

            // Grupowanie po kluczu
            var grupy = dbSet.AsEnumerable()
                .GroupBy(x => x.puuid)
                .ToList();

            var sredniePomiary = new List<Secondary_averave>();

            foreach (var grupa in grupy)
            {
                var rekord = new Secondary_averave
                {
                    puuid = grupa.Key
                };

                foreach (var pole in pola)
                {
                    if (pole.PropertyType.IsEnum)
                    {
                        // znajdź najczęściej występującą wartość w grupie (mode)
                        var najczestsza = grupa
                            .Select(x => pole.GetValue(x))
                            .GroupBy(v => v)
                            .OrderByDescending(g => g.Count())
                            .First().Key;

                        var poleDocelowe = typeof(Secondary_averave).GetProperty(pole.Name);
                        if (poleDocelowe != null)
                        {
                            poleDocelowe.SetValue(rekord, najczestsza);
                        }
                    }
                    else
                    {


                        var wartosci = grupa.Select(x =>
                        {
                            var val = pole.GetValue(x);
                            if (val is bool b) return b ? 1.0 : 0.0;
                            return Convert.ToDouble(val);
                        }).ToList();

                        var srednia = wartosci.Average();

                        // ustawiamy wartość w rekordzie docelowym
                        var poleDocelowe = typeof(Secondary_averave).GetProperty(pole.Name);
                        if (poleDocelowe != null)
                        {
                            poleDocelowe.SetValue(rekord, Math.Round(srednia, 4));
                        }
                    }
                }
                
                
                sredniePomiary.Add(rekord);
            }

            // Czyścimy starą tabelę (opcjonalnie)
            dbSetout.RemoveRange(dbSetout);
            //db.SaveChanges();

            // Zapis do bazy
            dbSetout.AddRange(sredniePomiary);
            //db.SaveChanges();

            Console.WriteLine("Uśrednione dane (liczbowe i boolean) zapisane do nowej tabeli.");
        }
    }
}
