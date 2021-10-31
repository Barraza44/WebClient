﻿using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
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
    }
}