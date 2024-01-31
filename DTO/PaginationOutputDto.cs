namespace DTOs
{
    public class PaginationOutputDto<T>
    {
        public PaginationOutputDto(List<T> items, int totalCount, int page, int? pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
        }
        public List<T> Items { get; }

        // all itemes count as if there is no pagination
        public int TotalCount { get; }
        public int Page { get; } = 1;
        public int? PageSize { get; } = null;
        public bool HasNextPage => (PageSize > 0) ? Page * PageSize < TotalCount : false;
        public bool HasPreviousPage => Page > 1;

    }
}
