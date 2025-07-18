using AircraftService.Data;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// DbContext'i ekle
builder.Services.AddDbContext<AircraftDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"),
        new MySqlServerVersion(new Version(8, 0, 42)))
);

// Controller servislerini ekle
builder.Services.AddControllers();

// Swagger/OpenAPI servislerini ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Aircraft API", Version = "v1" });
});

var app = builder.Build();

// Sadece Development ortamda Swagger UI'ı etkinleştir
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aircraft API V1");
        c.RoutePrefix = string.Empty; // Swagger UI ana sayfa olarak açılır
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
