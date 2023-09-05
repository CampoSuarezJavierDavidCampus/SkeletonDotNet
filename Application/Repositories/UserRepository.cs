using System.Linq.Expressions;
using Application.Repositories.Generics;
using Domain.Entities;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repositories;
public class UserRepository : GenericRepositoryWithIntId<User>, IUserRepository
{
    public UserRepository(ApiContext context) : base(context){}

    public async Task<User?> FindUserByUsername(string username)=>await FindFirst(p => p.Usename == username);

    protected override async Task<IEnumerable<User>> GetRecords(Expression<Func<User, bool>>? conditions = null, bool GetFist = false){
        int take = 0;
        if(GetFist)
            take = 1;

        if (conditions is not null){
            return await _Entity.Where(conditions).Take(take).Include(
                x =>x.RolUsers.ToList().Select(
                    j =>_Context.Rols.Where(
                        x => x.IdPk == j.RolIdPk
                    ).Select(
                        x => x.Description
                    )
                )
            ).ToListAsync();
        }
        return await _Entity.Take(take).ToListAsync();
    }
}
