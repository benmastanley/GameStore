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
            group.MapGet("/", (GameStoreContext dbContext) => dbContext.Games
                .Include(g => g.Genre)
                .Select(g => g.ToGameSummaryDto())
                .AsNoTracking());

            // GET endpoint to retrieve a single game by id
            group.MapGet("/{id}", (int id, GameStoreContext dbContext) =>
            {
                var game = dbContext.Games.Find(id);
                return game != null ? Results.Ok(game.ToGameDetailsDto()) : Results.NotFound();
            }).WithName(GetGameEndpointName);

            // POST endpoint to create a new game
            group.MapPost("/", (CreateGameDto createGameDto, GameStoreContext dbContext) =>
            {
                Game game = createGameDto.ToEntity();
                dbContext.Games.Add(game);
                dbContext.SaveChanges();

                var gameDto = game.ToGameSummaryDto();

                // Returns 201 Created with a Location header pointing to the new resource
                return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, gameDto);
            });

            // PUT endpoint to update an existing game
            group.MapPut("/{id}", (int id, UpdateGameDto updateGameDto, GameStoreContext dbContext) =>
            {
                var existingGame = dbContext.Games.FirstOrDefault(g => g.Id == id);
                if (existingGame == null)
                {
                    // Returns 404 Not Found if the game does not exist
                    return Results.NotFound();
                }

                dbContext.Entry(existingGame).CurrentValues.SetValues(updateGameDto.ToEntity(id));
                dbContext.SaveChanges();
                 
                return Results.NoContent();
            });

            // DELETE endpoint to remove a game by id
            group.MapDelete("/{id}", (int id, GameStoreContext dbContext) =>
            {
                var game = dbContext.Games.FirstOrDefault(g => g.Id == id);
                if (game == null)
                {
                    // Returns 404 Not Found if the game does not exist
                    return Results.NotFound();
                }
                dbContext.Remove(game);
                dbContext.SaveChanges();
                // Returns 204 No Content on successful deletion
                return Results.NoContent();
            });

            return group;
        }
    }
}
