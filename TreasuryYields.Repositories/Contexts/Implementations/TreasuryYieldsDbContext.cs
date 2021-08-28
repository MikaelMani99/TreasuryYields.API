using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TreasuryYields.Models.Entities;
using TreasuryYields.Repositories.Contexts.Interfaces;

namespace TreasuryYields.Repositories.Contexts.Implementations
{
    public class TreasuryYieldsDbContext : DbContext, ITreasuryYieldsDbContext
    {
        public TreasuryYieldsDbContext(DbContextOptions<TreasuryYieldsDbContext> options) : base(options) { }
        /// <summary>
        /// Overriding the OnModelCreating method, as I wasnt to control what happens
        /// when the Model is being created, so I can map the relationship between tables
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapping relations
            modelBuilder.Entity<TreasuryYieldsDay>()
                                .HasOne(t => t.Treasury)
                                .WithMany(d => d.TreasuryYieldsDays);


            // Seeding Data
            modelBuilder.Entity<Treasury>().HasData(
              new Treasury{
                  ID = Guid.NewGuid(), 
                  Country = "United States of America", 
                  Agency = "U.S. Department of the Treasury", 
                  Seal = new Uri("https://upload.wikimedia.org/wikipedia/commons/thumb/c/cb/Seal_of_the_United_States_Department_of_the_Treasury.svg/1024px-Seal_of_the_United_States_Department_of_the_Treasury.svg.png"), 
                  Alpha2Code = "US", 
                  Alpha3Code = "USA"}
            );

        }

        public DbSet<Treasury> Treasuries { get; set; }
        public DbSet<TreasuryYieldsDay> TreasuryYieldsDays { get; set; }
        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
    }
}