using System.Linq.Expressions;
using Domain.Entities;
using Domain.Interface;
using Persistence;

namespace Application.Repositories;
public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApiContext context) : base(context){}

    public User FindUserByUsername(string username){
        return FindFirst(p => p.Usename == username);
    }
}
