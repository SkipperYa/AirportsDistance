namespace AirportsDistance.Server.Interfaces
{
	public interface IHttpResponseHandler<T>
		where T : class
	{
		public Task<T> HandleResponse(HttpResponseMessage response, CancellationToken cancellationToken);
	}
}
