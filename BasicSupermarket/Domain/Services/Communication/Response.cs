namespace BasicSupermarket.Domain.Services.Communication;

public class Response<T>
{
    public bool Success { get; init; }
    public string? Message { get; init; }
    public T? Resource { get; init; }

    public Response(T resource)
    {
        Success = true;
        Message = null;
        Resource = resource;
    }

    public Response(string message)
    {
        Success = false;
        Message = message;
        Resource = default;
    }
    
    public static Response<T> SuccessResponse(T resource) => new Response<T>(resource);

    public static Response<T> FailureResponse(string message) => new Response<T>(message);

}