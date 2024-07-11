using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConsumeRedditWebAPI.Models;
using System.Net;

using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.DataProtection;
using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;

namespace ConsumeRedditWebAPI.Services
{
    public class RedditAccountService : IRedditAccountService
    {
        private readonly HttpClient _httpClient;

        public RedditAccountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetToken(string userName, string userPassword, string clientId, string clientSecret, string userAgent)
        {
            using HttpRequestMessage request = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://www.reddit.com/api/v1/access_token")
            };

            string requestAuthLogin = Convert.ToBase64String(Encoding.Default.GetBytes($"{clientId}:{clientSecret}"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", requestAuthLogin);
            request.Headers.Add("User-Agent", "test-app");
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "username", userName },
                { "password", userPassword }
            });

            using HttpResponseMessage response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            RedditToken token = await response.Content.ReadFromJsonAsync<RedditToken>();

            return token.access_token;

        }
    }
}