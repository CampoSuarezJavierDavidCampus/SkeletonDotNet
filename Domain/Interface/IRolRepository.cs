using Application.Helpers.Security;
using Domain.Entities;
using Domain.Interface.Generics;

namespace Domain.Interface;
public interface IRolRepository:IGenericRepositoryWithIntId<Rol>{
    Task<Rol?> FindByRol(Rols description);
}
