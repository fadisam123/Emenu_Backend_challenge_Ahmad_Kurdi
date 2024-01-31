using Microsoft.AspNetCore.Mvc;

namespace DTOs
{
    public class PaginationInputDto
    {
        [FromQuery]
        public string? Filter { get; set; } = null;
        [FromQuery]
        public string? SortColumn { get; set; } = null;
        [FromQuery]
        public string SortOrder { get; set; } = "desc";
        [FromQuery]
        public int Page { get; set; } = 1;
        [FromQuery]
        public int? PageSize { get; set; } = null;
    }
}
