using Api.Helpers.Paginator;
using Microsoft.EntityFrameworkCore;

namespace Application.Helpers;
public static class PagerExtensions{    
    public static (int currentPageIndex, IEnumerable<T> records) GetPage<T>(this DbSet<T> entity, Pager<T> opt){        
        if (opt <= )
        {
            
        }

        /* if (totalPages <= 0 || pageIndex > totalPages || pageIndex < 1){
            return (0,Enumerable.Empty<T>());
        }
        return (++pageIndex , records
                .Skip(_RecordsPerPage * --pageIndex )
                .Take(_RecordsPerPage)
                .ToList()); */
    }
}
