namespace BasicSupermarket.Domain.Dto;

public record ErrorResponseDto
{
    public bool Success => false;
    private string Message { get; set; }

    public ErrorResponseDto(String message)
    {
        Message = String.Empty;
        
        if (!String.IsNullOrEmpty(message))
        {
            Message = message;
        }
    }
}