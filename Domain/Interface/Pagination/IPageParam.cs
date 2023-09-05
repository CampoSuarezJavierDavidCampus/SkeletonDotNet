namespace Domain.Interface.Pagination;
public interface IPageParam{    
    public int PageIndex { get; set; }
    public int RecordsPerPage { get; set; }
}
