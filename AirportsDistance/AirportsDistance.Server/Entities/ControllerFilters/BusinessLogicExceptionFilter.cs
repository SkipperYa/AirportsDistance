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
			string exceptionStack = context.Exception.StackTrace;
			string exceptionMessage = context.Exception.Message;

			var stringBuilder = new StringBuilder();

			stringBuilder.Append($"ActionName: {actionName} \n");
			stringBuilder.Append($"ExceptionStack: {exceptionStack} \n");
			stringBuilder.Append($"ExceptionMessage: {exceptionMessage} \n");

			var error = stringBuilder.ToString();

			// Send message to client if it BusinessLogicException
			if (context.Exception is BusinessLogicException logicException)
			{
				_logger.LogError(error);

				context.Result = new JsonResult(new Response.Response(logicException.Message));
			}
			// If it is TaskCanceledException set message Service Unavailable.
			else if (context.Exception is TaskCanceledException)
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
