namespace Application.Helpers;
public static class Paginator{
    private static readonly int _RecordsPerPage = 20;
    public static decimal TotalPages<T>(this IEnumerable<T> records){        
        return Math.Ceiling((decimal)records.Count() / _RecordsPerPage);
    }

    public static (int currentPageIndex, IEnumerable<T> records) GetPage<T>(this IEnumerable<T> records, int pageIndex = 1){
        decimal totalPages = records.TotalPages();
        if (totalPages <= 0 || pageIndex > totalPages || pageIndex < 1){
            return (0,Enumerable.Empty<T>());
        }
        return (++pageIndex , records
                .Skip(_RecordsPerPage * --pageIndex )
                .Take(_RecordsPerPage)
                .ToList());
    }
}
