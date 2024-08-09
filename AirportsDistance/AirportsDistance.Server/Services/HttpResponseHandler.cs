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
