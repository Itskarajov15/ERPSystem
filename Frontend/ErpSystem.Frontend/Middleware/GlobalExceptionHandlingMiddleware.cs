using System.Net;
using System.Text.Json;
using ErpSystem.Frontend.Core.Models.Common;
using ErpSystem.Frontend.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ErpSystem.Frontend.Middleware;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
    private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

    public GlobalExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionHandlingMiddleware> logger,
        ITempDataDictionaryFactory tempDataDictionaryFactory
    )
    {
        _next = next;
        _logger = logger;
        _tempDataDictionaryFactory = tempDataDictionaryFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "HTTP Request error occurred");
            await HandleExceptionAsync(context, ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt");
            context.Response.Redirect("/Account/Login");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var localizationService =
            context.RequestServices.GetRequiredService<ILocalizationService>();
        var errorMessage = GetLocalizedErrorMessage(exception, localizationService);

        if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = GetStatusCode(exception);

            var response = new ErrorResponse
            {
                Message = errorMessage,
                StatusCode = GetStatusCode(exception),
            };

            var result = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(result);
            return;
        }

        if (exception is HttpRequestException ex && ex.StatusCode == HttpStatusCode.Forbidden)
        {
            var tempData = _tempDataDictionaryFactory.GetTempData(context);
            tempData["ErrorMessage"] = errorMessage;

            var referer = context.Request.Headers["Referer"].ToString();
            if (
                !string.IsNullOrEmpty(referer)
                && Uri.TryCreate(referer, UriKind.Absolute, out var refererUri)
            )
            {
                var redirectPath = refererUri.PathAndQuery;
                if (redirectPath != context.Request.Path)
                {
                    context.Response.Redirect(redirectPath);
                    return;
                }
            }

            context.Response.Redirect("/");
            return;
        }

        context.Response.ContentType = "text/html";
        var tempDataOther = _tempDataDictionaryFactory.GetTempData(context);
        tempDataOther["ErrorMessage"] = errorMessage;
        context.Response.Redirect("/Home/Error");
    }

    private string GetLocalizedErrorMessage(
        Exception exception,
        ILocalizationService localizationService
    )
    {
        var key = exception switch
        {
            HttpRequestException ex when ex.StatusCode == HttpStatusCode.NotFound =>
                "Resource not found",
            HttpRequestException ex when ex.StatusCode == HttpStatusCode.Unauthorized =>
                "Unauthorized access",
            HttpRequestException ex when ex.StatusCode == HttpStatusCode.Forbidden =>
                "Forbidden access",
            HttpRequestException ex when ex.StatusCode == HttpStatusCode.BadRequest =>
                "Invalid request",
            UnauthorizedAccessException => "Unauthorized access",
            _ => "An error occurred while processing your request",
        };

        return localizationService.Translate(key);
    }

    private int GetStatusCode(Exception exception)
    {
        return exception switch
        {
            HttpRequestException ex when ex.StatusCode.HasValue => (int)ex.StatusCode.Value,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError,
        };
    }
}
