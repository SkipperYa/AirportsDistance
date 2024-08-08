using AirportsDistance.Server.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace AirportsDistance.Server.Services
{
	public class AirportDetailsCacheService : BaseCacheService<AirportDetails>
	{
		public AirportDetailsCacheService(IMemoryCache memoryCache) : base(memoryCache)
		{
		}
	}
}
