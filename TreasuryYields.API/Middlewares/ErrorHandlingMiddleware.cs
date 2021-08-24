using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using TreasuryYields.Models.Exceptions;

namespace TreasuryYields.API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        public ErrorHandlingMiddleware(RequestDelegate next, IHostEnvironment env)
        {
            _next = next;
            _env = env;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _env);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception, IHostEnvironment env)
        {
            HttpStatusCode status;
            string message;
            var stackTrace = exception.StackTrace;
            var exceptionType = exception.GetType();
            if (exceptionType == typeof(NotFoundException))
            {
                message = exception.Message;
                status = HttpStatusCode.NotFound;
            }
            else
            {
                status = HttpStatusCode.InternalServerError;
                message = "🤡 Oopsie the server fucked up 🤡";
            }
            var result = JsonConvert.SerializeObject(new { errors = new { Message = new string[] { message } } });
            // if the server is being run locally / set to in development
            // the stacktrace will follow the error message in the request
            if (env.IsDevelopment())
            {
                result = JsonConvert.SerializeObject(new { errors = new { Message = new string[] { message } }, stacktrace = stackTrace });
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            return context.Response.WriteAsync(result.ToString());
        }
    }
}