using System.Threading.RateLimiting;

namespace api_reservas.Middleware
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly TokenBucketRateLimiter _rateLimiter;

        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;
            _rateLimiter = new TokenBucketRateLimiter(new TokenBucketRateLimiterOptions
            {
                TokenLimit = 100, // -- Maximum tokens in the bucket
                TokensPerPeriod = 10, // -- Tokens added per period
                ReplenishmentPeriod = TimeSpan.FromMinutes(1), // -- Replenishment period
                AutoReplenishment = true
            });
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Acquire one token (one permit) from the rate limiter
            var result = await _rateLimiter.AcquireAsync(1);

            if (result.IsAcquired)
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Too many requests. Please try again later.");
            }
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
