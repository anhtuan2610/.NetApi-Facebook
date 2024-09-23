namespace WebCoffe.Dto.Request;

public class GetTotalPageRequest
{
    public int PageSize { get; set; } = 10;
    public string? SearchString { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}