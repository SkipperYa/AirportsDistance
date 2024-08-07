using AirportsDistance.Server.Entities;
using AirportsDistance.Server.Entities.CustomException;
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
		public async Task<AirportDetails> GetAsync(string iata, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(iata))
			{
				throw new BusinessLogicException("IATA code id required");
			}

			var client = _clientFactory.CreateClient(ClientName);

			client.BaseAddress = new Uri($"{client.BaseAddress}{iata}");

			try
			{
				var result = await client.GetFromJsonAsync<AirportDetails>($"{iata}", cancellationToken);

				if (result is null)
				{
					throw new BusinessLogicException("Invalid IATA code");
				}

				return result;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
