using Microsoft.Extensions.Caching.Memory;
using PrintMaster.Application.InterfaceServices;

namespace PrintMaster.Application.ImplementServices
{
    public class BlacklistedTokenService : IBlacklistedTokenService
    {
        private readonly IMemoryCache _memoryCache;
        private const string BlacklistCacheKey = "BlacklistedTokens";

        public BlacklistedTokenService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void BlacklistToken(string token, TimeSpan expiration)
        {
            _memoryCache.Set(token, true, expiration);
        }

        public bool IsTokenBlacklisted(string token)
        {
            return _memoryCache.TryGetValue(token, out bool _);
        }
    }
}
