using AutoMapper;
using TreasuryYields.Models.Entities;
using TreasuryYields.Models.DTOs;

namespace TreasuryYields.API.Mappings
{
    public class MappingProfile: Profile
    {
        public MappingProfile(){
            CreateMap<TreasuryYieldsDay,TreasuryYieldsDayDTO>();
        }
    }
}