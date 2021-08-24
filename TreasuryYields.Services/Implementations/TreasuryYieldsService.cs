using TreasuryYields.Services.Interfaces;
using TreasuryYields.Repositories.Interfaces;
using TreasuryYields.Models.Entities;
using System;
using System.Collections.Generic;
using TreasuryYields.Models.DTOs;
using System.Globalization;
using AutoMapper;

namespace TreasuryYields.Services.Implementations
{
    public class TreasuryYieldsService : ITreasuryYieldsService
    {
        private readonly ITreasuryYieldsRepository _tyr;
        private readonly IMapper _mapper;
        public TreasuryYieldsService(ITreasuryYieldsRepository TYR, IMapper mapper)
        {
            _tyr = TYR;
            _mapper = mapper;
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

        public TreasuryYieldsDayDTO GetTreasuryYieldsDay(Guid ID)
        {
            return _mapper.Map<TreasuryYieldsDayDTO>(_tyr.GetTreasuryYieldsDay(ID));
        }

        public TreasuryYieldsDayDTO GetTreasuryYieldsDayByDate(String date, String format)
        {
            var dateformatted = ConvertStringToDate(date, format);
            return _mapper.Map<TreasuryYieldsDayDTO>(_tyr.GetTreasuryYieldsDayByDate(dateformatted));
        }

        public IEnumerable<TreasuryYieldsDayDTO> GetTreasuryYieldsByDateRange(String dateFrom, String dateTo, String format)
        {
            var dateFromFormatted = ConvertStringToDate(dateFrom, format);
            var dateToFormatted = ConvertStringToDate(dateTo, format);
            return  _mapper.Map<IEnumerable<TreasuryYieldsDayDTO>>(_tyr.GetTreasuryYieldsByDateRange(dateFromFormatted, dateToFormatted));
        }
    }
}