
using RouteService.Data;
using System;
using Microsoft.EntityFrameworkCore;
using RouteService.Repositories;

namespace RouteService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Controller ve Swagger servislerini ekleme
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Registering AirportService
            builder.Services.AddHttpClient("AirportService", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5265");
            });

            // Repository Injection
            builder.Services.AddScoped<IRouteRepository, RouteRepository>();


            // Veritaban� ba�lant�s� (MSSQL)
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Repository injection
            

            var app = builder.Build();

            // Geli�tirme ortam�ysa Swagger UI�� aktif et
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Yetkilendirme middleware (kullan�yorsan�z etkili olur)
            app.UseAuthorization();

            // Controller endpointlerini haritala
            app.MapControllers();

            app.Run();
        }
    }
}
