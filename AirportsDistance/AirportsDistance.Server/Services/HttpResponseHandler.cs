using AirportsDistance.Server.Entities.CustomException;
using AirportsDistance.Server.Interfaces;
using System.Net;

namespace AirportsDistance.Server.Services
{
	public class HttpResponseHandler<T> : IHttpResponseHandler<T>
		where T : class, new()
	{
		protected virtual void HandleNotSuccessStatusCode(HttpResponseMessage response)
		{
			switch (response.StatusCode)
			{
				case HttpStatusCode.NotFound:
				{
					throw new InvalidRequestException(response.ReasonPhrase);
				}
				case HttpStatusCode.BadRequest:
				{
					throw new InvalidRequestException(response.ReasonPhrase);
				}
				case HttpStatusCode.ServiceUnavailable:
				{
					throw new RemoteServiceInternalException(response.ReasonPhrase);
				}
				case HttpStatusCode.InternalServerError:
				{
					throw new RemoteServiceInternalException(response.ReasonPhrase);
				}
				default:
				{
					throw new UnknownBusinessLogicException(response.ReasonPhrase);
				}
			}
		}

		protected virtual async Task<T> HandleSuccessStatusCode(HttpResponseMessage response, CancellationToken cancellationToken)
		{
			return await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
		}

		/// <summary>
		/// Handle response from HTTP request
		/// </summary>
		/// <param name="response">HttpResponseMessage</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns></returns>
		public async Task<T> HandleResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken)
		{
			try
			{
				if (!response.IsSuccessStatusCode)
				{
					HandleNotSuccessStatusCode(response);
				}

				return await HandleSuccessStatusCode(response, cancellationToken);
			}
			catch (Exception e)
			{
				throw;
			}
		}
	}
}
