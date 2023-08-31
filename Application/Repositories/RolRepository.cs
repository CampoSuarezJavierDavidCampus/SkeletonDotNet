using System.Linq.Expressions;
using Domain.Entities;
using Domain.Interface;
using Persistence;

namespace Application.Repositories;
public class RolRepository : GenericRepository<Rol>, IRolRepository{
    public RolRepository(ApiContext context) : base(context){}
}
