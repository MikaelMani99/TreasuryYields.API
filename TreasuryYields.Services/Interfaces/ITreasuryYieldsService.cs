using System;
using System.Collections.Generic;
using TreasuryYields.Models.DTOs;
using TreasuryYields.Models.Entities;

namespace TreasuryYields.Services.Interfaces
{
    public interface ITreasuryYieldsService
    {
        TreasuryYieldsDayDTO GetTreasuryYieldsDay(Guid ID);
        TreasuryYieldsDayDTO GetTreasuryYieldsDayByDate(String date, String format);
        IEnumerable<TreasuryYieldsDayDTO> GetTreasuryYieldsByDateRange(String dateFrom, String dateTo, String format);
    }
}