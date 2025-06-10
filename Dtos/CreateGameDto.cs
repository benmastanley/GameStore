using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos
{
    public record class CreateGameDto(
        [Required][MaxLength(50)] string name,
        [Required][MaxLength(20)] string genre,
        [Range(1, 100)] decimal price,
         DateOnly releaseDate);
}
