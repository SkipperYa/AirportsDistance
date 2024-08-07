using Microsoft.AspNetCore.Mvc;

namespace AirportsDistance.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public abstract class BaseController : ControllerBase
	{
		private readonly ILogger<BaseController> _logger;

		public BaseController(ILogger<BaseController> logger)
		{
			_logger = logger;
		}
	}
}
