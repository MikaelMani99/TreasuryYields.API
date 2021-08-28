using System;
namespace TreasuryYields.Models.DTOs
{
    public record TreasuryYieldsDayDTO
    {
        public DateTime Date { get; init; }
        public string Country { get; init; }
        public string Agency { get; init; }
        public Uri Seal { get; init; }
        public string Alpha2Code { get; init; }
        public string Alpha3Code { get; init; }
        public TreasuryYieldsDayDataDto Data { get; init; }
        
    }
}