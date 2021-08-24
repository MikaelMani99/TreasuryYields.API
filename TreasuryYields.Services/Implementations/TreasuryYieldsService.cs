using TreasuryYields.Services.Interfaces;
using TreasuryYields.Repositories.Interfaces;
using TreasuryYields.Models.Entities;
using System;
using System.Collections.Generic;
using TreasuryYields.Models.DTOs;
using System.Globalization;

namespace TreasuryYields.Services.Implementations
{
    public class TreasuryYieldsService : ITreasuryYieldsService
    {
        private readonly ITreasuryYieldsRepository _tyr;
        public TreasuryYieldsService(ITreasuryYieldsRepository TYR)
        {
            _tyr = TYR;
        }
        /// <summary>
        /// Takes in a String that includes a date that can be converted to DateTime using a particular format
        /// </summary>
        /// <param name="date">date in a string format</param>
        /// <param name="format">format in a string format</param>
        /// <returns>date as a DateTime Struct</returns>
        private static DateTime ConvertStringToDate(String date, String format)
        {
            var dateFormatted = DateTime.ParseExact(
                date,
                format,
                CultureInfo.InvariantCulture
            );
            return dateFormatted;
        }

        public TreasuryYieldsDay GetTreasuryYieldsDay(Guid ID)
        {
            return _tyr.GetTreasuryYieldsDay(ID);
        }

        public TreasuryYieldsDay GetTreasuryYieldsDayByDate(String date, String format)
        {
            return _tyr.GetTreasuryYieldsDayByDate(date, format);
        }

        public IEnumerable<TreasuryYieldsDay> GetTreasuryYieldsByDateRange(String dateFrom, String dateTo, String format)
        {
            var dateFromFormatted = ConvertStringToDate(dateFrom, format);
            var dateToFormatted = ConvertStringToDate(dateTo, format);
            return  _tyr.GetTreasuryYieldsByDateRange(dateFromFormatted, dateToFormatted);
        }
    }
}