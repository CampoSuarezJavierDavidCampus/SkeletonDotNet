using Api.Dtos;
using AutoMapper;
using Domain.Entities;
namespace Api.Profiles;
public class RolDtoConfiguration: Profile{
    public RolDtoConfiguration(){
        CreateMap<RolDto,Rol>()
            .ForMember(x => x.Users, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<RolDtoWithId,Rol>()
            .ForMember(x => x.Users, opt => opt.Ignore())
            .ForMember(x => x.IdPk, opt => opt.MapFrom(src => src.Id))
            .ReverseMap();

        CreateMap<RolXUserDtos,Rol>()
            .ForMember(x => x.IdPk, opt => opt.Ignore())
            .ForMember(x => x.IdPk, opt => opt.MapFrom(src => src.Id))
            .ForMember(x => x.Users, opt => opt.MapFrom(src => src.Users))
            .ReverseMap();
    }
}
