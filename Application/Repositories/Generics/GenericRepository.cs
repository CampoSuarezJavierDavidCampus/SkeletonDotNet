using System.Linq.Expressions;
using Domain.Interface;
using Application.Helpers;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Api.Helpers.Paginator;
using Domain.Interface.Pagination;

namespace Application.Repositories;
public abstract class GenericRepository<T>: IGenericRepository<T>  where T : class
{
    private readonly ApiContext _Context;
    private readonly DbSet<T> _Entity ;
    public GenericRepository(ApiContext context){
        _Context = context;
        _Entity = _Context.Set<T>();
    }
    public virtual IEnumerable<T> Find(Expression<Func<T, bool>>? expression = null){
        var records = GetRecords(expression);
        return records;      
    }
    public virtual async Task<IPager<T>> Find(IPageParam param, Expression<Func<T, bool>>? expression = null){
        if (GetRecords(expression) is not IQueryable<T> records)
        {
            return new Pager<T>(Enumerable.Empty<T>(), param);
        }
        return await records.GetPaged(param) ;
    }
    
    public T FindFirst(Expression<Func<T, bool>> expression) => GetRecords(expression,true).First();

    public virtual void Add(T entity)=>_Entity.Add(entity);
    public virtual void AddRange(ICollection<T> entities)=>_Entity.AddRange(entities);
    public virtual void Update(T entity) => _Entity.Update(entity);
    public virtual void Remove(T entity) => _Entity.Remove(entity);
    public virtual void RemoveRange(ICollection<T> entities) => _Entity.RemoveRange(entities);

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
