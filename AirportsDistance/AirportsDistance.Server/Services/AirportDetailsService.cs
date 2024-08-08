using AirportsDistance.Server.Entities;
using AirportsDistance.Server.Entities.CustomException;
using AirportsDistance.Server.Interfaces;
using System.Net;
using System.Text.RegularExpressions;

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
			var airportDetails = _cacheService.Get(iata);

			if (airportDetails is not null)
			{
				return airportDetails;
			}

			var client = _clientFactory.CreateClient(ClientName);

			client.BaseAddress = new Uri($"{client.BaseAddress}{iata}");

			try
			{
				var response = await client.GetAsync($"{iata}", cancellationToken);

				if (response.IsSuccessStatusCode)
				{
					airportDetails = await response.Content.ReadFromJsonAsync<AirportDetails>(cancellationToken: cancellationToken);
				}
				else
				{
					switch (response.StatusCode)
					{
						case HttpStatusCode.NotFound:
						{
							throw new BusinessLogicException("Invalid IATA code");
						}
						case HttpStatusCode.BadRequest:
						{
							throw new BusinessLogicException("Invalid IATA code");
						}
						case HttpStatusCode.ServiceUnavailable:
						{
							throw new BusinessLogicException("Remote service is Unavailable. Please try later");
						}
						case HttpStatusCode.InternalServerError:
						{
							throw new BusinessLogicException("Internal server error in remote service. Please try later");
						}
						default:
						{
							throw new BusinessLogicException("Something went wrong. Please try later");
						}
					}
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
