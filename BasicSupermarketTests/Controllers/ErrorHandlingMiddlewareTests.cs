using BasicSupermarket.Controllers;
using BasicSupermarket.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;

namespace BasicSupermarketTests.Controllers;

public class ErrorHandlingMiddlewareTests
{
    private readonly Mock<ILogger<ErrorHandlingMiddleware>> _loggerMock;
    private readonly ErrorHandlingMiddleware _middleware;

    public ErrorHandlingMiddlewareTests()
    {
        _loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
        _middleware = new ErrorHandlingMiddleware(
            next: (innerHttpContext) => throw new Exception("Test exception"),
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Invoke_WithDomainException_ReturnsDomainExceptionResponse()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        var domainException = new DomainException("Test domain error", "DOMAIN_ERROR", 400);
        var middleware = new ErrorHandlingMiddleware(
            next: (innerHttpContext) => throw domainException,
            _loggerMock.Object
        );

        // Act
        await middleware.Invoke(context);

        // Assert
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(context.Response.Body);
        var responseBody = await reader.ReadToEndAsync();
        Assert.Contains("Test domain error", responseBody);
        Assert.Equal(400, context.Response.StatusCode);
    }

    [Fact]
    public async Task Invoke_WithGenericException_ReturnsGenericErrorResponse()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        var middleware = new ErrorHandlingMiddleware(
            next: (innerHttpContext) => throw new Exception("Test generic error"),
            _loggerMock.Object
        );

        // Act
        await middleware.Invoke(context);

        // Assert
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var reader = new StreamReader(context.Response.Body);
        var responseBody = await reader.ReadToEndAsync();
        Assert.Contains("An unhandled exception occurred", responseBody);
        Assert.Equal(500, context.Response.StatusCode);
    }
} 