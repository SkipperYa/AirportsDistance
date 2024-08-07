using AirportsDistance.Server.Entities;

namespace AirportsDistance.Server.Interfaces
{
	public interface IDistanceService
	{
		double GetDistance(Coordinate point1, Coordinate point2);
	}
}
