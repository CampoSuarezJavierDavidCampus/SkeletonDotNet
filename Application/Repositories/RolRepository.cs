using System.Linq.Expressions;
using Application.Repositories.Generics;
using Domain.Entities;
using Domain.Interface;
using Persistence;

namespace Application.Repositories;
public class RolRepository : GenericRepositoryWithIntId<Rol>, IRolRepository{
    public RolRepository(ApiContext context) : base(context){}
}
