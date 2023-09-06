namespace Domain.Interface.Pagination;
public interface IPager<T>{    
    IEnumerable<T> Records { get; set;}
    int TotalPages {get;}
    bool HasPreviusPage {get;}
    bool HasNextPage {get;}
    IPageParam? PageOptions { get; }

}
