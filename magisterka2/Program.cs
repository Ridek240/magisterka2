using magisterka2;
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        //using var db = new AppDbContext();
        //db.Database.EnsureCreated();

        //db.People.Add(new Person { Name = "Janusz" });
        //db.SaveChanges();

        //var people = db.People.ToList();
        //foreach (var p in people)
        //{
        //    Console.WriteLine($"ID: {p.Id}, Name: {p.Name}");
        //}
        AddMatch addMatch = new AddMatch();
        addMatch.AddRootObjectAsync();
        while(true) { }
    }


}