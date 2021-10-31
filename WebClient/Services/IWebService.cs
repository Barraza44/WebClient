using System.Threading.Tasks;

namespace WebClient.Services
{
    public interface IWebService
    {
        public Task<string> GetAsync(string url);

        public Task<string> PostAsync(string url, string body);
    }
}