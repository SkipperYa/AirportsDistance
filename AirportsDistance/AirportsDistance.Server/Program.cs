using AirportsDistance.Server.Entities;
using AirportsDistance.Server.Entities.ControllerFilters;
using AirportsDistance.Server.Interfaces;
using AirportsDistance.Server.Services;
using Microsoft.AspNetCore.Http.Timeouts;

namespace AirportsDistance.Server
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Logging.ClearProviders();
			builder.Logging.AddConsole();

			// Add services to the container.

			builder.Services.AddControllers(options =>
			{
				options.Filters.Add(typeof(BusinessLogicExceptionFilter));
			});

			builder.Services.AddRequestTimeouts(options =>
			{
				options.AddPolicy("DefaultTimeout10s", new RequestTimeoutPolicy()
				{
					Timeout = TimeSpan.FromMilliseconds(10000),
				});
			});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddTransient<IDistanceService, AirportsDistanceService>();
			builder.Services.AddTransient<IAirportDetailsService, AirportDetailsService>();
			builder.Services.AddSingleton<ICacheService<AirportDetails>, AirportDetailsCacheService>();
			builder.Services.AddSingleton<IIATACodeValidator, IATACodeValidator>();

			builder.Services.AddMemoryCache();

			builder.Services.AddHttpClient(AirportDetailsService.ClientName, client =>
			{
				client.BaseAddress = new Uri("https://places-dev.cteleport.com/airports/");
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.MapControllers();

			app.MapFallbackToFile("/index.html");

			app.Run();
		}
	}
}
