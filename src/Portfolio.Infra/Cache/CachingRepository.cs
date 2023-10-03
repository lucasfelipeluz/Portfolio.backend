using Microsoft.Extensions.Caching.Memory;

namespace Portfolio.Infra.Cache
{
  public class CachingRepository : ICachingRepository
  {
    private readonly IMemoryCache _memoryCache;

    public CachingRepository(IMemoryCache memoryCache)
    {
      _memoryCache = memoryCache;
    }

    public T Get<T>(string key)
    {
      _memoryCache.TryGetValue(key, out T value);

      return value;
    }

    public void Remove(string key)
    {
      _memoryCache.Remove(key);
    }

    public void Save<T>(string key, T value)
    {
      var memoryCacheEntryOptions = new MemoryCacheEntryOptions {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(3),
        SlidingExpiration = TimeSpan.FromDays(3),
      }; 
      

      _memoryCache.Set(key, value, memoryCacheEntryOptions);
    }
  }
}
