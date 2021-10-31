using System.Net.Http;
using System.Threading.Tasks;

namespace WebClient.Services
{
    public class WebService : IWebService
    {
        private readonly HttpClient _client = new();
        
        public async Task<string> GetAsync(string url)
        {
            return await _client.GetStringAsync(url);
        }

        public void PostAsync(string url, string body)
        {
            
        }
    }
}