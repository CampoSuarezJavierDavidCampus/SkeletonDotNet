namespace Api.Helpers.Paginator;
public class Pager<T>{
    private readonly int _TotalPages;    
    private readonly PageParam _PageOptions;
    public Pager(IEnumerable<T> records, PageParam param){
        _PageOptions = param;
        _TotalPages = (int)Math.Ceiling(records.Count() / (decimal) param.RecordsPerPage);        
    }

    public int TotalPages => _TotalPages;
    public bool HasPreviusPage => _PageOptions.PageIndex > 1;
    public bool HasNextPage => _PageOptions.PageIndex < _TotalPages;

}
