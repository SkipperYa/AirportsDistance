namespace AirportsDistance.Server.Interfaces
{
	public interface IHttpClientService
	{
		Task<HttpResponseMessage> GetAsync(string clientName, string uri, CancellationToken cancellationToken = default);
	}
}
