using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Controller'ları ekle
builder.Services.AddControllers();
builder.Services.AddHttpClient();  // <-- Bu satır olmalı!


// Swagger için gerekli servisleri ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "FlightTime API", 
        Version = "v1",
        Description = "FlightTime mikroservis API dokümantasyonu"
    });
});

var app = builder.Build();

// Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlightTime API v1");
    c.RoutePrefix = string.Empty; // Swagger UI ana sayfa olarak açılır
});

app.UseAuthorization();

app.MapControllers();

app.Run();
