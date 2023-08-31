using System.Linq.Expressions;

namespace Domain.Interface;
public interface IGenericRepository<T> where T : class{

    Task<(int totalRecords, IEnumerable<T> Records)> Find(Expression<Func<T,bool>>? expression = null) ;
    Task<T> FindFist(Expression<Func<T,bool>> expression) ;     
    Task<int> SaveChanges();

    void Add(T entity);
    void AddRange(ICollection<T> entities);

    void Remove(T entity);
    void RemoveRange(ICollection<T> entities);

    void Update(T entity);

}
