using FastEndpoints;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
 
 
public class GlobalExceptionMiddleware {
    // private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
 
    public GlobalExceptionMiddleware(
        // RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger
    ){
        // _next = next;
        _logger = logger;
    }
 
    public Task HandleAsync(Exception ex, HttpContext httpContext) {
        _logger.LogError(ex, "An unhandled exception occured");
 
        var statusCode = HttpStatusCode.InternalServerError;
        if(ex is KeyNotFoundException) {
            statusCode = HttpStatusCode.NotFound;
        }
        else if(ex is ArgumentException) {
            statusCode = HttpStatusCode.BadRequest;
        }
 
        httpContext.Response.StatusCode = (int)statusCode;
 
        return httpContext.Response.WriteAsJsonAsync(new {message = ex.Message});
    }
    // public async Task InvokeAsync() {
    //     try {
    //         await _next(context);
    //     }
    //     catch (Exception ex) {
    //         _logger.LogError(ex, "An unhandled exception occured");
    //         await HandleExceptionAsync(context, ex);
    //     }
    // }
}
