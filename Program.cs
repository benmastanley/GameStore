using GameStore.Api.Context;
using GameStore.Api.Endpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

// Create a WebApplication builder instance
var builder = WebApplication.CreateBuilder(args);

// Add services to the container for API documentation and endpoint discovery
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
});

builder.Services.AddDbContext<GameStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Build the WebApplication
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


// Enable Swagger middleware for API documentation (available in all environments)
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
});

app.MapGamesEndpoints();

// Start the application
app.Run();
