using AirportsDistance.Server.Controllers;
using AirportsDistance.Server.Entities.CustomException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace AirportsDistance.Server.Entities.ControllerFilters
{
	/// <summary>
	/// Global filter for Controllers
	/// </summary>
	public class BusinessLogicExceptionFilter : Attribute, IExceptionFilter
	{
		protected readonly ILogger<BaseController> _logger;

		public BusinessLogicExceptionFilter(ILogger<BaseController> logger)
		{
			_logger = logger;
		}

		public void OnException(ExceptionContext context)
		{
			string actionName = context.ActionDescriptor.DisplayName;

			var exception = context.Exception;

			var stringBuilder = new StringBuilder();

			stringBuilder.Append($"ActionName: {actionName} \n");

			stringBuilder.Append($"ExceptionStackTrace: {exception.StackTrace} \n");
			stringBuilder.Append($"ExceptionMessage: {exception.Message} \n");
			stringBuilder.Append('\n');

			if (exception.InnerException != null)
			{
				stringBuilder.Append($"ExceptionStackTrace: {exception.InnerException.StackTrace} \n");
				stringBuilder.Append($"ExceptionMessage: {exception.InnerException.Message} \n");
				stringBuilder.Append('\n');
			}

			var error = stringBuilder.ToString();

			// Send message to client if it BusinessLogicException
			if (exception is BusinessLogicException logicException)
			{
				_logger.LogError(error);

				context.Result = new JsonResult(new Response.Response(logicException.Message));
			}
			// If it is TaskCanceledException set message Service Unavailable.
			else if (exception is TaskCanceledException)
			{
				_logger.LogWarning(error);

				context.Result = new JsonResult(new Response.Response("Service Unavailable."));
			}
			// Otherwise set message Something went wrong
			else
			{
				_logger.LogCritical(error);

				context.Result = new JsonResult(new Response.Response("Internal server error."));
			}
		}
	}
}
