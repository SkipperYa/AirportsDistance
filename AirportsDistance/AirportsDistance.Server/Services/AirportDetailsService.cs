using AirportsDistance.Server.Entities;
using AirportsDistance.Server.Entities.CustomException;
using AirportsDistance.Server.Interfaces;

namespace AirportsDistance.Server.Services
{
	public class AirportDetailsService : IAirportDetailsService
	{
		public static string ClientName = "AirportDetails";

		private readonly IHttpClientService _httpClientService;
		private readonly ICacheService<AirportDetails> _cacheService; 
		private readonly IHttpResponseHandler<AirportDetails> _httpResponseHandler;

		public AirportDetailsService(
			IHttpClientService httpClientService,
			ICacheService<AirportDetails> cacheService,
			IHttpResponseHandler<AirportDetails> httpResponseHandler
		)
		{
			_httpClientService = httpClientService;
			_cacheService = cacheService;
			_httpResponseHandler = httpResponseHandler;
		}

		/// <summary>
		/// Get <see cref="AirportDetails">AirportDetails</see> by IATA code
		/// </summary>
		/// <param name="iata">IATA code</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns> <see cref="AirportDetails">AirportDetails</see> </returns>
		/// <exception cref="BusinessLogicException"></exception>
		public async Task<AirportDetails> GetAsync(string iata, CancellationToken cancellationToken)
		{
			var airportDetails = _cacheService.Get(iata);

			if (airportDetails is not null)
			{
				return airportDetails;
			}

			try
			{
				var response = await _httpClientService.GetAsync(ClientName, iata.Trim().ToUpperInvariant(), cancellationToken);

				airportDetails = await _httpResponseHandler.HandleResponseAsync(response, cancellationToken);

				_cacheService.Set(iata, airportDetails);

				return airportDetails;
			}
			catch (InvalidRequestException ire)
			{
				throw new InvalidRequestException("Invalid IATA code.", ire);
			}
			catch (RemoteServiceInternalException rsie)
			{
				throw new BusinessLogicException("Remote service is Unavailable.Please try later.", rsie);
			}
			catch (UnknownBusinessLogicException uble)
			{
				throw new BusinessLogicException("Something went wrong. Please try later.", uble);
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}
