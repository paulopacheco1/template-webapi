namespace Template.API.Middlewares;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
                => app.UseMiddleware<ExceptionHandlerMiddleware>();
}
