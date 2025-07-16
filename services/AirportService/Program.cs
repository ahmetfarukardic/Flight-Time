using AirportService.Data;
using Microsoft.EntityFrameworkCore;
using AirportService.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Controller ve Swagger servislerini ekleme
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Veritabanı bağlantısı (MSSQL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository injection
builder.Services.AddScoped<IAirportRepository, AirportRepository>();

var app = builder.Build();

// Geliştirme ortamıysa Swagger UI’ı aktif et
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Yetkilendirme middleware (kullanıyorsanız etkili olur)
app.UseAuthorization();

// Controller endpointlerini haritala
app.MapControllers();

app.Run();