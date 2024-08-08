namespace AirportsDistance.Server.Interfaces
{
	public interface IIATACodeValidator
	{
		/// <summary>
		/// Validate IATA code
		/// </summary>
		/// <param name="iata">IATA code</param>
		/// <exception cref="BusinessLogicException"> Exception with validation error </exception>
		void Validate(string iata);
	}
}
