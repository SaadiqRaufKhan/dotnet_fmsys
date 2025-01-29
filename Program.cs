using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using fleetsystem.repository;
using fleetsystem.config;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddDbContext<TruckDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 25))));

builder.Services.AddScoped<ITruckRepository, TruckRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();

builder.Services.AddFastEndpoints();

var app = builder.Build();

// Ensure the database and tables are created automatically (for development environment)
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TruckDbContext>();
    dbContext.Database.EnsureDeleted(); // This will delete the database and tables if they exist
    dbContext.Database.EnsureCreated(); // This will create the database and tables if they don't exist

}

// app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseDefaultExceptionHandler();

// Configure middleware                 
app.UseFastEndpoints();

// Run the application
app.Run();



// var builder = WebApplication.CreateBuilder(args);

// var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

// app.MapGet("/trucks", () => "Hello User Find trucks here!");

// app.Run();
