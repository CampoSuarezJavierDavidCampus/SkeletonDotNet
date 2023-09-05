using System.Linq.Expressions;
using Domain.Interface.Pagination;

namespace Domain.Interface;
public interface IGenericRepository<T> where T : class{

    Task<IEnumerable<T>> Find(Expression<Func<T, bool>>? expression = null);    
    Task<IPager<T>> Find(IPageParam param, Expression<Func<T, bool>>? expression = null);  
    Task<T?> FindFirst(Expression<Func<T,bool>> expression) ;         
    void Add(T entity);
    void AddRange(ICollection<T> entities);

    void Remove(T entity);
    void RemoveRange(ICollection<T> entities);

    void Update(T entity);

}
