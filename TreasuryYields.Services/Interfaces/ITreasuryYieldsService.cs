using System;
using System.Threading.Tasks;
using TreasuryYields.Models;
using TreasuryYields.Models.Entities;

namespace TreasuryYields.Services.Interfaces
{
    public interface ITreasuryYieldsService
    {
        TreasuryYieldsDay GetTreasuryYieldsDay(Guid ID);
        TreasuryYieldsDay GetTreasuryYieldsDayByDate(String date, String format);
    }
}