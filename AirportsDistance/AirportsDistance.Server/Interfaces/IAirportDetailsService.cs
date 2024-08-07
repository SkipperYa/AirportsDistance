using AirportsDistance.Server.Entities;

namespace AirportsDistance.Server.Interfaces
{
	public interface IAirportDetailsService
	{
		Task<AirportDetails> GetAsync(string iata, CancellationToken cancellationToken);
	}
}
