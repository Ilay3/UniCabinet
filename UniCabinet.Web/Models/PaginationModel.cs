namespace UniCabinet.Web.Models
{
    public class PaginationModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int VisiblePages { get; set; } = 5;
        public string Action { get; set; }
        public string Controller { get; set; }
        public PaginationRouteValues RouteValues { get; set; }
    }


    public class PaginationRouteValues
    {
        public string Role { get; set; }
        public int PageSize { get; set; }
        public string Query { get; set; }
    }
}
