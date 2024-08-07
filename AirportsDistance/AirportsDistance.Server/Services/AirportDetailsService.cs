using AirportsDistance.Server.Entities;
using AirportsDistance.Server.Interfaces;

namespace AirportsDistance.Server.Services
{
	public class AirportDetailsService : IAirportDetailsService
	{
		public static string ClientName = "AirportDetails";

		private readonly IHttpClientFactory _clientFactory;

		public AirportDetailsService(IHttpClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
		}

		// Add Caching for request
		public async Task<AirportDetails> Get(string iata, CancellationToken cancellationToken)
		{
			var client = _clientFactory.CreateClient(ClientName);

			client.BaseAddress = new Uri($"{client.BaseAddress}/{iata}");

			try
			{
				var result = await client.GetFromJsonAsync<AirportDetails>($"{iata}", cancellationToken);
			}
			catch (Exception e)
			{

			}
		}
	}
}
