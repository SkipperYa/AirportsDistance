using AirportsDistance.Server.Entities;

namespace AirportsDistance.Server.Interfaces
{
	public interface IDistanceService
	{
		/// <summary>
		/// Calculating distance by Haversine formula.
		/// </summary>
		/// <param name="point1">Coordinate 1</param>
		/// <param name="point2">Coordinate 2</param>
		/// <returns>Distance</returns>
		double GetDistance(Coordinate point1, Coordinate point2);
	}
}
