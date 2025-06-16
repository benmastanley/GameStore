using GameStore.Frontend.Models;
using System.Threading.Tasks;

namespace GameStore.Frontend.Clients
{
    public class GenresClient
    {
        private readonly HttpClient _httpClient;
        public GenresClient(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<Genre[]> GetGenres()
            => await _httpClient.GetFromJsonAsync<Genre[]>("/genres") ?? [];
    }
}
