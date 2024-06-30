using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware;

public class ExceptionsMiddleware
{
    public RequestDelegate _next;
    public ILogger<ExceptionsMiddleware> _logger;
    public IHostEnvironment _env;
  public ExceptionsMiddleware(RequestDelegate next, ILogger<ExceptionsMiddleware> logger, 
  IHostEnvironment env)
  {
        _next = next;
        _logger = logger;
        _env = env;
  }

  public async Task InvokeAsync(HttpContext context)
  {
     try
     {
         await _next(context);
     }
     catch (System.Exception ex)
     {
        _logger.LogError(ex, ex.Message );
        context.Response.ContentType = "application/json";
        context.Response.StatusCode =(int) HttpStatusCode.InternalServerError;      
        
        var response = _env.IsDevelopment()
        ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
        : new ApiException(context.Response.StatusCode, ex.Message,"Internal Server Error");
      
        var jsonOptions =new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
        var json = JsonSerializer.Serialize(response, jsonOptions);

        await context.Response.WriteAsync(json);  
     
     }

  }


}
