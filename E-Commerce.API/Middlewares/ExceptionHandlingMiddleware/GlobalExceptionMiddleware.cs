using E_Commerce.Application.Common.Exceptions.Base;
using E_Commerce.Domain.Common.Exceptions.@base;

namespace E_Commerce.API.Middleware;

public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        context.Response.ContentType = "application/json";



        var statusCode = exception switch
        {
            DomainException => StatusCodes.Status400BadRequest,

            ConflictException => StatusCodes.Status409Conflict,

            NotFoundException => StatusCodes.Status404NotFound,

            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,

            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.StatusCode = statusCode;

        var response = new
        {
            status = statusCode,

            code = exception is DomainException domainException
                ? domainException.Code
                : "internal.server.error",

            message = exception.Message
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}