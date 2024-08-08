using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;

namespace AirportsDistance.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	[RequestTimeout("DefaultTimeout10s")]
	public abstract class BaseController : ControllerBase
	{
		protected readonly ILogger<BaseController> _logger;

		public BaseController(ILogger<BaseController> logger)
		{
			_logger = logger;
		}
	}
}
