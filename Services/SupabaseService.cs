using System.Net.Http.Json;

namespace Perguntas.Client.Services
{
    public class SupabaseService
    {
        private readonly HttpClient _httpClient;

        private const string SupabaseUrl =
            "https://qydptxgpigmnkpnwqvua.supabase.co/rest/v1/";

        private const string SupabaseKey =
            "sb_publishable_4I6BgEHbVluVZhrqrfXFaQ_T9x6A77X";

        public SupabaseService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri(SupabaseUrl);

            _httpClient.DefaultRequestHeaders.Add(
                "apikey",
                SupabaseKey
            );
        }

        public async Task<List<T>> GetAsync<T>(string table)
        {
            var response = await _httpClient.GetAsync(
                $"{table}?select=*"
            );

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<T>>()
                   ?? [];
        }
    }
}