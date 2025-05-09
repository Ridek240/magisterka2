using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magisterka2
{

    public class GameDbContext : DbContext
    {
        /*
        public DbSet<Rootobject> RootObjects { get; set; }
        public DbSet<Metadata> Metadatas { get; set; }
        public DbSet<Info> Infos { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Challenges> Challenges { get; set; }
        public DbSet<Missions> Missions { get; set; }
        public DbSet<Perks> Perks { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Selection> Selections { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Feats> Feats { get; set; }
        public DbSet<Objectives> Objectives { get; set; }
        public DbSet<Ban> Bans { get; set; }

        // Connection string in OnConfiguring or via DI (Dependency Injection) in Startup.cs
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=League.db");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Defining relationships between entities if needed (example for Participants and Info)

            modelBuilder.Entity<Info>()
                .HasMany(i => i.participants)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Participant>()
                .HasOne(p => p.challenges)
                .WithOne()
                .HasForeignKey<Challenges>(c => c.ParticipantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Participant>()
                .HasOne(p => p.missions)
                .WithOne()
                .HasForeignKey<Missions>(c => c.ParticipantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Participant>()
                .HasOne(p => p.perks)
                .WithOne()
                .HasForeignKey<Perks>(c => c.ParticipantId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Atakhan>().HasNoKey();
            modelBuilder.Entity<Baron>().HasNoKey();
            modelBuilder.Entity<Dragon>().HasNoKey();
            modelBuilder.Entity<Horde>().HasNoKey();
            modelBuilder.Entity<Inhibitor>().HasNoKey();
            modelBuilder.Entity<Riftherald>().HasNoKey();
            modelBuilder.Entity<Tower>().HasNoKey();

            // Other necessary model configuration based on your classes
        }
    }
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
        */
    }
}
