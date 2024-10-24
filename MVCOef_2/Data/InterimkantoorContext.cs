using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Interimkantoor.Data
{
    public class InterimkantoorContext : IdentityDbContext<CustomUser>
    {
        public InterimkantoorContext(DbContextOptions<InterimkantoorContext> options) : base(options)
        {
        }

        public DbSet<Klant> Klanten { get; set; } = default!;
        public DbSet<Job> Jobs { get; set; } = default!;

        public DbSet<KlantJob> KlantJobs { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Klant>().ToTable("Klant");
            modelBuilder.Entity<Job>().ToTable("Job");
            modelBuilder.Entity<KlantJob>().ToTable("KlantJob");

            modelBuilder.Entity<KlantJob>()
                .HasOne(p => p.Klant)
                .WithMany(x => x.KlantJobs)
                .HasForeignKey(y => y.KlantId)
                .OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            modelBuilder.Entity<KlantJob>()
                .HasOne(p => p.Job)
                .WithMany(x => x.KlantJobs)
                .HasForeignKey(y => y.JobId)
                .OnDelete(deleteBehavior: DeleteBehavior.Restrict);

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Job>().HasData(
                new List<Job>()
                {
                    new Job()
                    {
                        Id = 1,
                        AantalPlaatsen = 5,
                        EindDatum = DateTime.Now.AddDays(1),
                        StartDatum = DateTime.Now,
                        IsBadge = true,
                        IsKleding = false,
                        IsWerkschoenen = false,
                        Locatie = "Turnhout",
                        Omschrijving = "Leraar positie"
                    },
                    new Job()
                    {
                        Id = 2,
                        AantalPlaatsen = 1,
                        EindDatum = DateTime.Now.AddDays(7),
                        StartDatum = DateTime.Now,
                        IsBadge = true,
                        IsKleding = true,
                        IsWerkschoenen = true,
                        Locatie = "Lier",
                        Omschrijving = "Security guard"
                    },
                });

            modelbuilder.Entity<Klant>().HasData(
                new List<Klant>
                {
                    new Klant
                    {
                         Id = "1",
                         Bankrekeningnummer = "123",
                         Gemeente = "Turnhout",
                         Huisnummer = "51",
                         Naam = "De magazijnier",
                         Voornaam = "Jos",
                         Postcode = "3540",
                         Straat = "Steenweg"
                    },
                    new Klant
                    {
                         Id = "2",
                         Bankrekeningnummer = "wadfe",
                         Gemeente = "Lier",
                         Huisnummer = "51",
                         Naam = "De businessman",
                         Voornaam = "Frank",
                         Postcode = "3540",
                         Straat = "Steenweg"
                    }
                });

            modelbuilder.Entity<KlantJob>().HasData(
                new List<KlantJob>
                {
                    new KlantJob
                    {
                        Id = 1,
                        JobId = 2,
                        KlantId = "1",
                    }
                });
        }
    }
}