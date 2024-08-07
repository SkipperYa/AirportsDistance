using AirportsDistance.Server.Entities;
using AirportsDistance.Server.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace AirportsDistance.Server.Services
{
	public class AirportDetailsCacheService : ICacheService<AirportDetails>
	{
		private readonly IMemoryCache _memoryCache;

		public AirportDetailsCacheService(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}

		public AirportDetails Get(string key) => _memoryCache.TryGetValue(key, out AirportDetails? entity) ? entity : null;

		public void Set(string key, AirportDetails entity) => _memoryCache.Set(key, entity);
	}
}
