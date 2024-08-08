using AirportsDistance.Server.Entities;
using AirportsDistance.Server.Entities.CustomException;
using AirportsDistance.Server.Interfaces;

namespace AirportsDistance.Server.Services
{
	public class AirportDetailsService : IAirportDetailsService
	{
		public static string ClientName = "AirportDetails";

		private readonly IHttpClientFactory _clientFactory;
		private readonly ICacheService<AirportDetails> _cacheService;

		public AirportDetailsService(IHttpClientFactory clientFactory, ICacheService<AirportDetails> cacheService)
		{
			_clientFactory = clientFactory;
			_cacheService = cacheService;
		}

		public async Task<AirportDetails> GetAsync(string iata, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(iata))
			{
				throw new BusinessLogicException("IATA code id required");
			}

			if (iata.Length != 3)
			{
				throw new BusinessLogicException("IATA code must contains only 3-letter");
			}

			if (!Regex.IsMatch(iata, @"^[a-zA-Z]+$"))
			{
				throw new BusinessLogicException("IATA code must contains only letters");
			}

			var airportDetails = _cacheService.Get(iata);

			if (airportDetails is not null)
			{
				return airportDetails;
			}

			var client = _clientFactory.CreateClient(ClientName);

			client.BaseAddress = new Uri($"{client.BaseAddress}{iata}");

			try
			{
				airportDetails = await client.GetFromJsonAsync<AirportDetails>($"{iata}", cancellationToken);

				if (airportDetails is null)
				{
					throw new BusinessLogicException("Invalid IATA code");
				}

				_cacheService.Set(iata, airportDetails);

				return airportDetails;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
