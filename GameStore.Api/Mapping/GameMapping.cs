using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping
{
    public static class GameMapping
    {
        public static Game ToEntity(this CreateGameDto gameDto)
        {
            if (gameDto == null) return null;
            return new Game
            {
                Name = gameDto.Name,
                GenreId = gameDto.GenreId,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate
            };
        }
        public static Game ToEntity(this UpdateGameDto gameDto, int id)
        {
            if (gameDto == null) return null;
            return new Game
            {
                Id = id,
                Name = gameDto.Name,
                GenreId = gameDto.GenreId,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate
            };
        }

        public static GameSummaryDto ToGameSummaryDto(this Game game)
        {
            if (game == null) return null;
            return new GameSummaryDto(
                game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate);
        }

        public static GameDetailsDto ToGameDetailsDto(this Game game)
        {
            if (game == null) return null;
            return new GameDetailsDto(
                game.Id,
                game.Name,
                game.GenreId.ToString(),
                game.Price,
                game.ReleaseDate);
        }
    }
}
