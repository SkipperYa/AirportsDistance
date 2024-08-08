using AirportsDistance.Server.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace AirportsDistance.Server.Services
{
	public class BaseCacheService<T> : ICacheService<T>
		where T : class
	{
		private readonly IMemoryCache _memoryCache;

		public BaseCacheService(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}

		public T Get(string key)
		{
			if (_memoryCache.TryGetValue<T>(key, out var value))
			{
				return value;
			}

			return null;
		}

		public void Set(string key, T entity)
		{
			_memoryCache.Set(key, entity);
		}
	}
}
