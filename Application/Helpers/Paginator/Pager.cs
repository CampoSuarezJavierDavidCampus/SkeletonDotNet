using Domain.Interface.Pagination;

namespace Api.Helpers.Paginator;
public class Pager<T>: IPager<T>{
        private readonly int _TotalPages;    
        private readonly IPageParam _PageOptions;
        private IEnumerable<T> _Records;
        public Pager(IEnumerable<T> records, IPageParam param){
            _PageOptions = param;
            _TotalPages = (int)Math.Ceiling(records.Count() / (decimal) param.RecordsPerPage);        
            _Records = records;
        }
        public IEnumerable<T> Records { 
            get => _Records; 
            set => _Records = value; 
        }
        public int TotalPages => _TotalPages;
        public bool HasPreviusPage => _PageOptions.PageIndex > 1;
        public bool HasNextPage => _PageOptions.PageIndex < _TotalPages;

}
