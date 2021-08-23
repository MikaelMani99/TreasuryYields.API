using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TreasuryYields.Models.Entities;

namespace TreasuryYields.Repositories.Contexts.Interfaces
{
    public interface ITreasuryYieldsDbContext
    {
        DbSet<TreasuryYieldsDay> TreasuryYieldsDays { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}