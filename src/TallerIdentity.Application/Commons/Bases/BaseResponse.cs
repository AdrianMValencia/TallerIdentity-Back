namespace TallerIdentity.Application.Commons.Bases;

public class BaseResponse<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
    public int? TotalRecords { get; set; }
    public IEnumerable<BaseError>? Errors { get; set; }
}
