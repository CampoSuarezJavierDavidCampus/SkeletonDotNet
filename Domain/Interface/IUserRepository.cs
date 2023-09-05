using Domain.Entities;
using Domain.Interface.Generics;

namespace Domain.Interface;
public interface IUserRepository: IGenericRepositoryWithIntId<User>{
    Task<User?> FindUserByUsername(string username);
}
