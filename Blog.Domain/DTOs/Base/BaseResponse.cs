namespace Blog.Domain.DTOs;

public class BaseResponse<T>
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = "Operação realizada com sucesso!";
    public T? Data { get; set; }
    public List<string>? Erros { get; set; }

    public BaseResponse() { }

    public BaseResponse(T data)
    {
        Data = data;
    }

    public BaseResponse(string message, bool success = true)
    {
        Message = message;
        Success = success;
    }
}