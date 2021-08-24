using TreasuryYields.Services.Interfaces;
using TreasuryYields.Repositories.Interfaces;
using TreasuryYields.Models.Entities;
using System;

namespace TreasuryYields.Services.Implementations
{
    public class TreasuryYieldsService : ITreasuryYieldsService
    {
        private ITreasuryYieldsRepository _tyr;
        public TreasuryYieldsService(ITreasuryYieldsRepository TYR)
        {
            _tyr = TYR;
        }

        public TreasuryYieldsDay GetTreasuryYieldsDay(Guid ID)
        {
            return _tyr.GetTreasuryYieldsDay(ID);
        }

        public TreasuryYieldsDay GetTreasuryYieldsDayByDate(String date, String format)
        {
            return _tyr.GetTreasuryYieldsDayByDate(date, format);
        }
    }
}