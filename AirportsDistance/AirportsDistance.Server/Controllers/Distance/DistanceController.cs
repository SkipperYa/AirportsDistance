using AirportsDistance.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AirportsDistance.Server.Controllers.Distance
{
	public class DistanceController : BaseController
	{
		private readonly IAirportDetailsService _airportDetailsService;
		private readonly IDistanceService _distanceService;

		public DistanceController(ILogger<BaseController> logger, IAirportDetailsService airportDetailsService, IDistanceService distanceService) : base(logger)
		{
			_airportDetailsService = airportDetailsService;
			_distanceService = distanceService;
		}

		[HttpGet]
		public async Task<IActionResult> Get(string iata1 = "AMS", string iata2 = "SVO", CancellationToken cancellationToken = default)
		{
			var airportDetails1 = await _airportDetailsService.Get(iata1, cancellationToken);
			var airportDetails2 = await _airportDetailsService.Get(iata2, cancellationToken);

			var distance = _distanceService.GetDistance(airportDetails1.Location, airportDetails2.Location);

			return Ok(distance);
		}
	}
}
