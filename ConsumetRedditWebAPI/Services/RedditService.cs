using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using ConsumeRedditWebAPI.Models;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace ConsumeRedditWebAPI.Services

{
    public class RedditService : IRedditService
    {
        private readonly HttpClient _httpClient;
        public RedditService(HttpClient httpClient)
        {
            this._httpClient = httpClient;

        }

        public async Task<string> GetMostUpVotes(string accessToken)
        {

            using HttpRequestMessage request = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://oauth.reddit.com/r/funny/top")
            };

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            request.Headers.Add("User-Agent", "MM-test-token");

            using HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();

            var responseObject = await System.Text.Json.JsonSerializer.DeserializeAsync<GetPostsWithMostUpVotes>(responseStream);

            MostUpVotes mvp = new MostUpVotes();

            var titleOfPost1 = responseObject?.data?.children[0]?.data?.title;
            
            var authorOfPost1 = responseObject?.data?.children[0]?.data?.author;

            Console.Write("Title 1: " + titleOfPost1 + "\n" + "Author: " + authorOfPost1);
            Console.Write("\n");
            var titleOfPost2 = responseObject?.data?.children[1]?.data?.title;
            var authorOfPost2 = responseObject?.data?.children[1]?.data?.author;
            Console.Write("Title 2: " + titleOfPost2 + "\n" + "Author: " + authorOfPost2);
            Console.Write("\n");
            var titleOfPost3 = responseObject?.data?.children[2]?.data?.title;
            var authorOfPost3 = responseObject?.data?.children[2]?.data?.author;
            Console.Write("Title 3: " + titleOfPost3 + "\n" + "Author: " + authorOfPost3);

            return string.Empty;

        }

    }
}
