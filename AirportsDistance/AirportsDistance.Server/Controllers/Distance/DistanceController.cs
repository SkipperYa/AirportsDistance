using Microsoft.AspNetCore.Mvc;

namespace AirportsDistance.Server.Controllers.Distance
{
	public class DistanceController : BaseController
	{
		public DistanceController(ILogger<BaseController> logger) : base(logger)
		{
		}

		[HttpGet]
		public ActionResult<double> Get(double latitude1, double longitude1, double latitude2, double longitude2)
		{
			return Ok(15.0);
		}
	}
}
