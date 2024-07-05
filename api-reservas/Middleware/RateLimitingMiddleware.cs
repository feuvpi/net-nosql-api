using System.Threading.RateLimiting;

namespace api_reservas.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ClientRateLimiter _rateLimiter;
        private readonly ILogger<RateLimitingMiddleware> _logger;

        public RateLimitingMiddleware(RequestDelegate next, ClientRateLimiter rateLimiter, ILogger<RateLimitingMiddleware> logger)
        {
            _next = next;
            _rateLimiter = rateLimiter;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var result = await _rateLimiter.AcquireAsync(clientIp, 1);

            if (result.IsAcquired)
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;

                context.Response.OnStarting(() =>
                {
                    if (!context.Response.HasStarted)
                    {
                        // Extrai a duração recomendada para Retry-After
                        var retryAfterDuration = GetRetryAfterDuration(result);
                        context.Response.Headers.RetryAfter = retryAfterDuration.ToString();
                    }

                    return Task.CompletedTask;
                });

                _logger.LogWarning($"Rate limit exceeded for IP address: {clientIp}");
                await context.Response.WriteAsync("Too many requests. Please try again later.");
            }
        }

        private TimeSpan GetRetryAfterDuration(RateLimitLease lease)
        {
            var metadata = lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter) ? retryAfter : TimeSpan.Zero;
            return metadata;
        }
    }

    public static class RateLimitingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRateLimitingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RateLimitingMiddleware>();
        }
    }
}
