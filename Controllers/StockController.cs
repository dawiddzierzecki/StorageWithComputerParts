using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoragewithComputerParts.Data;
using StoragewithComputerParts.Models;

namespace StoragewithComputerParts.Controllers
{
    public class StockController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockController(ApplicationDbContext context)
        {
            _context = context;
            
        }


        public IActionResult Index()
        {
            var data = _context.Stocks.Include(s => s.Product).ToList();
            return View(data);
        }

    }
}
