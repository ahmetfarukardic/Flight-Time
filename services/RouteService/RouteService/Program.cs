
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RouteService.Data;
using RouteService.Repositories;
using System;

namespace RouteService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Route Service API",
                    Version = "v1",
                    Description = "API for calculating and managing flight routes"
                });
            });

            // Registering AirportService
            builder.Services.AddHttpClient("AirportService", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5265");
            });

            // Repository Injection
            builder.Services.AddScoped<IRouteRepository, RouteRepository>();

            //Ading Repository Injection
            builder.Services.AddScoped<IGreatCircleDistanceCalculator, DistanceCalculator>();

            

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            var app = builder.Build();

            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            
            app.MapControllers();

            app.Run();
        }
    }
}
