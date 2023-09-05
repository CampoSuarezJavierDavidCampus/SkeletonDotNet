using Domain.Entities.Generics;

namespace Domain.Interface.Generics;
public interface IGenericRepositoryWithStringId<T>: IGenericRepository<T> where T:BaseEntityWithStrinId{
    Task<T> FindByStringId(string id);  
}
