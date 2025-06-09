using GameStore.Api.Dtos;
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

// Build the WebApplication
var app = builder.Build();

// Enable Swagger middleware for API documentation (available in all environments)
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
});

// Constant for the named route used in CreatedAtRoute responses
const string GetGameEndpointName = "GetGame";

// In-memory list to store game DTOs
var dtos = new List<GameDto>
{
    new(1, "The Witcher 3", "RPG", 49.99m, new DateOnly(2015, 5, 19)),
    new(2, "Stardew Valley", "Simulation", 14.99m, new DateOnly(2016, 2, 26)),
    new(3, "Celeste", "Platformer", 19.99m, new DateOnly(2018, 1, 25))
};


// Configure the HTTP request pipeline
// GET endpoint to retrieve all games
app.MapGet("/games", () => dtos);

// GET endpoint to retrieve a single game by id
app.MapGet("/games/{id}", (int id) => dtos.Find(g => g.id == id))
    .WithName(GetGameEndpointName);

// POST endpoint to create a new game
app.MapPost("/games", (CreateGameDto createGameDto) =>
{
    var newGame = new GameDto(
        id: dtos.Count + 1,
        name: createGameDto.name,
        genre: createGameDto.genre,
        price: createGameDto.price,
        releaseDate: createGameDto.releaseDate);
    dtos.Add(newGame);
    // Returns 201 Created with a Location header pointing to the new resource
    return Results.CreatedAtRoute(GetGameEndpointName, new { Id = newGame.id }, newGame);
});

// PUT endpoint to update an existing game
app.MapPut("/games/{id}", (int id, UpdateGameDto updateGameDto) =>
{
    var game = dtos.Find(g => g.id == id);
    if (game == null)
    {
        // Returns 404 Not Found if the game does not exist
        return Results.NotFound();
    }
    // Create a new GameDto with updated values
    game = new GameDto(
        id: game.id,
        name: updateGameDto.name,
        genre: updateGameDto.genre,
        price: updateGameDto.price,
        releaseDate: updateGameDto.releaseDate);

    // Replace the old game with the updated one
    dtos[dtos.FindIndex(g => g.id == id)] = game;
    return Results.Ok(game);
});

// DELETE endpoint to remove a game by id
app.MapDelete("/games/{id}", (int id) =>
{
    var game = dtos.Find(g => g.id == id);
    if (game == null)
    {
        // Returns 404 Not Found if the game does not exist
        return Results.NotFound();
    }
    dtos.Remove(game);
    // Returns 204 No Content on successful deletion
    return Results.NoContent();
});

// Start the application
app.Run();
