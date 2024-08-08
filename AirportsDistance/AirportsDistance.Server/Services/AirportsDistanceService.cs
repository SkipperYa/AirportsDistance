using AirportsDistance.Server.Entities;
using AirportsDistance.Server.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace AirportsDistance.Server.Services
{
	public class AirportsDistanceService : IDistanceService
	{
		private const double _earthRadius = 6371.0710;

		private double GetRadiance(double value) => value * Math.PI / 180.0;

		public double GetDistance([NotNull] Coordinate point1, [NotNull] Coordinate point2)
		{
			var f1 = GetRadiance(point1.Latitude);
			var f2 = GetRadiance(point2.Latitude);

			var deltaF = GetRadiance(point2.Latitude - point1.Latitude);
			var deltaL = GetRadiance(point2.Longitude - point1.Longitude);

			var a = Math.Sin(deltaF / 2.0) * Math.Sin(deltaF / 2.0)
				+ Math.Cos(f1) * Math.Cos(f2) * Math.Sin(deltaL / 2) * Math.Sin(deltaL / 2);

			var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

			return Math.Round(_earthRadius * c * 0.621371, 2);
		}
	}
}
