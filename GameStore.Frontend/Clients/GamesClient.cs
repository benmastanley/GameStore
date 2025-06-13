using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients
{
    public class GamesClient
    {
        private readonly List<GameSummary> games =
        [
            new(){
                Id = 1,
                Name = "Street Fighter II",
                Genre = "Fighting",
                Price = 19.99m,
                ReleaseDate = new DateOnly(1992, 7, 15)
            },
            new (){
                Id = 2,
                Name = "The Legend of Zelda: Ocarina of Time",
                Genre = "Action-Adventure",
                Price = 59.99m,
                ReleaseDate = new DateOnly(1998, 11, 21)
            },
            new (){
                Id = 3,
                Name = "Final Fantasy VII",
                Genre = "Role-Playing",
                Price = 49.99m,
                ReleaseDate = new DateOnly(1997, 1, 31)
            },
        ];

        private readonly Genre[] genres = new GenresClient().GetGenres();
        public GameSummary[] GetGames() => [.. games];
        
        public void AddGame(GameDetails game)
        {
            ArgumentException.ThrowIfNullOrEmpty(game.GenreId);
            var genre = genres.Single(x => x.Id == int.Parse(game.GenreId));

           var gameSummary = new GameSummary
            {
                Id = games.Count + 1,
                Name = game.Name,
                Genre = genre.Name,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate
            };
             
            games.Add(gameSummary);
        }
    }

}

