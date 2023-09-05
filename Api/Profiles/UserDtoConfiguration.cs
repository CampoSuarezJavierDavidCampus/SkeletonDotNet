using Api.Dtos;
using AutoMapper;
using Domain.Entities;
namespace Api.Profiles;
public class UserDtoConfiguration :Profile{
    UserDtoConfiguration(){
        CreateMap<UserXRolDto,User>()
        .ReverseMap();
    }
}
