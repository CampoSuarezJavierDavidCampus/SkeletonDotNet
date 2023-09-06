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

     public override async Task<User?> FindFirst(Expression<Func<User, bool>> expression) {
        if (expression is not null){
            return await _Entity.Where(expression)
            .Include(x => x.Rols)
            .Select(x => new User(){
                IdPk = x.IdPk,
                Usename = x.Usename,
                Email = x.Email,
                Rols = x.Rols.Select(j => new Rol(){
                    Description = j.Description                    
                }).ToList()
            })
            .FirstAsync();
        }
        return await _Entity.FirstAsync();
    }

    protected override async Task<IEnumerable<User>> GetRecords(Expression<Func<User, bool>>? conditions = null){
        if (conditions is not null){
            return await _Entity.Where(conditions)
                .Include(x => x.Rols)
                .Select(x => new User(){
                    IdPk = x.IdPk,
                    Usename = x.Usename,
                    Email = x.Email,
                    Rols = x.Rols.Select(j => new Rol(){
                        Description = j.Description                    
                    }).ToList()
                })
                .ToListAsync();
        }
        return await _Entity.ToListAsync();
    }
}
