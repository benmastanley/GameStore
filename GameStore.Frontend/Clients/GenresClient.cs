using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients
{
    public class GenresClient
    {
        private readonly List<Genre> genres =
        [
            new(){
                Id = 1,
                 Name = "Fighting",
            },
            new (){
                Id = 2,
                Name = "Action-Adventure",
            },
            new (){
                Id = 3,
                Name = "Role-Playing",
            },
            new (){
                Id = 4,
                Name = "Shooter",
            },
            new (){
                Id = 5,
                Name = "Strategy",
            },
        ];

        public Genre[] GetGenres() => [.. genres];
    }
}
