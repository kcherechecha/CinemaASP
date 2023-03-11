using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabProject.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LabProject.Controllers
{
    public class SessionsController : Controller
    {
        private readonly CinemaContext _context;

        public SessionsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Sessions
        public async Task<IActionResult> Index(int? id, string? name)
        {
            //if (id == null) return RedirectToAction("Cinemas", "Index");
            //ViewBag.HallId = id;
            //ViewBag.HallName = name;
            //var sessionsByHall = _context.Sessions.Where(b=>b.HallId == id).Include(b => b.Hall);
            //return View(await sessionsByHall.ToListAsync());

            if (id == null) return RedirectToAction("Cinemas", "Index");
            ViewBag.CinemaId = id;
            ViewBag.CinemaName = name;
            var hallsByCinema = _context.Halls.Where(b => b.CinemaId == id).Include(b => b.Cinema);
            var sessionsByHall = _context.Sessions.Where(hallsByCinema => hallsByCinema.HallId == id).Include(hallsByCinema => hallsByCinema.Hall);
            return View(await sessionsByHall.ToListAsync());
        }

        // GET: Sessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Hall)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.SessionId == id);
            if (session == null)
            {
                return NotFound();
            }

            //return View(session);
            return RedirectToAction("Index", "Movies", new { id = session.SessionId, name = session.SessionNumber });
        }

        // GET: Sessions/Create
        public IActionResult Create(int hallId)
        {
            ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "HallId");
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieName");
            

            //ViewBag.CinemaId = cinemaId;
            //ViewBag.CinemaName = _context.Cinemas.Where(b => b.CinemaId == cinemaId).FirstOrDefault().CinemaName;

            //ViewBag.HallId = hallId;
            //ViewBag.HallName = _context.Halls.Where(c => c.HallId == hallId).FirstOrDefault().HallName;
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int hallId, [Bind("SessionId,SessionNumber,SessionDateTime,SessionState,HallId,MovieId")] Session session)
        {

            //session.HallId = hallId;
            if (ModelState.IsValid)
            {
                _context.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Index", "Sessions", new { id = hallId, name = _context.Halls.Where(c => c.HallId == hallId).FirstOrDefault().HallName });
            }
            ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "HallId", session.HallId);
             ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieId", session.MovieId);
            return View(session);
            //RedirectToAction("Index", "Sessions", new { id = hallId, name = _context.Halls.Where(c => c.HallId == hallId).FirstOrDefault().HallName });
        }

        // GET: Sessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "HallId", session.HallId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieId", session.MovieId);
            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SessionId,SessionNumber,SessionDateTime,SessionState,HallId,MovieId")] Session session)
        {
            if (id != session.SessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(session);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(session.SessionId))
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
            ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "HallId", session.HallId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "MovieId", "MovieId", session.MovieId);
            return View(session);
        }

        // GET: Sessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Hall)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.SessionId == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sessions == null)
            {
                return Problem("Entity set 'CinemaContext.Sessions'  is null.");
            }
            var session = await _context.Sessions.FindAsync(id);
            if (session != null)
            {
                _context.Sessions.Remove(session);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionExists(int id)
        {
          return (_context.Sessions?.Any(e => e.SessionId == id)).GetValueOrDefault();
        }
    }
}
