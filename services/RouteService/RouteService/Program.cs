
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


            // Veritabaný baðlantýsý (MSSQL)
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Repository injection
            

            var app = builder.Build();

            // Geliþtirme ortamýysa Swagger UI’ý aktif et
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Yetkilendirme middleware (kullanýyorsanýz etkili olur)
            app.UseAuthorization();

            // Controller endpointlerini haritala
            app.MapControllers();

            app.Run();
        }
    }
}
