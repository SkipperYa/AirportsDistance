using AirportsDistance.Server.Entities;

namespace AirportsDistance.Server.Interfaces
{
	public interface IAirportDetailsService
	{
		/// <summary>
		/// Get <see cref="AirportDetails">AirportDetails</see> by IATA code
		/// </summary>
		/// <param name="iata">IATA code</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns> <see cref="AirportDetails">AirportDetails</see> </returns>
		/// <exception cref="BusinessLogicException"></exception>
		Task<AirportDetails> GetAsync(string iata, CancellationToken cancellationToken);
	}
}
