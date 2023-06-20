using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieHunter.Data;
using MovieHunter.Models;

namespace MovieHunter.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public ReservationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("MyReservation")]
        public async Task<IActionResult> Index(int? id)
        {
            //get the current authenticated user and store inside userId
            var userId = await _userManager.GetUserAsync(User);

            //check if a user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                //store the reservation information of userId inside applicationdbcontext and then return as list)
                var applicationDbContext = _context.Reservations.Include(r => r.Customer)
                                                            .Include(r => r.Movie)
                                                            .Include(r => r.AvailableDate)
                                                            .Where(r => r.CustomerId.Equals(userId.Id))
                                                            .AsNoTracking();

                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "MovieApi");
            }
        }

        //Shows the form to create the reservation
        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            //Same concept as before, check if the user is authenticated and then store inside userId
            if (User.Identity.IsAuthenticated)
            {
                var userId = await _userManager.GetUserAsync(User);

                //get the data as select where r equals current user, to get that user reservation
                ViewData["CustomerId"] = new SelectList(_context.Users.Where(r => r.Id.Equals(userId.Id)), "Id", "Email");
                //select list to show only the movie you selected from first page
                ViewData["MovieId"] = new SelectList(_context.Movies.Where(s => s.MovieId == id), "MovieId", "Title");
                //select list to show the data for the movie selected.
                ViewData["DateId"] = new SelectList(_context.AvailableDate.Where(s => s.MovieId == id), "DateId", "Date");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Movies");
            }
        }

        [Route("[controller]/[action]/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("TicketNumbers,MovieId,CustomerId,DateId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                //save the new reservation if the model validation is correct
                _context.Add(reservation);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Email", reservation.CustomerId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "Title", reservation.MovieId);
            ViewData["DateId"] = new SelectList(_context.AvailableDate, "DateId", "Date", reservation.DateId);

            return View(reservation);
        }

        [Route("[controller]/[action]/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }
            //include all tables and get reservation that has the reservationid equals id
            var reservation = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Movie)
                .Include(r => r.AvailableDate)
                .FirstOrDefaultAsync(m => m.ReservationId == id);

            if (reservation == null)
            {
                return NotFound();
            }
            //remove, [httpdelete] cause me some errors
            _context.Reservations.Remove(reservation);
            //save the chances
            await _context.SaveChangesAsync();

            //redirect to index page of reservation table
            return RedirectToAction(nameof(Index));
        }

        //Pedram
        [HttpGet]
        public IActionResult Preview(int? movieId, int? ticketNumbers, int? dateId, string customerId)
        {
            if (movieId == null || ticketNumbers == null || dateId == null || string.IsNullOrEmpty(customerId))
            {
                return NotFound();
            }

            // Retrieve the necessary data from the database
            var movie = _context.Movies.Find(movieId);
            var customer = _context.Users.Find(customerId);
            var date = _context.AvailableDate.Find(dateId);

            if (movie == null || customer == null || date == null)
            {
                return NotFound();
            }

            // Create a view model to pass the data to the preview view
            var previewViewModel = new Reservation
            {
                Movie = movie,
                TicketNumbers = (byte)ticketNumbers.Value,
                AvailableDate = date,
                Customer = customer
            };

            return View(previewViewModel);
        }

        private bool ReservationExists(int id)
        {
            return (_context.Reservations?.Any(e => e.ReservationId == id)).GetValueOrDefault();
        }
    }
}