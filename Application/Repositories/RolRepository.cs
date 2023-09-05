using Application.Helpers.Security;
using Application.Repositories.Generics;
using Domain.Entities;
using Domain.Interface;
using Persistence;

namespace Application.Repositories;
public class RolRepository : GenericRepositoryWithIntId<Rol>, IRolRepository{
    public RolRepository(ApiContext context) : base(context){}
    public Rol? FindByRol(Rols rol)=>FindFirst(p => p.Description == rol.ToString());
}
