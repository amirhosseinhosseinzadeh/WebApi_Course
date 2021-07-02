using AutoMapper;
using HotelListing.Models;
using HotelListing.Domains;

namespace HotelListing.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer() 
        {
            CreateMap<Country, CountryModel>().ReverseMap();
            CreateMap<Country, CreateCountryModel>().ReverseMap();
            CreateMap<Hotel, HotelModel>().ReverseMap();
            CreateMap<Hotel, CreateHotelModel>().ReverseMap();
        }
    }
}