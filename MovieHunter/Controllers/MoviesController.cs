using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieHunter.Data;
using MovieHunter.Models;

namespace MovieHunter.Controllers
{
    
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Movies
        
        public async Task<IActionResult> Index(string? name)
        {
            ViewData["MovieName"] = name;
            var movies = from m in _context.Movies
                         select m;

            if (!string.IsNullOrEmpty(name))
            {
                movies = movies.Where(r => r.Title.Contains(name) || r.Title.StartsWith(name));

                return View(await movies.AsNoTracking().ToListAsync());
            }


            return _context.Movies != null ?
                        View(await _context.Movies.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Movies'  is null.");
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
