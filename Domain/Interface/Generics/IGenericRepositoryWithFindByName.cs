using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Generics;

namespace Domain.Interface.Generics;
public interface IGenericRepositoryWithFindByName<T>:IGenericRepositoryWithStringId<T> where T:BaseEntityWithStrinId{
    
}
