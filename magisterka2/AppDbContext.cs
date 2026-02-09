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

        public DbSet<Primary> Primary { get; set; }
        public DbSet<Secondary> Secondary { get; set; }
        public DbSet<Secondary> SecondaryAwerage { get; set; }
        public DbSet<Secondary_averave> Secondary_Awerage { get; set; }
        public DbSet<PlayerRank> PlayerRanks { get; set; }

        public DbSet<DiagnosticResult> DiagnosticResults { get; set; }

        public DbSet<ValidationResult> ValidationResults { get; set; }
        public DbSet<OutcomeResult> OutcomeResults { get; set; }
        public DbSet<ConfusionMatrixEntry> ConfusionMatrixEntries { get; set; }
        public DbSet<RocPoint> RocPoints { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //=> options.UseSqlite("Data Source=C:\\magisterka\\Bazy\\RawData.db");

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=MagisterkaDb;Trusted_Connection=True;", opts => opts.CommandTimeout(120));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Primary>()
                .HasKey(op => new { op.matchId, op.participantId });
            modelBuilder.Entity<Secondary>()
                .HasKey(op => new { op.matchId, op.puuid });
            modelBuilder.Entity<PlayerRank>()
                .HasKey(op =>  op.puuid);
            modelBuilder.Entity<Secondary_averave>()
                .HasKey(op => op.puuid);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ValidationResult>()
    .HasMany(v => v.OutcomeResults)
    .WithOne()
    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ValidationResult>()
                .HasMany(v => v.ConfusionMatrix)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ValidationResult>()
                .HasMany(v => v.RocPoints)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
