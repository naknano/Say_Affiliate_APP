namespace WebApplication1.Data.DTO;
public class ResponseDto<T>
{
    public int responseCode { get; set; }
    public string responseMessage { get; set; } = string.Empty;
    public T? data { get; set; }
    public List<T>? dataList { get; set; }

}