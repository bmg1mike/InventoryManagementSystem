namespace Domain;

public class PaginationResponse<T>
{
    public PaginatedList<T> Data { get; set; }
    public int CurrentPage { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
}