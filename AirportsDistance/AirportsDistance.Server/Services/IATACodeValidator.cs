using AirportsDistance.Server.Entities.CustomException;
using AirportsDistance.Server.Interfaces;
using System.Text.RegularExpressions;

namespace AirportsDistance.Server.Services
{
	public class IATACodeValidator : IIATACodeValidator
	{
		/// <summary>
		/// Validate IATA code
		/// </summary>
		/// <param name="iata">IATA code</param>
		/// <exception cref="BusinessLogicException"> Exception with validation error </exception>
		public void Validate(string iata)
		{
			if (string.IsNullOrEmpty(iata))
			{
				throw new BusinessLogicException("IATA code id required");
			}

			if (iata.Length != 3)
			{
				throw new BusinessLogicException("IATA code must contains only 3-letter");
			}

			if (!Regex.IsMatch(iata, @"^[a-zA-Z]+$"))
			{
				throw new BusinessLogicException("IATA code must contains only letters");
			}
		}
	}
}
