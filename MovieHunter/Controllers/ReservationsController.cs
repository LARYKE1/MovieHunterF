using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


        
        // GET: Reservations
        public async Task<IActionResult> Index(int? id)
        {
            var userId = await _userManager.GetUserAsync(User);
            var idUser = " ";

            bool val1 = User.Identity.IsAuthenticated;

            if (val1==true)
            {
                idUser = userId.Id;
            }
            else
            {
                return RedirectToAction("Index","Movies");
            }

            var applicationDbContext = _context.Reservations.Include(r => r.Customer)
                                                            .Include(r => r.Movie)
                                                            .Where(r => r.CustomerId.Equals(idUser))
                                                            .AsNoTracking();
                                                            

            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reservations/Create
        public async Task<IActionResult> Create(int? id)
        {
            bool val1 = User.Identity.IsAuthenticated;

           

            if (val1.Equals(true))
            {
                var userId = await _userManager.GetUserAsync(User);
                var idUser = userId.Id;
               
                ViewData["CustomerId"] = new SelectList(_context.Users.Where(r => r.Id.Equals(idUser)), "Id", "Email");
                ViewData["MovieId"] = new SelectList(_context.Movies.Where(s => s.MovieId == id), "MovieId", "Title");
                return View();
            }
            else
            {

                return RedirectToAction("Index", "Movies");
            }
            
        }
        [Route("[controller]/[action]/{id}")]
        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("ReservationId,TicketNumbers,ReservationTime,MovieId,CustomerId")] Reservation reservation)
        {
            

            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Email", reservation.CustomerId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "Title", reservation.MovieId);
            return View(reservation);
        }
        [Route("[controller]/[action]/{id?}")]
        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            bool val1 = User.Identity.IsAuthenticated;



            if (val1.Equals(true))
            {
                var userId = await _userManager.GetUserAsync(User);
                var idUser = userId.Id;

                if (id == null || _context.Reservations == null)
                {
                    return NotFound();
                }

                var reservation = await _context.Reservations.FindAsync(id);
                if (reservation == null)
                {
                    return NotFound();
                }
                ViewData["CustomerId"] = new SelectList(_context.Users.Where(r=>r.Id.Equals(idUser)), "Id", "Email", reservation.CustomerId);
                ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "Title", reservation.MovieId);
                return View(reservation);
            }
            else
            {
                return RedirectToAction("Index", "Movies");
            }
        }
        [Route("[controller]/[action]/{id?}")]
        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationId,TicketNumbers,ReservationTime,PriceTotal,MovieId,CustomerId")] Reservation reservation)
        {

            if (id != reservation.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ReservationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Users, "Id", "Id", reservation.CustomerId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "Title", reservation.MovieId);
            return View(reservation);
        }
        [Route("[controller]/[action]/{id}")]
        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Customer)
                .Include(r => r.Movie)
                .FirstOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }
        [Route("[controller]/[action]/{id}")]
        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reservations'  is null.");
            }
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
          return (_context.Reservations?.Any(e => e.ReservationId == id)).GetValueOrDefault();
        }
    }
}
