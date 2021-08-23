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
            // TODO add relationships
        }

        public DbSet<TreasuryYieldsDay> TreasuryYieldsDays { get; set; }
        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();
    }
}