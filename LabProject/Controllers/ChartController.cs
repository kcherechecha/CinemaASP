using LabProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LabProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly CinemaContext _context;
        public ChartController(CinemaContext context)
        {
            _context = context;
        }

        [HttpGet("JsonDataSessions")]
        public JsonResult JsonDataSessions()
        {
            var halls = _context.Halls.Include(h => h.Sessions).ToList();
            List<object> hallSession = new List<object>();
            hallSession.Add(new[] {"Зала", "Кількість сеансів"});
            foreach (var c in halls)
            {
                hallSession.Add(new object[] { c.HallName, c.Sessions.Count });
            }
            return new JsonResult(hallSession);
        }

        [HttpGet("JsonDataTeam")]
        public JsonResult JsonDataTeam()
        {
            var movies = _context.Movies.Include(h => h.MovieCasts).ToList();
            List<object> team = new List<object>();
            team.Add(new[] { "Фільм", "Кількість членів команди" });
            foreach (var c in movies)
            {
                team.Add(new object[] { c.MovieName, c.MovieCasts.Count });
            }
            return new JsonResult(team);
        }
    }
}
