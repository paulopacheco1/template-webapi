
using System.Net;
using Newtonsoft.Json;
using PetroTemplate.Domain.Exceptions;

namespace PetroTemplate.API.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        httpContext.Request.EnableBuffering();

        try
        {
            await _next(httpContext);
        }
        catch (RequestValidationException ex)
        {
            _logger.LogWarning(ex, $"Validation Exception: {ex.Message}");
            await HandleExceptionAsync(httpContext, ex);
        }
        catch (AppException ex)
        {
            _logger.LogWarning(ex, $"Application Exception: {ex.Message}");
            await HandleExceptionAsync(httpContext, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"***Internal Unhandled Exception*** {ex.Message}");
            await HandleExceptionAsync(httpContext);
        }
        
    }

    private static async Task HandleExceptionAsync(HttpContext context, RequestValidationException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)exception.HttpStatusCode;

        await context.Response.WriteAsync(new ValidationExceptionResponse()
        {
            StatusCode = context.Response.StatusCode,
            Type = "Validation Error",
            Message = exception.Message,
            Fields = exception.Fields,
        }.ToString());
    }

    private static async Task HandleExceptionAsync(HttpContext context, AppException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)exception.HttpStatusCode;

        await context.Response.WriteAsync(new ExceptionResponse()
        {
            StatusCode = context.Response.StatusCode,
            Type = "Application Exception",
            Message = exception.Message,
        }.ToString());
    }

    private static async Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await context.Response.WriteAsync(new ExceptionResponse()
        {
            StatusCode = context.Response.StatusCode,
            Type = "Internal Server Error",
            Message = "Ocorreu um erro interno. Por favor, tente novamente mais tarde.",
        }.ToString());
    }
}

public class ExceptionResponse
{
    [JsonProperty(Order = -99)]
    public int? StatusCode { get; set; }

    [JsonProperty(Order = -98)]
    public string? Type { get; set; }

    [JsonProperty(Order = -97)]
    public string? Message { get; set; }

    public override string ToString() => JsonConvert.SerializeObject(this);
}

public class ValidationExceptionResponse : ExceptionResponse
{
    public IEnumerable<FieldValidationErrors>? Fields { get; set; }
    public override string ToString() => JsonConvert.SerializeObject(this);
}
