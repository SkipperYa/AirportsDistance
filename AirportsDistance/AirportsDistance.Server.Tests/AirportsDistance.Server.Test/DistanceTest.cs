using AirportsDistance.Server.Controllers.Distance;
using AirportsDistance.Server.Entities;
using AirportsDistance.Server.Entities.CustomException;
using AirportsDistance.Server.Entities.Response;
using AirportsDistance.Server.Interfaces;
using AirportsDistance.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace AirportsDistance.Server.Test
{
	public class DistanceTest
	{
		private AirportDetails GetDetailsSVO()
		{
			return new AirportDetails()
			{
				IATA = "SVO",
				Name = "Sheremetyevo",
				Location = new Coordinate()
				{
					Latitude = 55.966324,
					Longitude = 37.416574,
				}
			};
		}

		private AirportDetails GetDetailsEWR()
		{
			return new AirportDetails()
			{
				IATA = "EWR",
				Name = "Newark",
				Location = new Coordinate()
				{
					Latitude = 40.689071,
					Longitude = -74.178753,
				}
			};
		}

		[Fact]
		public async Task GetCorrectDistance()
		{
			var mockLogger = new Mock<ILogger<DistanceController>>();

			var mockAirportDetailsService = new Mock<IAirportDetailsService>();

			mockAirportDetailsService.Setup(q => q.GetAsync("SVO", default)).Returns(Task.FromResult(GetDetailsSVO()));
			mockAirportDetailsService.Setup(q => q.GetAsync("EWR", default)).Returns(Task.FromResult(GetDetailsEWR()));

			// Arrange
			var distanceController = new DistanceController(mockLogger.Object, mockAirportDetailsService.Object, new AirportsDistanceService(), new IATACodeValidator());

			// Act
			var result = (await distanceController.Get("SVO", "EWR")) as OkObjectResult;

			// Assert
			Assert.NotNull(result);

			var data = result.Value as Response;
			Assert.NotNull(data);
			Assert.True(data.Success);

			var distanceResponse = data.Data as DistanceResponse;
			Assert.NotNull(distanceResponse);

			Assert.Equal(4657.84, distanceResponse.Distance);
		}

		[Fact]
		public async Task GetDistanceWithEmptyIATA()
		{
			var mockLogger = new Mock<ILogger<DistanceController>>();

			var mockDistanceService = new Mock<IDistanceService>();

			var mockAirportDetailsService = new Mock<IAirportDetailsService>();

			mockAirportDetailsService.Setup(q => q.GetAsync("SVO", default)).Returns(Task.FromResult(GetDetailsSVO()));
			mockAirportDetailsService.Setup(q => q.GetAsync("", default)).Returns(Task.FromResult(new AirportDetails()));

			// Arrange
			var distanceController = new DistanceController(mockLogger.Object, mockAirportDetailsService.Object, new AirportsDistanceService(), new IATACodeValidator());

			// Act
			var result = await Assert.ThrowsAsync<BusinessLogicException>(async () => await distanceController.Get("SVO", ""));

			// Assert
			Assert.NotNull(result);
			Assert.Equal("IATA code id required", result.Message);
		}

		[Fact]
		public async Task GetDistanceWithNumbersIATA()
		{
			var mockLogger = new Mock<ILogger<DistanceController>>();

			var mockDistanceService = new Mock<IDistanceService>();

			var mockAirportDetailsService = new Mock<IAirportDetailsService>();

			mockAirportDetailsService.Setup(q => q.GetAsync("SVO", default)).Returns(Task.FromResult(GetDetailsSVO()));
			mockAirportDetailsService.Setup(q => q.GetAsync("123", default)).Returns(Task.FromResult(new AirportDetails()));

			var mockIATACodeValidator = new Mock<IIATACodeValidator>();

			// Arrange
			var distanceController = new DistanceController(mockLogger.Object, mockAirportDetailsService.Object, new AirportsDistanceService(), new IATACodeValidator());

			// Act
			var result = await Assert.ThrowsAsync<BusinessLogicException>(async () => await distanceController.Get("SVO", "123"));

			// Assert
			Assert.NotNull(result);
			Assert.Equal("IATA code must contains only letters", result.Message);
		}

		[Fact]
		public async Task GetDistanceWithLargeIATA()
		{
			var mockLogger = new Mock<ILogger<DistanceController>>();

			var mockAirportDetailsService = new Mock<IAirportDetailsService>();

			mockAirportDetailsService.Setup(q => q.GetAsync("SVO", default)).Returns(Task.FromResult(GetDetailsSVO()));
			mockAirportDetailsService.Setup(q => q.GetAsync("EWRWRWRWR", default)).Returns(Task.FromResult(new AirportDetails()));

			// Arrange
			var distanceController = new DistanceController(mockLogger.Object, mockAirportDetailsService.Object, new AirportsDistanceService(), new IATACodeValidator());

			// Act
			var result = await Assert.ThrowsAsync<BusinessLogicException>(async () => await distanceController.Get("SVO", "EWRWRWRWR"));

			// Assert
			Assert.NotNull(result);
			Assert.Equal("IATA code must contains only 3-letter", result.Message);
		}
	}
}