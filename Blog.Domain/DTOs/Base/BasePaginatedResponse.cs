namespace Blog.Domain.DTOs;

public class BasePaginatedResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = "Operação realizada com sucesso!";
    public IEnumerable<T>? Data { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalItem { get; set; }
}