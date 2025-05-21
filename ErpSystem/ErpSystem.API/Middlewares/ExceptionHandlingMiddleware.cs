using System.Text.Json;
using ErpSystem.Application.Common.Exceptions;

namespace ErpSystem.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IWebHostEnvironment _environment;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        IWebHostEnvironment environment
    )
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An unhandled exception has occurred");

        var statusCode = StatusCodes.Status500InternalServerError;
        object response = new
        {
            status = statusCode,
            message = _environment.IsDevelopment()
                ? exception.Message
                : "An error occurred while processing your request.",
        };

        switch (exception)
        {
            case ValidationException validationEx:
                statusCode = StatusCodes.Status400BadRequest;
                response = new
                {
                    status = statusCode,
                    message = "Validation failed",
                    errors = validationEx.Errors,
                };
                break;

            case NotFoundException notFoundEx:
                statusCode = StatusCodes.Status404NotFound;
                response = new { status = statusCode, message = notFoundEx.Message };
                break;

            case UnauthorizedAccessException unauthorizedEx:
                statusCode = StatusCodes.Status401Unauthorized;
                response = new { status = statusCode, message = unauthorizedEx.Message };
                break;

            case InvalidOperationException invalidOpEx:
                statusCode = StatusCodes.Status400BadRequest;
                response = new { status = statusCode, message = invalidOpEx.Message };
                break;

            case ArgumentException argEx:
                statusCode = StatusCodes.Status400BadRequest;
                response = new { status = statusCode, message = argEx.Message };
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        var json = JsonSerializer.Serialize(response, jsonOptions);
        await context.Response.WriteAsync(json);
    }
}
