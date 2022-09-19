using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }

        public DbSet<ProgramingLanguage> ProgramingLanguages { get; set; }
        public DbSet<Technology> Technologies { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions,IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProgramingLanguage>(x =>
            {
                x.ToTable("ProgramingLanguages").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("Id");
                x.Property(p => p.Name).HasColumnName("Name");

                x.HasMany(p => p.Technologies);
            });

            modelBuilder.Entity<Technology>(x =>
            {
                x.ToTable("Technologies").HasKey(k => k.Id);
                x.Property(p => p.Id).HasColumnName("Id");
                x.Property(p => p.Name).HasColumnName("Name");
                x.Property(p => p.ProgramingLanguageId).HasColumnName("ProgramingLanguageId");

                x.HasOne(p => p.ProgramingLanguage);
            });

            ProgramingLanguage[] languageSeeds = { new(1, "C#"), new(2, "Java"), new(3, "Python") };
            modelBuilder.Entity<ProgramingLanguage>().HasData(languageSeeds);

            Technology[] technologieSeeds = { new(1, "Django", 3), new(2, ".NET", 1), new(3, "Spring", 2) };
            modelBuilder.Entity<Technology>().HasData(technologieSeeds);
        }

    }
}
