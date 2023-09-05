using Application.Helpers.Security;
using Domain.Entities;
using Domain.Interface.Generics;

namespace Domain.Interface;
public interface IRolRepository:IGenericRepositoryWithIntId<Rol>{
    Rol? FindByRol(Rols description);
}
