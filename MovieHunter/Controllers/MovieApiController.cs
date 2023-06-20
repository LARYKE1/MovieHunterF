using Microsoft.AspNetCore.Mvc;
using MovieHunter.Models;

namespace MovieHunter.Controllers
{
    
    public class MovieApiController : Controller
    {
        private readonly HttpClient _httpClient;

        public MovieApiController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5000/api/");
        }
        [Route("~/")]
        [Route("Movie/{title}/{genre?}")]
        [Route("MovieHunter/{title}/{genre?}")]
        public async Task<IActionResult> Index(string? title, string? genre)
        {
            //api route
            var allMovies = "movies";

            //if statement to check if string name is null or not
            if (!string.IsNullOrEmpty(title))
            {
                //if is not the URI will change so it can get from api the title
                allMovies += $"?title={title}";
            }
            if(!string.IsNullOrEmpty(title))
            {
                allMovies += $"&genre={genre}";
            }
            //get all the movies from api, from http://localhost:5000/api/movies
            var response = await _httpClient.GetAsync(allMovies);

            //if the response from the api is Ok, it will read the api and send to the view
            if (response.IsSuccessStatusCode)
            {
                var movie = await response.Content.ReadAsAsync<List<Movies>>();

                return View(movie);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("Movie/{id:int}")]
        [Route("MovieHunter/{id:int}")]
        public async Task<IActionResult> Details(int? id)
        {
            //set the uri to /movies/id
            var movieById = $"movies/{id}";

            //get the movie
            var response = await _httpClient.GetAsync(movieById);

            //if the response is okay read the movie and send to the view
            if (response.IsSuccessStatusCode)
            {
                var movie = await response.Content.ReadAsAsync<Movies>();

                return View(movie);
            }
            else
            {
                return NotFound();
            }
        }


    }
}