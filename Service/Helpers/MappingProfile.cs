using AutoMapper;
using Domain.Entities;
using Service.DTOs.Admin.Cities;
using Service.DTOs.Admin.Countries;

namespace Service.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CountryCreateDto, Country>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryEditDto, Country>();

            CreateMap<City, CityDto>().ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));
            CreateMap<CityCreateDto, City>();
        }
    }
}
