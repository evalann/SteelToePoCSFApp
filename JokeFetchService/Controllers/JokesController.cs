using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JokeFetchService.Controllers
{
    [Route("api/[controller]")]
    public class JokesController : Controller
    {
        private readonly FetchJoke _fetchJoke;

        public JokesController(FetchJoke fetchJoke)
        {
            _fetchJoke = fetchJoke;
        }

        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            var joke = await _fetchJoke.ExecuteAsync();
            return joke.Joke;
        }
    }
}
