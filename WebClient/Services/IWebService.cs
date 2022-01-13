using System.Threading.Tasks;

namespace WebClient.Services
{
    public interface IWebService
    {
        /// <summary>
        /// Sends a GET request to a specified URL and returns the response as a string asynchronously
        /// </summary>
        /// <param name="url">The Url to request to</param>
        /// <param name="headers">The HTTP request headers</param>
        /// <returns>A string containing the response body</returns>
        public Task<string> GetAsync(string url, string headers);


        /// <summary>
        /// Sends a POST request to a specified URL as a string and returns the response body as a string asynchronously
        /// </summary>
        /// <param name="url">The Url to request to</param>
        /// <param name="body">The string data that will be sent as the request body</param>
        /// <param name="headers">The HTTP request headers</param>
        /// <returns>A string containing the response body</returns>
        public Task<string> PostAsync(string url, string body, string headers);


        /// <summary>
        /// Sends a POST request to a specified URL as JSON and returns the response body as a string asynchronously
        /// </summary>
        /// <param name="url">The Url to request to</param>
        /// <param name="body">The string data that will be sent as the request body</param>
        /// <param name="headers">The HTTP request headers</param>
        /// <returns>A string containing the response body</returns>
        public Task<string> PostAsJsonAsync(string url, string body, string headers);

        /// <summary>
        /// Sends a POST request to a specified URL as UrlFormEncoded and returns the response body as a string asynchronously
        /// </summary>
        /// <param name="url">The URL to request to</param>
        /// <param name="body">The key-value pair collection as string that will be sent as the request body</param>
        /// <param name="headers">The HTTP request headers</param>
        /// <returns>A string containing the response body</returns>
        public Task<string> PostAsFormAsync(string url, string body, string headers);

        public Task<string> PutAsync(string url, string body, string headers);
        public Task<string> PutAsJsonAsync(string url, string body, string headers);
        public Task<string> PutAsFormAsync(string url, string body, string headers);

        public Task<string> PatchAsync(string url, string body, string headers);
        public Task<string> PatchAsJsonAsync(string url, string body, string headers);
        public Task<string> PatchAsFormAsync(string url, string body, string headers);

        public Task<string> DeleteAsync(string url, string headers);



        /// <summary>
        /// Sends a GET request to a specified URL and returns the response as a byte array asynchronously
        /// </summary>
        /// <param name="url">The Url to request to</param>
        /// <returns>A byte array containing the response body</returns>
        public Task<byte[]> GetAsByteArrayAsync(string url);
    }
}