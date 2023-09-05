using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Generics;
using Domain.Interface.Generics;
using Persistence;

namespace Application.Repositories.Generics;
public class GenericRepositoryWithStringId<T>: GenericRepository<T>, IGenericRepositoryWithStringId<T> where T : BaseEntityWithStrinId
{    
    public GenericRepositoryWithStringId(ApiContext context) : base(context){}    

    public virtual T FindByStringId(string id)=>FindFirst(p => p.IdPk == id);
}
