using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CAPGEMINI_CROPDEAL.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
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
                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            // Map common exception types to HTTP status codes
            var (statusCode, title) = ex switch
            {
                ArgumentNullException or ArgumentException
                    => ((int)HttpStatusCode.BadRequest, "Bad Request"),

                UnauthorizedAccessException
                    => ((int)HttpStatusCode.Unauthorized, "Unauthorized"),

                // Common Identity errors can be treated as 400
                IdentityException
                    => ((int)HttpStatusCode.BadRequest, "Identity Error"),

                KeyNotFoundException
                    => ((int)HttpStatusCode.NotFound, "Not Found"),

                InvalidOperationException
                    => ((int)HttpStatusCode.Conflict, "Conflict"),

                // fallback
                _ => ((int)HttpStatusCode.InternalServerError, "Internal Server Error")
            };

            context.Response.StatusCode = statusCode;

            var response = new
            {
                title,
                statusCode,
                message = ex.Message
            };

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }

    // Optional: custom exception you can throw from services for Identity-style errors
    public sealed class IdentityException : Exception
    {
        public IdentityException(string message) : base(message) { }
    }
}