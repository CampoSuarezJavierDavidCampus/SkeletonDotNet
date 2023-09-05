using Application.Helpers.Security;
using Application.Repositories.Generics;
using Domain.Entities;
using Domain.Interface;
using Persistence;

namespace Application.Repositories;
public class RolRepository : GenericRepositoryWithIntId<Rol>, IRolRepository{
    public RolRepository(ApiContext context) : base(context){}
    public async Task<Rol?> FindByRol(Rols rol)=>await FindFirst(p => p.Description == rol.ToString());
}
