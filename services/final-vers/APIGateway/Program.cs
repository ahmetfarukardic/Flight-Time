using APIGateway.Services;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient<HttpAirportServiceClient>();
builder.Services.AddScoped<IAirportLocationClient, HttpAirportServiceClient>();


builder.Services.AddHttpClient<HttpDistanceServiceClient>();
builder.Services.AddScoped<IDistanceServiceClient, HttpDistanceServiceClient>();



builder.Services.AddHttpClient<HttpAircraftServiceClient>();
builder.Services.AddScoped<IAircraftServiceClient, HttpAircraftServiceClient>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
