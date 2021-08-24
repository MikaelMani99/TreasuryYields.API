using System;
using System.Collections.Generic;
using TreasuryYields.Models.DTOs;
using TreasuryYields.Models.Entities;

namespace TreasuryYields.Services.Interfaces
{
    public interface ITreasuryYieldsService
    {
        TreasuryYieldsDay GetTreasuryYieldsDay(Guid ID);
        TreasuryYieldsDay GetTreasuryYieldsDayByDate(String date, String format);
        IEnumerable<TreasuryYieldsDay> GetTreasuryYieldsByDateRange(String dateFrom, String dateTo, String format);
    }
}