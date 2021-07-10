using AutoMapper;
using HotelListing.Models;
using HotelListing.Domains;
using HotelListing.Data;
using Microsoft.AspNetCore.Identity;

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
            CreateMap<ApiUser, UserModel>().ReverseMap();
            CreateMap<IdentityRole, RoleModel>().ReverseMap();
            CreateMap<IdentityRole,CreateRoleModel>().ReverseMap();
        }
    }
}