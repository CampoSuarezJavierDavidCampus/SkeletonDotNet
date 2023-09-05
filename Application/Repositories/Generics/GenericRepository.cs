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
    protected readonly ApiContext _Context;
    protected readonly DbSet<T> _Entity ;
    public GenericRepository(ApiContext context){
        _Context = context;
        _Entity = _Context.Set<T>();
    }
    public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>>? expression = null){
        var records = await GetRecords(expression);
        return records;      
    }
    public virtual async Task<IPager<T>> Find(IPageParam param, Expression<Func<T, bool>>? expression = null){
        if (GetRecords(expression) is not IQueryable<T> records)
        {
            return new Pager<T>(Enumerable.Empty<T>(), param);
        }
        return await records.GetPaged(param) ;
    }
    
    public async Task<T?> FindFirst(Expression<Func<T, bool>> expression) =>  (await GetRecords(expression,true))?.First();

    public virtual void Add(T entity)=>_Entity.Add(entity);
    public virtual void AddRange(ICollection<T> entities)=>_Entity.AddRange(entities);
    public virtual void Update(T entity) => _Entity.Update(entity);
    public virtual void Remove(T entity) => _Entity.Remove(entity);
    public virtual void RemoveRange(ICollection<T> entities) => _Entity.RemoveRange(entities);

    protected virtual async Task<IEnumerable<T>> GetRecords(Expression<Func<T, bool>>? conditions = null, bool GetFist = false){
        int take = 0;
        if(GetFist)
            take = 1;

        if (conditions is not null){
            return await _Entity.Where(conditions).Take(take).ToListAsync();
        }
        return await _Entity.Take(take).ToListAsync();
    }

}
