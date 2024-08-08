using AirportsDistance.Server.Entities.CustomException;
using AirportsDistance.Server.Entities;
using AirportsDistance.Server.Interfaces;
using System.Net;

namespace AirportsDistance.Server.Services
{
	public class HttpResponseHandler<T> : IHttpResponseHandler<T>
		where T : class
	{
		public async Task<T> HandleResponse(HttpResponseMessage response, CancellationToken cancellationToken)
		{
			if (response.IsSuccessStatusCode)
			{
				return await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
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
		}
	}
}
