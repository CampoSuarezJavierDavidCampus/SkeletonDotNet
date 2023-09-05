using Domain.Entities.Generics;

namespace Domain.Interface.Generics;
public interface IGenericRepositoryWithIntId<T>: IGenericRepository<T> where T:BaseEntityWithIntId{
    Task<T> FindByIntId(int id);  
}
