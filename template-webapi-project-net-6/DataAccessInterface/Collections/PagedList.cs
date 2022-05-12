namespace DataAccessInterface.Collections;
public class PagedList<T> where T : class
{
     public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Elements { get; set; } = new List<T>();
        public bool HasPreviousPage => this.PageIndex > 1;
        public bool HasNextPage => this.PageIndex < this.TotalPages;
}