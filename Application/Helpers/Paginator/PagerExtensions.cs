using Api.Helpers.Paginator;
using Domain.Interface.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Application.Helpers;
public static class PagerExtensions{    
    public static async Task<IPager<T>> GetPaged<T>(this IQueryable<T> query, IPageParam opt) where T: class {
        IEnumerable<T> response = await  query
                        .Skip((opt.PageIndex - 1) * opt.RecordsPerPage)
                        .Take(opt.RecordsPerPage)
                        .ToListAsync();
        return new Pager<T>(response, opt);
    }    
}
