using HotelReservationSystem.Data;
using HotelReservationSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationSystem.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _context.Rooms.ToListAsync();
            return View(rooms);
        }
    }
}
