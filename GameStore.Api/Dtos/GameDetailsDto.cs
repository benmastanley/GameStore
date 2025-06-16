﻿namespace GameStore.Api.Dtos
{
    public record class GameDetailsDto(
        int Id,
        string Name,
        string GenreId,
        decimal Price,
        DateOnly ReleaseDate);
}
