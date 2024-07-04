using System.Collections.Concurrent;
using System.Threading.RateLimiting;

namespace api_reservas.Middleware
{
    public class ClientRateLimiter
    {
        private readonly ConcurrentDictionary<string, TokenBucketRateLimiter> _rateLimiters;
        private readonly TokenBucketRateLimiterOptions _options;

        public ClientRateLimiter(TokenBucketRateLimiterOptions options)
        {
            _options = options;
            _rateLimiters = new ConcurrentDictionary<string, TokenBucketRateLimiter>();
        }

        public async ValueTask<RateLimitLease> AcquireAsync(string clientIp, int permits)
        {
            var rateLimiter = _rateLimiters.GetOrAdd(clientIp, _ => new TokenBucketRateLimiter(_options));
            return await rateLimiter.AcquireAsync(permits);
        }
    }
}
