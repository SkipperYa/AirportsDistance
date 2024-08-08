using AirportsDistance.Server.Entities;
using AirportsDistance.Server.Interfaces;

namespace AirportsDistance.Server.Services
{
	public class AirportsDistanceService : IDistanceService
	{
		private const double _earthRadius = 6371.0710;
		private const double _coefficient = 0.621371;

		private double GetRadiance(double value) => value * Math.PI / 180.0;
		private double KilometerToMiles(double kilometers) => Math.Round(kilometers * _coefficient, 2);

		/// <summary>
		/// Calculating distance by Haversine formula.
		/// </summary>
		/// <param name="point1">Coordinate 1</param>
		/// <param name="point2">Coordinate 2</param>
		/// <returns>Distance</returns>
		public double GetDistance(Coordinate point1, Coordinate point2)
		{
			var radianLatitude1 = GetRadiance(point1.Latitude);
			var radianLatitude2 = GetRadiance(point2.Latitude);

			var deltaRadianLatitude = GetRadiance(point2.Latitude - point1.Latitude);
			var deltaRadianLongitude = GetRadiance(point2.Longitude - point1.Longitude);

			var angel = Math.Sin(deltaRadianLatitude / 2.0) * Math.Sin(deltaRadianLatitude / 2.0)
				+ Math.Cos(radianLatitude1) * Math.Cos(radianLatitude2) * Math.Sin(deltaRadianLongitude / 2.0) * Math.Sin(deltaRadianLongitude / 2.0);

			var coefficient = 2.0 * Math.Atan2(Math.Sqrt(angel), Math.Sqrt(1.0 - angel));

			return KilometerToMiles(_earthRadius * coefficient);
		}
	}
}
