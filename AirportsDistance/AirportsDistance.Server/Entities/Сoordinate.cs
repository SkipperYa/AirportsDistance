using System.Text.Json.Serialization;

namespace AirportsDistance.Server.Entities
{
	public class Coordinate
	{
		[JsonPropertyName("lat")]
		public double Latitude { get; set; }

		[JsonPropertyName("lon")]
		public double Longitude { get; set; }
	}
}
