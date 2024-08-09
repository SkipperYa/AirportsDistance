namespace AirportsDistance.Server.Interfaces
{
	public interface IHttpClientService
	{
		/// <summary>
		/// HTTP Get request
		/// </summary>
		/// <param name="clientName">Name of Named HTTP client</param>
		/// <param name="uri">Additional params</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns></returns>
		Task<HttpResponseMessage> GetAsync(string clientName, string uri, CancellationToken cancellationToken = default);
	}
}
