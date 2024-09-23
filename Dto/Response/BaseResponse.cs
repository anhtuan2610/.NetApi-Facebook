namespace Facebook_be.Dto.Response;

public class BaseResponse<T>
{
    public String code { get; set; }
    public String message { get; set; }
    public T data { get; set; }
}