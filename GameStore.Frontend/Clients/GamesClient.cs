using GameStore.Frontend.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace GameStore.Frontend.Clients
{
    public class GamesClient
    {
        private readonly HttpClient _httpClient;
        public GamesClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
         
        public async Task<GameSummary[]> GetGames() 
            => await _httpClient.GetFromJsonAsync<GameSummary[]>("games") ?? [];
        
        public async Task AddGame(GameDetails game)
            => await _httpClient.PostAsJsonAsync("games", game);

        public async Task DuplicateGame(int id) 
            => await _httpClient.PostAsync($"games/duplicate/{id}", null);
        public async Task<GameDetails?> GetGame(int id)
            => await _httpClient.GetFromJsonAsync<GameDetails>($"games/{id}")
            ?? throw new Exception("Could not find game!");

        public async Task UpdateGame(GameDetails updatedGame) 
            => await _httpClient.PutAsJsonAsync($"games/{updatedGame.Id}", updatedGame);

        public async Task DeleteGame(int id) 
            => await _httpClient.DeleteAsync($"games/{id}");
    }

}

