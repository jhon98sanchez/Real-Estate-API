namespace Common.Models
{
    public class PagedResultBase
    {
        private readonly int PageSizeMax = 200;
        private int ThisPageSize { get; set; } = 10;

        public int CurrentPage { get; set; }
        public int RowCount { get; set; }
        public int PageCount { get; set; }
        public int PageSize
        {
            get
            {
                return ThisPageSize;
            }
            set
            {
                ThisPageSize = (value > PageSizeMax) ? PageSizeMax : value;
            }
        }
        public int FirstRowOnPage => (CurrentPage - 1) * PageSize + 1;
        public int LastRowOnPage => Math.Min(CurrentPage * PageSize, RowCount);
    }

    public class PagedResult<TEntity> : PagedResultBase where TEntity : class
    {
        public List<TEntity> Results { get; set; }
        public PagedResult()
        {
            Results = [];
        }
    }

    public class PagedOneResult<TEntity>(TEntity entity) : PagedResultBase where TEntity : class
    {
        public TEntity Result { get; set; } = entity;
    }
}
