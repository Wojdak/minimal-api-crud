using AutoMapper;
using MinimalAPI.Models;

namespace MinimalAPI.Mappers
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "Standard"));
        }
    }
}
