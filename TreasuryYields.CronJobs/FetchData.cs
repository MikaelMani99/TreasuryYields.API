using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using TreasuryYields.Models.Entities;
using TreasuryYields.Repositories.Contexts.Interfaces;

namespace TreasuryYields.CronJobs
{
    public class FetchData
    {
        private readonly ITreasuryYieldsDbContext _dbContext;

        public FetchData(ITreasuryYieldsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Converts a dom element from the web scraping with, and checks if it's a double 
        /// if it's a double returns a double with the same value, else it returns null
        /// </summary>
        /// <param name="element">Dom element with the InnterHTML string containing a double</param>
        /// <returns>Double / Null</returns>
        private Double? ConvertToDouble(AngleSharp.Dom.IElement element)
        {
            // The format from the website is "123.22" which would by default
            // convert to 12322.00 but we want to have 123.22 so we must add 
            // a provider to the Convert.ToDouble, changeing the Numerical Decimal 
            // Sepirator.
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";

            return element.InnerHtml.ToString().Trim() == "N/A"
                ? null
                : Convert.ToDouble(element.InnerHtml.ToString(), provider);
        }

        /// <summary>
        /// Converts a dom element from the web scraping with the format MM/dd/yy 
        /// into a DateTime object.
        /// </summary>
        /// <param name="element">Dom element with InnerHTML string containing a date on the format MM/dd/yy</param>
        /// <returns>DateTime object</returns>
        private DateTime ConvertToDate(AngleSharp.Dom.IElement element)
        {
            return DateTime.ParseExact(
                element.InnerHtml.ToString(),
                "MM/dd/yy",
                CultureInfo.InvariantCulture
            );
        }
        /// <summary>
        /// Creates a new TreasuryYieldDay from the row
        /// </summary>
        /// <param name="row">AngleSharp.Dom.IElement, containing a row from the table</param>
        /// <returns>TreasuryYieldsDay Entity</returns>
        private TreasuryYieldsDay GetDayFromRow(AngleSharp.Dom.IElement row)
        {
            var treasury = _dbContext.Treasuries.FirstOrDefault(x => x.Country == "United States of America");
            return new()
            {
                ID = Guid.NewGuid(),
                Treasury = treasury,
                Date = ConvertToDate(row.Children[0]),
                OneMonths = ConvertToDouble(row.Children[1]),
                TwoMonths = ConvertToDouble(row.Children[2]),
                ThreeMonths = ConvertToDouble(row.Children[3]),
                SixMonths = ConvertToDouble(row.Children[4]),
                OneYears = ConvertToDouble(row.Children[5]),
                TwoYears = ConvertToDouble(row.Children[6]),
                ThreeYears = ConvertToDouble(row.Children[7]),
                FiveYears = ConvertToDouble(row.Children[8]),
                SevenYears = ConvertToDouble(row.Children[9]),
                TenYears = ConvertToDouble(row.Children[10]),
                TwentyYears = ConvertToDouble(row.Children[11]),
                ThirtyYears = ConvertToDouble(row.Children[12]),
            };
        }

        /// <summary>
        /// Task that Fetches Updates the databse with the Treasury Yield Days
        /// contained in the url given. 
        /// </summary>
        /// <param name="url">URL to the Treasury Yield website</param>
        /// <returns>Nothing</returns>
        public async Task Get(string url)
        {
            // Load default configuration
            var config = Configuration.Default.WithDefaultLoader();
            // Create a new browsing context
            var context = BrowsingContext.New(config);
            // returns <IDocument> that is queryable 
            var document = await context.OpenAsync(url);
            var table = document.QuerySelectorAll(".oddrow,.evenrow");

            foreach (var row in table)
            {
                var day = GetDayFromRow(row);
                var exists = _dbContext.TreasuryYieldsDays.FirstOrDefault(x => x.Date == day.Date);
                if (exists == null)
                {
                    _dbContext.TreasuryYieldsDays.Add(day);
                }
            }
            _dbContext.SaveChanges();
        }
    }
}