using AirportsDistance.Server.Entities.ControllerFilters;
using AirportsDistance.Server.Interfaces;
using AirportsDistance.Server.Services;

namespace AirportsDistance.Server
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers(options =>
			{
				options.Filters.Add(typeof(BusinessLogicExceptionFilter));
			});

			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddTransient<IDistanceService, AirportsDistanceService>();
			builder.Services.AddTransient<IAirportDetailsService, AirportDetailsService>();

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
