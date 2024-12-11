using App.Application.Features.Users.Users.Dtos;
using App.Domain.Entities.Identity;
using AutoMapper;

namespace App.Application.Features.Users.Users;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserApp, UserAppDto>().ReverseMap();
    }

}
