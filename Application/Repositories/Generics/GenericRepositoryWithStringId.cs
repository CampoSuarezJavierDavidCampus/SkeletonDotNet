using Domain.Entities.Generics;
using Domain.Interface.Generics;
using Persistence;

namespace Application.Repositories.Generics;
public class GenericRepositoryWithStringId<T>: GenericRepository<T>, IGenericRepositoryWithStringId<T> where T : BaseEntityWithStrinId
{    
    public GenericRepositoryWithStringId(ApiContext context) : base(context){}    

    public virtual async Task<T?> FindByStringId(string id)=>await FindFirst(p => p.IdPk == id);
}
