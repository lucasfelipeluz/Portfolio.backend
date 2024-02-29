using Portfolio.Core.Enums;
using Microsoft.Extensions.Caching.Memory;

namespace Portfolio.Infra.Cache;

public class CachingRepository : ICachingRepository
{
	private readonly IMemoryCache _memoryCache;

	public CachingRepository(IMemoryCache memoryCache)
	{
		_memoryCache = memoryCache;
	}

	public T Get<T>(CacheCode cacheCode)
	{
		_memoryCache.TryGetValue(cacheCode, out T value);

		return value;
	}

	public void Remove(CacheCode cacheCode)
	{
		_memoryCache.Remove(cacheCode);
	}

	public void Save<T>(CacheCode cacheCode, T value)
	{
		var memoryCacheEntryOptions = new MemoryCacheEntryOptions
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(3),
			SlidingExpiration = TimeSpan.FromDays(3),
		};

		_memoryCache.Set(cacheCode, value, memoryCacheEntryOptions);
	}
}
