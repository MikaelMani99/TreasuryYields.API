using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TreasuryYields.Models.Entities
{
    public record Treasury
    {
        [Key]
        public Guid ID { get; init; }
        public string Country { get; init; }
        public string Agency { get; init; }
        public Uri Seal { get; init; }
        public string Alpha2Code { get; init; }
        public string Alpha3Code { get; init; }

        // navigation properties
        public IEnumerable<TreasuryYieldsDay> TreasuryYieldsDays { get; init; }
    }
}