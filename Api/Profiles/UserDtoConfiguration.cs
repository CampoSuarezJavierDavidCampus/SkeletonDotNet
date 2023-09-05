using Api.Dtos;
using AutoMapper;
using Domain.Entities;
namespace Api.Profiles;
public class UserDtoConfiguration :Profile{
    public UserDtoConfiguration(){
        CreateMap<UserDto,User>()
        .ReverseMap();
        
        CreateMap<UserXRolDto,User>()
        .ReverseMap();

        CreateMap<UserDtoWithId,User>()
        .ForMember(x => x.IdPk,opt => opt.MapFrom(src => src.Id))
        .ReverseMap();
    }
}
