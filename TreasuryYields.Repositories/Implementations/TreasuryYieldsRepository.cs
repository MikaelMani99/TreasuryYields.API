using System;
using System.Globalization;
using System.Linq;
using TreasuryYields.Models.Entities;
using TreasuryYields.Repositories.Contexts.Interfaces;
using TreasuryYields.Repositories.Interfaces;
using TreasuryYields.Models.Exceptions;
using System.Collections.Generic;
using TreasuryYields.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace TreasuryYields.Repositories.Implementations
{
    public class TreasuryYieldsRepository : ITreasuryYieldsRepository
    {
        private readonly ITreasuryYieldsDbContext _dbContext;

        public TreasuryYieldsRepository(ITreasuryYieldsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TreasuryYieldsDay GetTreasuryYieldsDay(Guid ID)
        {
            return _dbContext.TreasuryYieldsDays.FirstOrDefault(d => d.ID == ID);
        }

        public TreasuryYieldsDay GetTreasuryYieldsDayByDate(DateTime date)
        {
            var yieldDay = _dbContext.TreasuryYieldsDays
                                        .Include(d => d.Treasury)
                                        .FirstOrDefault(d => d.Date == date);
            if (yieldDay == null) { throw new NotFoundException("no yield this day!"); }
            return yieldDay;
        }

        public IEnumerable<TreasuryYieldsDay> GetTreasuryYieldsByDateRange(DateTime dateFrom, DateTime dateTo)
        {
            var values = _dbContext.TreasuryYieldsDays
                                        .Include(d => d.Treasury)
                                        .Where(d => d.Date >= dateFrom && d.Date <= dateTo);
            return values;
        }
    }
}