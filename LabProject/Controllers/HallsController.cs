using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabProject.Models;

namespace LabProject.Controllers
{
    public class HallsController : Controller
    {
        private readonly CinemaContext _context;

        public HallsController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Halls
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Cinemas", "Index");
            ViewBag.CinemaId = id;
            ViewBag.CinemaName = name;
            var hallsByCinema = _context.Halls.Where(b => b.CinemaId == id).Include(b => b.Cinema);
            return View(await hallsByCinema.ToListAsync());
        }

        // GET: Halls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Halls == null)
            {
                return NotFound();
            }

            var hall = await _context.Halls
                .Include(h => h.Cinema)
                .FirstOrDefaultAsync(m => m.HallId == id);
            if (hall == null)
            {
                return NotFound();
            }

            //return View(hall);
            return RedirectToAction("Index", "Sessions", new { id = hall.HallId, name = hall.HallName });
        }

        // GET: Halls/Create
        public IActionResult Create(int cinemaId)
        {
            //ViewData["CinemaId"] = new SelectList(_context.Cinemas, "CinemaId", "CinemaName");

            ViewBag.CinemaId = cinemaId;
            ViewBag.CinemaName = _context.Cinemas.Where(c => c.CinemaId == cinemaId).FirstOrDefault().CinemaName;

            return View();
        }

        // POST: Halls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int cinemaId, [Bind("HallId,HallName,HallCapacity,CinemaId")] Hall hall)
        {
            hall.CinemaId = cinemaId;
            if (ModelState.IsValid)
            {
                var existHallName = await _context.Halls.FirstOrDefaultAsync(c => c.HallName == hall.HallName && c.CinemaId == cinemaId);

                if (existHallName != null)
                {
                    ViewBag.CinemaId = cinemaId;
                    ModelState.AddModelError("HallName", "Зала з такою назвою вже існує в цьому кінотеатрі");
                    return View(existHallName);
                }
                _context.Add(hall);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Halls", new { id = cinemaId, name = _context.Cinemas.Where(c => c.CinemaId == cinemaId).FirstOrDefault().CinemaName});
            }
            //ViewData["CinemaId"] = new SelectList(_context.Cinemas, "CinemaId", "CinemaName", hall.CinemaId);
            //return View(hall);
            return RedirectToAction("Index", "Halls", new { id = cinemaId, name = _context.Cinemas.Where(c => c.CinemaId == cinemaId).FirstOrDefault().CinemaName });
        }

        // GET: Halls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Halls == null)
            {
                return NotFound();
            }

            var hall = await _context.Halls.FindAsync(id);
            if (hall == null)
            {
                return NotFound();
            }
            //ViewData["CinemaId"] = new SelectList(_context.Cinemas, "CinemaId", "CinemaName", hall.CinemaId);
            return View(hall);
        }

        // POST: Halls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("HallId,HallName,HallCapacity,CinemaId")] Hall hall)
        {
            
            if (id != hall.HallId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existHallName = await _context.Halls.FirstOrDefaultAsync(c => c.HallId != hall.HallId && c.HallName == hall.HallName && c.CinemaId == hall.CinemaId);

                if (existHallName != null)
                {
                    ModelState.AddModelError("HallName", "Зала з такою назвою вже існує в цьому кінотеатрі");
                    return View(existHallName);
                }
                try
                {
                    _context.Update(hall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HallExists(hall.HallId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Halls", new { id = hall.CinemaId, name = _context.Cinemas.Where(c => c.CinemaId == hall.CinemaId).FirstOrDefault().CinemaName });
            }
            //ViewData["CinemaId"] = new SelectList(_context.Cinemas, "CinemaId", "CinemaName", hall.CinemaId);
            // return RedirectToAction("Index", "Halls", new { hall.CinemaId, hall.Cinema.CinemaName });
            return RedirectToAction("Index", "Halls", new { id = id, name = _context.Cinemas.Where(c => c.CinemaId == id).FirstOrDefault().CinemaName });
        }

        // GET: Halls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Halls == null)
            {
                return NotFound();
            }

            var hall = await _context.Halls
                .Include(h => h.Cinema)
                .FirstOrDefaultAsync(m => m.HallId == id);
            if (hall == null)
            {
                return NotFound();
            }

            return View(hall);
        }

        // POST: Halls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Halls == null)
            {
                return Problem("Entity set 'CinemaContext.Halls'  is null.");
            }
            var hall = await _context.Halls
                .Include(h => h.Sessions)
                .FirstOrDefaultAsync(h => h.HallId == id);

            if (hall != null)
            {
                foreach (var h in hall.Sessions)
                    _context.Remove(h);

                _context.Halls.Remove(hall);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HallExists(int id)
        {
          return (_context.Halls?.Any(e => e.HallId == id)).GetValueOrDefault();
        }

        //check if there is a cinema with the same address
       /* [HttpPost]
        public ActionResult HallNameExists(string HallName, int? id)
        {
            bool hallNameExists = false;
            ViewBag.CinemaId = id;
            var hall = _context.Halls.Where(b => b.CinemaId == id).Include(b => b.Cinema);

            //var hall = _context.Cinemas.Where(c => c.CinemaAddress == HallName).FirstOrDefault();

            //var hall = _context.Halls.Where()

            if (hall != null) hallNameExists = true;

            if (hallNameExists == true)
            {
                return Content("false");
            }
            else
            {
                return Content("true");
            }
        }*/

        //return to all halls
        public IActionResult ReturnToHalls(int cinemaId, string name)
        {
            ViewBag.CinemaId = cinemaId;
            ViewBag.CinemaName = name;
            var hallsByCinema = _context.Halls.Where(b => b.CinemaId ==cinemaId).Include(b => b.Cinema);
            return View(hallsByCinema.ToListAsync());
            //var halls = _context.Halls.Where(h => h.CinemaId == cinemaId).FirstOrDefault();
            //return View(halls);
            //return RedirectToAction("Index", "Hall", new { HallId = hallId });
        }
    }
}
