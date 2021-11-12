using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebClient.Services;

namespace WebClient.Handlers
{
    public class WebHandler : IWebService
    {
        private readonly HttpClient _client = new();


        public async Task<string> GetAsync(string url)
        {
            return await _client.GetStringAsync(url);
        }

        public async Task<string> PostAsync(string url, string body)
        {
            var content = new StringContent(body);
            var responseMessage = await _client.PostAsync(url, content);
            return await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsJsonAsync(string url, string body)
        {
            var content = JsonContent.Create(body);
            var responseMessage = await _client.PostAsync(url, content);
            return await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task<byte[]> GetAsByteArrayAsync(string url)
        {
            return await _client.GetByteArrayAsync(url);
        }
    }
}