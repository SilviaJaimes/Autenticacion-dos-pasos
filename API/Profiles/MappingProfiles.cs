using API.Dtos;
using AutoMapper;
using Dominio.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, RegisterDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, VerifyDto>().ReverseMap();
    }
}