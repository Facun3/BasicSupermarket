using BasicSupermarket;
using BasicSupermarket.Config;
using BasicSupermarket.Persistence;
using BasicSupermarket.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add logging service
builder.Services.AddLogging();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Cors config
// Load CORS settings from appsettings.json
var corsConfig = builder.Configuration.GetSection("Cors");

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("CustomCorsPolicy", policy =>
    {
        var allowedOrigins = corsConfig.GetValue<string>("AllowedOrigins");
        var allowAnyOrigin = corsConfig.GetValue<bool>("AllowAnyOrigin");
        var allowAnyHeader = corsConfig.GetValue<bool>("AllowAnyHeader");
        var allowAnyMethod = corsConfig.GetValue<bool>("AllowAnyMethod");

        if (allowAnyHeader)
        {
            policy.AllowAnyHeader();
        }
        if (allowAnyMethod)
        {
            policy.AllowAnyMethod();
        }
        if (allowAnyOrigin)
        {
            policy.AllowAnyOrigin();
        }
        else if (allowedOrigins != null && allowedOrigins.Length > 0)
        {
            policy.WithOrigins(allowedOrigins);
        }
    });
});

// Extension method for services configuration
builder.Services.ConfigureServices();

builder.Services.AddControllers();

builder.Services.AddFluentValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CustomCorsPolicy");

app.MapControllers();

app.Run();