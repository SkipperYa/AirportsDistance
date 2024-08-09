using AirportsDistance.Server.Interfaces;

namespace AirportsDistance.Server.Services
{
	public class HttpClientService : IHttpClientService
	{
		private readonly IHttpClientFactory _clientFactory;

		public HttpClientService(IHttpClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
		}

		/// <summary>
		/// HTTP Get request
		/// </summary>
		/// <param name="clientName">Name of Named HTTP client</param>
		/// <param name="uri">Additional params</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns></returns>
		public async Task<HttpResponseMessage> GetAsync(string clientName, string uri, CancellationToken cancellationToken = default)
		{
			try
			{
				var client = _clientFactory.CreateClient(clientName);

				var response = await client.GetAsync($"{client.BaseAddress}{uri}", cancellationToken);

				return response;
			}
			catch (Exception e)
			{
				throw;
			}
		}
	}
}
