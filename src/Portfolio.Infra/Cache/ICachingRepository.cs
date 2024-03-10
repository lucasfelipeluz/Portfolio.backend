using Portfolio.Core.Enums;

namespace Portfolio.Infra.Cache;

public interface ICachingRepository
{
	T Get<T>(CacheCode cacheCode);
	void Save<T>(CacheCode cacheCode, T value);
	void Remove(CacheCode cacheCode);
}
