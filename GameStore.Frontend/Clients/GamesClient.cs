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
            Genre genre = GetGenreById(game.GenreId);

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


        public GameDetails? GetGame(int id)
        {
            GameSummary game = GetGameSummaryById(id);

            var genre = genres.Single(x => string.Equals(
                x.Name,
                game.Genre,
                StringComparison.OrdinalIgnoreCase));

            return new GameDetails
            {
                Id = game.Id,
                Name = game.Name,
                GenreId = genre.Id.ToString(),
                Price = game.Price,
                ReleaseDate = game.ReleaseDate
            };
        }

        public void UpdateGame(GameDetails updatedGame)
        {
            //ArgumentNullException.ThrowIfNull(updatedGame);
            //ArgumentException.ThrowIfNullOrEmpty(updatedGame.Name);
            //ArgumentException.ThrowIfNullOrEmpty(updatedGame.GenreId);

            Genre genre = GetGenreById(updatedGame.GenreId);
            GameSummary existingGame = GetGameSummaryById(updatedGame.Id!.Value);

            existingGame.Name = updatedGame.Name;
            existingGame.Genre = genre.Name;
            existingGame.Price = updatedGame.Price;
            existingGame.ReleaseDate = updatedGame.ReleaseDate;
        }

        public void DeleteGame(int id)
        {
            GameSummary game = GetGameSummaryById(id);
            games.Remove(game);
        }


        private GameSummary GetGameSummaryById(int id)
        {
            var game = games.SingleOrDefault(x => x.Id == id);
            ArgumentNullException.ThrowIfNull(game);
            return game;
        }

        private Genre GetGenreById(string? id)
        {
            ArgumentException.ThrowIfNullOrEmpty(id);
            var genre = genres.Single(x => x.Id == int.Parse(id));
            return genre;
        }
    }

}

