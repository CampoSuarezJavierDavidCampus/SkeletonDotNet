namespace Domain.Interface.Pagination;
public interface IPager<T>{    
    public IEnumerable<T> Records { get; set;}
    public int TotalPages {get;}
    public bool HasPreviusPage {get;}
    public bool HasNextPage {get;}
}
