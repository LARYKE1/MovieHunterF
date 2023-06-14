using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieHunter.Data;

namespace MovieHunter.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            //Check if the Movies tables has any data
            if (_context.Movies.Any())
            {
                //LINQ query to retrieve all movies from Movies
                var movies = from m in _context.Movies
                             select m;

                //Check if the parameter is null
                if (!string.IsNullOrEmpty(search))
                {
                    //Trim method to remove any accidentally extra spaces
                    search = search.Trim();

                    //LINQ query to retrieve the movie that has the searching index in it
                    movies = from m in _context.Movies
                             where m.Title.Contains(search)
                             select m;

                    //AsNoTracking is used for performance, because there is no changed made on this view
                    return View(await movies.AsNoTracking().ToListAsync());
                }

                return View(await _context.Movies.AsNoTracking().ToListAsync());
            }
            else
            {
                return NotFound();
            }
        }


        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movies = await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieId == id);

            if (movies == null)
            {
                return NotFound();
            }

            return View(movies);
        }
    }
}