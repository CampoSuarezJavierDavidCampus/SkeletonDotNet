using System.Linq.Expressions;
using Application.Helpers.Security;
using Application.Repositories.Generics;
using Domain.Entities;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repositories;
public class RolRepository : GenericRepositoryWithIntId<Rol>, IRolRepository{
    public RolRepository(ApiContext context) : base(context){}
    public async Task<Rol?> FindByRol(Rols rol)=>await FindFirst(p => p.Description == rol.ToString());

    public override async Task<Rol?> FindFirst(Expression<Func<Rol, bool>> expression) {
        if (expression is not null){
            return await _Entity.Where(expression)
            .Include(x => x.Users)
            .Select(x => new Rol(){
                IdPk = x.IdPk,
                Description = x.Description,
                Users = x.Users.Select(j => new User(){
                    Usename = j.Usename,
                    Email = j.Email
                }).ToList()
            })
            .FirstAsync();
        }
        return await _Entity.FirstAsync();
    }

    protected override async Task<IEnumerable<Rol>> GetRecords(Expression<Func<Rol, bool>>? conditions = null){
        if (conditions is not null){
            return await _Entity.Where(conditions)
                .Include(x => x.Users)
                .Select(x => new Rol(){
                    IdPk = x.IdPk,
                    Description = x.Description,
                    Users = x.Users.Select(j => new User(){
                        Usename = j.Usename,
                        Email = j.Email
                    }).ToList()
                })
                .ToListAsync();
        }
        return await _Entity.ToListAsync();
    }


}
