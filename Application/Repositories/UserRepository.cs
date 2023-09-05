using System.Linq.Expressions;
using Application.Repositories.Generics;
using Domain.Entities;
using Domain.Interface;
using Persistence;

namespace Application.Repositories;
public class UserRepository : GenericRepositoryWithIntId<User>, IUserRepository
{
    public UserRepository(ApiContext context) : base(context){}

    public async Task<User?> FindUserByUsername(string username)=>await FindFirst(p => p.Usename == username);
}
