namespace BasicSupermarket.Domain.Communication;

public class Response<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Resource { get; set; }

    public Response(T resource)
    {
        Success = true;
        Message = null;
        Resource = resource;
    }

    public Response(String message)
    {
        Success = false;
        Message = message;
        Resource = default;
    }
}