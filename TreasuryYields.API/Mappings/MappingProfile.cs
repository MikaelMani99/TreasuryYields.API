using AutoMapper;
using TreasuryYields.Models.Entities;
using TreasuryYields.Models.DTOs;

namespace TreasuryYields.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TreasuryYieldsDay, TreasuryYieldsDayDTO>()
                    .ForMember(x => x.Country, opt => opt.MapFrom( src => src.Treasury.Seal))
                    .ForMember(x => x.Agency, opt => opt.MapFrom( src => src.Treasury.Agency))
                    .ForMember(x => x.Seal, opt => opt.MapFrom( src => src.Treasury.Seal))
                    .ForMember(x => x.Alpha2Code, opt => opt.MapFrom( src => src.Treasury.Alpha2Code))
                    .ForMember(x => x.Alpha3Code, opt => opt.MapFrom( src => src.Treasury.Alpha3Code))
                    .ForMember(x => x.Data, opt => opt.MapFrom( src => new TreasuryYieldsDayDataDto{
                        OneMonths = src.OneMonths,
                        TwoMonths = src.TwoMonths,
                        ThreeMonths = src.ThreeMonths,
                        SixMonths = src.SixMonths,
                        OneYears = src.OneYears,
                        TwoYears = src.TwoYears,
                        ThreeYears = src.ThreeYears,
                        FiveYears = src.FiveYears,
                        SevenYears = src.SevenYears,
                        TenYears = src.TenYears,
                        TwentyYears = src.TwentyYears,
                        ThirtyYears = src.ThirtyYears
                    }));

        }
    }
}