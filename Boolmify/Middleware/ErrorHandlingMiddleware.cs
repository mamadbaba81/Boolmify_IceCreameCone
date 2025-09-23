    using System.Net;
    using System.Text.Json;

    namespace Boolmify.Middleware;

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
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
                _logger.LogError(ex, "Unhandled exception");

                context.Response.ContentType = "application/json";

                var statusCode = ex switch
                {
                    InvalidOperationException => (int)HttpStatusCode.BadRequest,
                    ArgumentException => (int)HttpStatusCode.BadRequest,
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                context.Response.StatusCode = statusCode;

                var problem = new
                {
                    type = $"https://httpstatuses.com/{statusCode}",
                    title = statusCode == 500 ? "Internal Server Error" : ex.GetType().Name,
                    status = statusCode,
                    detail = ex.Message
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
            }
        }
    }
    