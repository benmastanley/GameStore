using GameStore.Api.Context;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints
{
    public static class GenresEndpoints
    {
        public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/genres");
            group.MapGet("/", async (GameStoreContext db) =>
                await db.Genres
                    .Select(g => g.ToDto())
                    .AsNoTracking()
                    .ToListAsync());
            return group;
        }
    }
}
