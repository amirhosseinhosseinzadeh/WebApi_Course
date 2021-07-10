using Microsoft.Extensions.Caching.Memory;

namespace HotelListing.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        
    }
}