namespace Api.Helpers.Paginator;
public class PageParam{
    private static readonly int _MaxRecords = 50;
    private int _PageIndex = 1;
    private int _RecordsPerPage = 5;
    public int PageIndex { 
        get => _PageIndex; 
        set => _PageIndex = (value > 0) ? value : 1; 
    }

    public int RecordsPerPage { 
        get =>_RecordsPerPage; 
        set =>_RecordsPerPage = (value > 0 && value < _MaxRecords) ? value : 1;  }
}
