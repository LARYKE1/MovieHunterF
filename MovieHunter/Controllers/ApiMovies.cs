using Microsoft.AspNetCore.Mvc;
using MovieHunter.Data;
using MovieHunter.Models;

namespace MovieHunter.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class ApiMovies : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiMovies(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllMovies([FromQuery] string? title, [FromQuery] string? genre)
        {
            IQueryable<Movies> moviesQuery = _context.Movies;

            if (!string.IsNullOrEmpty(title))
            {
                moviesQuery = moviesQuery.Where(m => m.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(genre))
            {
                moviesQuery = moviesQuery.Where(m => m.Genre.Contains(genre));
            }

            var movies = moviesQuery.ToList();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public IActionResult GetMovie(int id)
        {
            var movie = _context.Movies.FirstOrDefault(i => i.MovieId == id);

            if (movie == null)
            {
                return RedirectToAction("GetAllMovies");
            }
            return Ok(movie);
        }

        //[HttpGet("name/{name}")]
        //public IActionResult GetMovieByName(string name)
        //{
        //    var movie=_context.Movies.Where(r=>r.Title.Contains(name)).ToList();
        //    if(movie == null)
        //    {
        //        return RedirectToAction("GetAllMovies");
        //    }
        //    return Ok(movie);
        //}
    }
}