using System.Text.Json.Serialization;

namespace AirportsDistance.Server.Entities
{
	public class AirportDetails
	{
		[JsonPropertyName("iata")]
		public string IATA { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("city")]
		public string City { get; set; }

		[JsonPropertyName("city_iata")]
		public string CityIATA { get; set; }

		[JsonPropertyName("icao")]
		public string ICAO { get; set; }

		[JsonPropertyName("country")]
		public string Country { get; set; }

		[JsonPropertyName("country_iata")]
		public string CountryIATA { get; set; }

		[JsonPropertyName("location")]
		public Coordinate Location { get; set; }

		[JsonPropertyName("rating")]
		public double Rating { get; set; }

		[JsonPropertyName("hubs")]
		public double Hubs { get; set; }

		[JsonPropertyName("timezone_region_name")]
		public string TimeZoneRegionName { get; set; }

		[JsonPropertyName("type")]
		public string Type { get; set; }

		public AirportDetails()
		{
			Location = new Coordinate();
		}
	}
}
