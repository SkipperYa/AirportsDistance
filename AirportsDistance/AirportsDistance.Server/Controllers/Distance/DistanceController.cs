using AirportsDistance.Server.Entities.Response;
using AirportsDistance.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AirportsDistance.Server.Controllers.Distance
{
	public class DistanceController : BaseController
	{
		private readonly IAirportDetailsService _airportDetailsService;
		private readonly IDistanceService _distanceService;
		private readonly IIATACodeValidator _IATACodeValidator;

		public DistanceController(
			ILogger<BaseController> logger,
			IAirportDetailsService airportDetailsService,
			IDistanceService distanceService,
			IIATACodeValidator IATACodeValidator
		) : base(logger)
		{
			_airportDetailsService = airportDetailsService;
			_distanceService = distanceService;
			_IATACodeValidator = IATACodeValidator;
		}

		/// <summary>
		/// Get distance between two airports by 2 IATA codes
		/// </summary>
		/// <param name="iata1">First IATA code</param>
		/// <param name="iata2">Second IATA code</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Ok result with Response</returns>
		[HttpGet]
		[ResponseCache(Duration = 300)]
		public async Task<IActionResult> Get(string iata1, string iata2, CancellationToken cancellationToken = default)
		{
			_IATACodeValidator.Validate(iata1);
			_IATACodeValidator.Validate(iata2);

			var airportDetails1 = await _airportDetailsService.GetAsync(iata1, cancellationToken);
			var airportDetails2 = await _airportDetailsService.GetAsync(iata2, cancellationToken);

			var distance = _distanceService.GetDistance(airportDetails1.Location, airportDetails2.Location);

			var response = new Response(new DistanceResponse()
			{
				Point1 = airportDetails1.Name,
				Point2 = airportDetails2.Name,
				Distance = distance
			});

			return Ok(response);
		}
	}
}
