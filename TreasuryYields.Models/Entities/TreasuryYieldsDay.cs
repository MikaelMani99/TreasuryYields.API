using System;
using System.ComponentModel.DataAnnotations;

namespace TreasuryYields.Models.Entities
{
    public record TreasuryYieldsDay
    {
        [Key]
        public Guid ID { get; init; }
        public DateTime Date { get; init; }
        public double? OneMonths { get; init; }
        public double? TwoMonths { get; init; }
        public double? ThreeMonths { get; init; }
        public double? SixMonths { get; init; }
        public double? OneYears { get; init; }
        public double? TwoYears { get; init; }
        public double? ThreeYears { get; init; }
        public double? FiveYears { get; init; }
        public double? SevenYears { get; init; }
        public double? TenYears { get; init; }
        public double? TwentyYears { get; init; }
        public double? ThirtyYears { get; init; }
    }
}