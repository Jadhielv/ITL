using ITL.Domain.Common;
using ITL.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ITL.API.Middlewares;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
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

        if (context.Response.StatusCode == (int)HttpStatusCode.InternalServerError)
        {
            _logger.LogError(exception, "An internal server error occurred: {Message}", exception.Message);
        }

        var errorMessage = exception.InnerException?.Message ?? exception.Message;
        var jsonMessage = JsonConvert.SerializeObject(new { message = errorMessage });
        await context.Response.WriteAsync(jsonMessage);
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
