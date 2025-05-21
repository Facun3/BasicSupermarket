namespace BasicSupermarket.Domain.Exceptions;

public class DomainException: Exception
{
    public string Code { get; }
    public int StatusCode { get; }

    public DomainException(string message, string code = "DOMAIN_ERROR", int statusCode = 400): base(message)
    {
        Code = code;
        StatusCode = statusCode;
    }
}