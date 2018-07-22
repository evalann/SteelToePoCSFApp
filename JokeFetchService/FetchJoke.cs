using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Steeltoe.CircuitBreaker.Hystrix;

namespace JokeFetchService
{
    public class FetchJoke : HystrixCommand<JokeModel>
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public FetchJoke(IHystrixCommandOptions hystrixCommandOptions) : base(hystrixCommandOptions)
        {
        }

        protected override async Task<JokeModel> RunAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://icanhazdadjoke.com");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.TryAddWithoutValidation("User Agent", "https://github.com/evalann/SteelToePoCSFApp");
            var httpResponseMessage = await _httpClient.SendAsync(request);
            var jokeString = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<JokeModel>(jokeString);
        }
    }
}
