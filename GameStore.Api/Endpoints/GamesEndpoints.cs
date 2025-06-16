using GameStore.Api.Context;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints
{
    public static class GamesEndpoints
    {
        // Constant for the named route used in CreatedAtRoute responses
        const string GetGameEndpointName = "GetGame";


        public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/games").WithParameterValidation();

            // Configure the HTTP request pipeline
            // GET endpoint to retrieve all games
            group.MapGet("/", async (GameStoreContext dbContext) =>  
                await dbContext.Games
                .Include(g => g.Genre)
                .Select(g => g.ToGameSummaryDto())
                .AsNoTracking()
                .ToListAsync());

            // GET endpoint to retrieve a single game by id
            group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
            {
                var game = await dbContext.Games.FindAsync(id);
                return game != null ? Results.Ok(game.ToGameDetailsDto()) : Results.NotFound();
            }).WithName(GetGameEndpointName);

            // POST endpoint to create a new game
            group.MapPost("/", async (CreateGameDto createGameDto, GameStoreContext dbContext) =>
            {
                Game game = createGameDto.ToEntity();
                await dbContext.Games.AddAsync(game);
                await dbContext.SaveChangesAsync();
                game.Genre = await dbContext.Genres.FirstOrDefaultAsync(x => x.Id == game.GenreId);
                var gameDto = game.ToGameSummaryDto();

                // Returns 201 Created with a Location header pointing to the new resource
                return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, gameDto);
            });

            // PUT endpoint to update an existing game
            group.MapPut("/{id}", async (int id, UpdateGameDto updateGameDto, GameStoreContext dbContext) =>
            {
                var existingGame = await dbContext.Games.FirstOrDefaultAsync(g => g.Id == id);
                if (existingGame == null)
                {
                    // Returns 404 Not Found if the game does not exist
                    return Results.NotFound();
                }

                dbContext.Entry(existingGame).CurrentValues.SetValues(updateGameDto.ToEntity(id));
                await dbContext.SaveChangesAsync();
                 
                return Results.NoContent();
            });

            // DELETE endpoint to remove a game by id
            group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
            {
                var game = dbContext.Games.FirstOrDefault(g => g.Id == id);
                if (game == null)
                {
                    // Returns 404 Not Found if the game does not exist
                    return Results.NotFound();
                }
                dbContext.Remove(game);
                await dbContext.SaveChangesAsync();
                // Returns 204 No Content on successful deletion
                return Results.NoContent();
            });

            return group;
        }
    }
}
