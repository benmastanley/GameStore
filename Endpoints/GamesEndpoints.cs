using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints
{
    public static class GamesEndpoints
    {
        // Constant for the named route used in CreatedAtRoute responses
        const string GetGameEndpointName = "GetGame";

        // In-memory list to store game DTOs
        private static readonly List<GameDto> games =
        [
            new(1, "The Witcher 3", "RPG", 49.99m, new DateOnly(2015, 5, 19)),
            new(2, "Stardew Valley", "Simulation", 14.99m, new DateOnly(2016, 2, 26)),
            new(3, "Celeste", "Platformer", 19.99m, new DateOnly(2018, 1, 25))
        ];


        public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/games").WithParameterValidation();

            // Configure the HTTP request pipeline
            // GET endpoint to retrieve all games
            group.MapGet("/", () => games);

            // GET endpoint to retrieve a single game by id
            group.MapGet("/{id}", (int id) => games.Find(g => g.id == id))
                .WithName(GetGameEndpointName);

            // POST endpoint to create a new game
            group.MapPost("/", (CreateGameDto createGameDto) =>
            {
                var newGame = new GameDto(
                    id: games.Count + 1,
                    name: createGameDto.name,
                    genre: createGameDto.genre,
                    price: createGameDto.price,
                    releaseDate: createGameDto.releaseDate);
                games.Add(newGame);
                // Returns 201 Created with a Location header pointing to the new resource
                return Results.CreatedAtRoute(GetGameEndpointName, new { Id = newGame.id }, newGame);
            });

            // PUT endpoint to update an existing game
            group.MapPut("/{id}", (int id, UpdateGameDto updateGameDto) =>
            {
                var game = games.Find(g => g.id == id);
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
                games[games.FindIndex(g => g.id == id)] = game;
                return Results.Ok(game);
            });

            // DELETE endpoint to remove a game by id
            group.MapDelete("/{id}", (int id) =>
            {
                var game = games.Find(g => g.id == id);
                if (game == null)
                {
                    // Returns 404 Not Found if the game does not exist
                    return Results.NotFound();
                }
                games.Remove(game);
                // Returns 204 No Content on successful deletion
                return Results.NoContent();
            });

            return group;
        }
    }
}
