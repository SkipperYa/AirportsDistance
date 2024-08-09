namespace AirportsDistance.Server.Interfaces
{
	public interface IHttpResponseHandler<T>
		where T : class
	{
		/// <summary>
		/// Handle response from HTTP request
		/// </summary>
		/// <param name="response">HttpResponseMessage</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns></returns>
		public Task<T> HandleResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken);
	}
}
