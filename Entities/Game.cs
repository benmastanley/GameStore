namespace GameStore.Api.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public DateOnly ReleaseDate { get; set; }
        
        // Navigation property for related genres, if needed in the future
        public int? GenreId { get; set; }
        public Genre? Genre { get; set; }
    }
}
