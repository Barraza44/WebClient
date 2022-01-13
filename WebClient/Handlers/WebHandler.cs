using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebClient.Services;

namespace WebClient.Handlers
{
    public class WebHandler : IWebService
    {
        private readonly HttpClient _client = new();


        public async Task<string> GetAsync(string url, string headers)
        {
            if (headers is not null) AddHeaders(_client, headers);
            return await _client.GetStringAsync(url);
        }

        public async Task<string> PostAsync(string url, string body, string headers)
        {
            if (headers is not null) AddHeaders(_client, headers);
            var content = new StringContent(body);
            var responseMessage = await _client.PostAsync(url, content);
            return await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsJsonAsync(string url, string body, string headers)
        {
            if (headers is not null) AddHeaders(_client, headers);
            var content = JsonContent.Create(body);
            var responseMessage = await _client.PostAsync(url, content);
            return await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsFormAsync(string url, string body, string headers)
        {
            if (headers is not null) AddHeaders(_client, headers);
            var data = body.Split("&");
            var keysAndValues = new Dictionary<string, string>();
            foreach (var collection in data)
            {
                var entries = collection.Split("=");
                for (int i = 1; i < entries.Length; i++)
                {
                    keysAndValues.Add(entries[i - 1], entries[i]);
                }
            }

            var content = new FormUrlEncodedContent(keysAndValues);
            var response = await _client.PostAsync(url, content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<byte[]> GetAsByteArrayAsync(string url)
        {
            return await _client.GetByteArrayAsync(url);
        }
        
        public async Task<string> PutAsync(string url, string body, string headers)
        {
            if (headers is not null) AddHeaders(_client, headers);
            var content = new StringContent(body);
            var responseMessage = await _client.PutAsync(url, content);
            return await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task<string> PutAsJsonAsync(string url, string body, string headers)
        {
            if (headers is not null) AddHeaders(_client, headers);
            var content = JsonContent.Create(body);
            var responseMessage = await _client.PutAsJsonAsync(url, content);
            return await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task<string> PutAsFormAsync(string url, string body, string headers)
        {
            if (headers is not null) AddHeaders(_client, headers);
            var data = body.Split("&");
            var keysAndValues = new Dictionary<string, string>();
            foreach (var collection in data)
            {
                var entries = collection.Split("=");
                for (int i = 1; i < entries.Length; i++)
                {
                    keysAndValues.Add(entries[i - 1], entries[i]);
                }
            }

            var content = new FormUrlEncodedContent(keysAndValues);
            var response = await _client.PutAsync(url, content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PatchAsync(string url, string body, string headers)
        {
            if (headers is not null) AddHeaders(_client, headers);
            var content = new StringContent(body);
            var responseMessage = await _client.PatchAsync(url, content);
            return await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task<string> PatchAsJsonAsync(string url, string body, string headers)
        {
            if (headers is not null) AddHeaders(_client, headers);
            var content = JsonContent.Create(body);
            var responseMessage = await _client.PatchAsync(url, content);
            _client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            return await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task<string> PatchAsFormAsync(string url, string body, string headers)
        {
            if (headers is not null) AddHeaders(_client, headers);
            var data = body.Split("&");
            var keysAndValues = new Dictionary<string, string>();
            foreach (var collection in data)
            {
                var entries = collection.Split("=");
                for (int i = 1; i < entries.Length; i++)
                {
                    keysAndValues.Add(entries[i - 1], entries[i]);
                }
            }

            var content = new FormUrlEncodedContent(keysAndValues);
            var response = await _client.PatchAsync(url, content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteAsync(string url, string headers)
        {
            if (headers is not null) AddHeaders(_client, headers);
            var responseMessage = await _client.DeleteAsync(url);
            return await responseMessage.Content.ReadAsStringAsync();
        }
        
        private static void AddHeaders(HttpClient client, string headers)
        {
            var headerList = headers.Split(", ");
            foreach (var header in headerList)
            {
                var pairs = header.Split(": ");
                for (int j = 1; j < pairs.Length; j++)
                {
                    client.DefaultRequestHeaders.Add(pairs[j - 1], pairs[j]);
                }
            }
        }
    }
}