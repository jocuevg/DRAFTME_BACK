using System.Net;

namespace DRAFTME_API.Middlewares;

public record ExceptionResponse(HttpStatusCode StatusCode, string Description);
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
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
        ExceptionResponse response = exception switch
        {
            KeyNotFoundException _ => new ExceptionResponse(HttpStatusCode.NotFound, exception.Message),
            FormatException _ => new ExceptionResponse(HttpStatusCode.UnprocessableEntity, exception.Message),
            _ => new ExceptionResponse(HttpStatusCode.InternalServerError, "Internal server error. Please retry later.")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.StatusCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}
