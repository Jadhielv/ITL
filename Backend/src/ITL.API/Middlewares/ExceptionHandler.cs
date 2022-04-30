using ITL.Domain.Common;
using ITL.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ITL.API.Middlewares;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;

    public ExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)GetStatusCode(exception);

        // TODO: Log InternalServerError Exceptions

        var errorMessage = exception.InnerException?.Message ?? exception.Message;

        // TODO: Use Json converter
        var jsonMesage = $"{{\"message\": \"{errorMessage}\"}}";

        await context.Response.WriteAsync(jsonMesage);
    }

    public HttpStatusCode GetStatusCode(Exception exception)
    {
        var internalException = exception as BaseException;
        if (internalException == null)
        {
            return HttpStatusCode.InternalServerError;
        }
        return internalException.StatusCode;
    }
}
