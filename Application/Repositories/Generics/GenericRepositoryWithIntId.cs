using Domain.Entities.Generics;
using Domain.Interface.Generics;
using Persistence;

namespace Application.Repositories.Generics;
public class GenericRepositoryWithIntId<T> : GenericRepository<T>, IGenericRepositoryWithIntId<T> where T : BaseEntityWithIntId
{
    public GenericRepositoryWithIntId(ApiContext context) : base(context){}
    public virtual async Task<T> FindByIntId(int id)=>await FindFirst(p => p.IdPk == id);
}
