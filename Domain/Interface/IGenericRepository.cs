using System.Linq.Expressions;

namespace Domain.Interface;
public interface IGenericRepository<T> where T : class{

    (decimal totalPages, IEnumerable<T> Records) Find(Expression<Func<T, bool>>? expression = null);
    (int CurrentIndex, IEnumerable<T> Records) Find(int pageIndex, Expression<Func<T, bool>>? expression = null);
    T FindFirst(Expression<Func<T,bool>> expression) ;     
    Task<int> SaveChanges();

    void Add(T entity);
    void AddRange(ICollection<T> entities);

    void Remove(T entity);
    void RemoveRange(ICollection<T> entities);

    void Update(T entity);

}
