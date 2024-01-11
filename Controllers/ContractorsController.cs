using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoragewithComputerParts.Data;
using StoragewithComputerParts.Models;

namespace StoragewithComputerParts.Controllers
{
    public class ContractorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Contractors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contractors.ToListAsync());
        }

        // GET: Contractors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractor = await _context.Contractors
                .FirstOrDefaultAsync(m => m.ContractorId == id);
            if (contractor == null)
            {
                return NotFound();
            }

            return View(contractor);
        }

        // GET: Contractors/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contractors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ContractorId,ContractorName,ContractorAddress,ContractorCity,ContractorPostalCode,ContractorNIP,ContractorPhoneNumber,ContractorEmail,ContractorWebsite")] Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contractor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contractor);
        }

        // GET: Contractors/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractor = await _context.Contractors.FindAsync(id);
            if (contractor == null)
            {
                return NotFound();
            }
            return View(contractor);
        }

        // POST: Contractors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ContractorId,ContractorName,ContractorAddress,ContractorCity,ContractorPostalCode,ContractorNIP,ContractorPhoneNumber,ContractorEmail,ContractorWebsite")] Contractor contractor)
        {
            if (id != contractor.ContractorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contractor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractorExists(contractor.ContractorId))
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
            return View(contractor);
        }

        // GET: Contractors/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractor = await _context.Contractors
                .FirstOrDefaultAsync(m => m.ContractorId == id);
            if (contractor == null)
            {
                return NotFound();
            }

            return View(contractor);
        }

        // POST: Contractors/Delete/5
        [HttpPost, ActionName("ADMIN")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contractor = await _context.Contractors.FindAsync(id);
            if (contractor != null)
            {
                _context.Contractors.Remove(contractor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContractorExists(int id)
        {
            return _context.Contractors.Any(e => e.ContractorId == id);
        }
    }
}
