using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger; 
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,       
            IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
            
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var options = new JsonSerializerOptions(){PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
                var response = _env.IsDevelopment()? JsonSerializer.Serialize(new ApiException(context.Response.StatusCode, ex.Message,
                 ex.StackTrace.ToString()), options):
                    JsonSerializer.Serialize(new ApiResponse(context.Response.StatusCode), options);               
                await context.Response.WriteAsync(response);
            }
        }        
    }
}