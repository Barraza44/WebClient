using System.Threading.Tasks;

namespace WebClient.Services
{
    public interface IWebService
    {
        /// <summary>
        /// Sends a GET request to a specified URL and returns the response as a string asynchronously
        /// </summary>
        /// <param name="url">The Url to request to</param>
        /// <returns>A string containing the response body</returns>
        public Task<string> GetAsync(string url);

        
        /// <summary>
        /// Sends a POST request to a specified URL as a string and returns the response body as a string asynchronously
        /// </summary>
        /// <param name="url">The Url to request to</param>
        /// <param name="body">The string data that will be sent as the request body</param>
        /// <returns>A string containing the response body</returns>
        public Task<string> PostAsync(string url, string body);

        
        /// <summary>
        /// Sends a POST request to a specified URL as JSON and returns the response body as a string asynchronously
        /// </summary>
        /// <param name="url">The Url to request to</param>
        /// <param name="body">The string data that will be sent as the request body</param>
        /// <returns>A string containing the response body</returns>
        public Task<string> PostAsJsonAsync(string url, string body);

        public Task<string> PostAsFormAsync(string url, string body);

        /// <summary>
        /// Sends a GET request to a specified URL and returns the response as a byte array asynchronously
        /// </summary>
        /// <param name="url">The Url to request to</param>
        /// <returns>A byte array containing the response body</returns>
        public Task<byte[]> GetAsByteArrayAsync(string url);
    }
}