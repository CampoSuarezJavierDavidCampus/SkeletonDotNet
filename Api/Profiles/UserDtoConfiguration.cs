using Api.Dtos;
using AutoMapper;
using Domain.Entities;
namespace Api.Profiles;
public class UserDtoConfiguration :Profile{
    public UserDtoConfiguration(){
        CreateMap<UserDto,User>()
        .ForMember(x => x.Rols, opt => opt.Ignore())
        .ReverseMap();
        
        CreateMap<UserDtoWithId,User>()
        .ForMember(x => x.Rols, opt => opt.Ignore())
        .ReverseMap();

        CreateMap<UserXRolDto,User>()
        .ForMember(x => x.IdPk, opt => opt.Ignore())
        .ForMember(x => x.Rols, opt => opt.MapFrom(src => src.Rols))
        .ReverseMap();
    }
}
