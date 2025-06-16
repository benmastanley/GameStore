using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping
{
    public static class GenreMapping
    {
        public static GenreDto ToDto(this Genre genre)
        {
            if (genre == null) return null;
            return new GenreDto(genre.Id.ToString(), genre.Name);
        }
    }
}
