using System;
using System.Collections.Generic;
using TreasuryYields.Models.DTOs;
using TreasuryYields.Models.Entities;
namespace TreasuryYields.Repositories.Interfaces
{
    public interface ITreasuryYieldsRepository
    {
        TreasuryYieldsDay GetTreasuryYieldsDay(Guid ID);
        TreasuryYieldsDay GetTreasuryYieldsDayByDate(DateTime date);
        IEnumerable<TreasuryYieldsDay> GetTreasuryYieldsByDateRange(DateTime dateFrom, DateTime dateTo);
    }
}