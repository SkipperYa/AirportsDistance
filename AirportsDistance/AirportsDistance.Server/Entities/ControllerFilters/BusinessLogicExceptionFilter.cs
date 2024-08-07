﻿using Microsoft.AspNetCore.Mvc.Filters;

namespace AirportsDistance.Server.Entities.ControllerFilters
{
	/// <summary>
	/// Global filter for Controllers
	/// </summary>
	public class BusinessLogicExceptionFilter : Attribute, IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			// throw new NotImplementedException();
		}
	}
}
