namespace GameStore.Api.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        //public string Description { get; set; } = string.Empty;
        // Navigation property for related games
        public ICollection<Game>? Games { get; set; }
    }
}
