using System;
using TreasuryYields.Models.Entities;
namespace TreasuryYields.Repositories.Interfaces
{
    public interface ITreasuryYieldsRepository
    {
        TreasuryYieldsDay GetTreasuryYieldsDay(Guid ID);
        TreasuryYieldsDay GetTreasuryYieldsDayByDate(String date, String format);
    }
}