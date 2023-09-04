using System.Linq.Expressions;
using Domain.Interface;
using Application.Helpers;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repositories;
public abstract class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
{
    private readonly ApiContext _Context;
    private readonly DbSet<T> _Entity ;
    public GenericRepository(ApiContext context){
        _Context = context;
        _Entity = _Context.Set<T>();
    }
    public virtual (decimal totalPages, IEnumerable<T> Records) Find(Expression<Func<T, bool>>? expression = null){
        var records = GetRecords(expression);
        return (
            records.TotalPages(),
            records
        );        
    }

    public virtual (int CurrentIndex, IEnumerable<T> Records) Find(int pageIndex, Expression<Func<T, bool>>? expression = null){
        var records = GetRecords(expression);
        return records.GetPage(pageIndex);      
    }
    
    public T FindFirst(Expression<Func<T, bool>> expression) => GetRecords(expression,true).First();

    public virtual async void Add(T entity){
        _Entity.Add(entity);
        await SaveChanges();
    }
    public virtual async void AddRange(ICollection<T> entities){
        _Entity.AddRange(entities);
        await SaveChanges();
    }
    public virtual void Update(T entity) => _Entity.Update(entity);
    public virtual void Remove(T entity) => _Entity.Remove(entity);
    public virtual void RemoveRange(ICollection<T> entities) => _Entity.RemoveRange(entities);

    public virtual async Task<int> SaveChanges()=>await _Context.SaveChangesAsync();

    public virtual void Dispose(){
        _Context.Dispose();
        GC.SuppressFinalize(this); 
    }

    protected virtual IEnumerable<T> GetRecords(Expression<Func<T, bool>>? conditions = null, bool GetFist = false){
        int take = 0;
        if(GetFist)
            take = 1;

        if (conditions is not null){
            return _Entity.Where(conditions).Take(take).ToList<T>();
        }
        return _Entity.Take(take).ToList<T>();
    }

}
