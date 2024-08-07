using AirportsDistance.Server.Entities;

namespace AirportsDistance.Server.Interfaces
{
	public interface IAirportDetailsService
	{
		Task<AirportDetails> Get(string iata, CancellationToken cancellationToken);
	}
}
