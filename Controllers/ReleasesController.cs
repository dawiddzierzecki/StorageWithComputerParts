using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoragewithComputerParts.Data;
using StoragewithComputerParts.Data.Enums;
using StoragewithComputerParts.Models;
using StoragewithComputerParts.ViewModels;

namespace StoragewithComputerParts.Controllers
{
    public class ReleasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReleasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Releases
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Releases.Include(r => r.Contractor).Include(r => r.Protocol);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Releases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var release = await _context.Releases
                .Include(r => r.Contractor)
                .Include(r => r.Protocol)
                .FirstOrDefaultAsync(m => m.ReleaseId == id);
            if (release == null)
            {
                return NotFound();
            }

            return View(release);
        }


        // GET: Releases/Edit/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var release = await _context.Releases.FindAsync(id);
            if (release == null)
            {
                return NotFound();
            }
            ViewData["ContractorId"] = new SelectList(_context.Contractors, "ContractorId", "ContractorEmail", release.ContractorId);
            ViewData["ProtocolId"] = new SelectList(_context.Protocols, "ProtocolId", "ProtocolId", release.ProtocolId);
            return View(release);
        }

        // POST: Releases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int id, [Bind("ReleaseId,Quantity,Comment,ReleaseDate,ContractorId,ProtocolId")] Release release)
        {
            if (id != release.ReleaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(release);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReleaseExists(release.ReleaseId))
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
            ViewData["ContractorId"] = new SelectList(_context.Contractors, "ContractorId", "ContractorEmail", release.ContractorId);
            ViewData["ProtocolId"] = new SelectList(_context.Protocols, "ProtocolId", "ProtocolId", release.ProtocolId);
            return View(release);
        }

        // GET: Releases/Delete/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var release = await _context.Releases
                .Include(r => r.Contractor)
                .Include(r => r.Protocol)
                .FirstOrDefaultAsync(m => m.ReleaseId == id);
            if (release == null)
            {
                return NotFound();
            }

            return View(release);
        }

        // POST: Releases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var release = await _context.Releases.FindAsync(id);
            if (release != null)
            {
                _context.Releases.Remove(release);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReleaseExists(int id)
        {
            return _context.Releases.Any(e => e.ReleaseId == id);
        }

        //GET: Release/Add
        [Authorize(Roles = "User")]
        public IActionResult Add()
        {
            // Pobieranie listy produktów z bazy danych
            var products = _context.Products.ToList();
            var contractors = _context.Contractors.ToList();
            //var protocols = _context.Protocols.ToList();

            ViewBag.Contractors = contractors;
            ViewBag.Products = products;
            ViewBag.StockQuantities = _context.Stocks.ToList();
            return View();
        }

        // POST: Delivery/Add
        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public IActionResult Add(AddDeliveryViewModel viewModel)
        {
            // Pobierz stan magazynu dla każdego produktu
            var stockQuantities = _context.Stocks.ToDictionary(s => s.ProductId, s => s.Quantity);
            
            // Sprawdź, czy ilość produktów w wydaniu nie przekracza dostępnej ilości w magazynie
            foreach (var releaseProduct in viewModel.Products)
            {
                if (releaseProduct.Quantity > stockQuantities.GetValueOrDefault(releaseProduct.ProductId, 0))
                {
                    //var productName = ViewBag.Products.ContainsKey(releaseProduct.ProductId) ? ViewBag.Products[releaseProduct.ProductId] : "Unknown Product";

                    //ModelState.AddModelError($"ReleaseProducts[{releaseProduct.ProductId}].Quantity",
                    //    $"Quantity cannot be greater than available stock for {releaseProduct.ProductId}");

                    ModelState.AddModelError($"Products[{releaseProduct.ProductId}].Quantity",
                        $"Quantity cannot be greater than available stock for Product ID {releaseProduct.ProductId}. Available quantity: {stockQuantities.GetValueOrDefault(releaseProduct.ProductId, 0)}");
                    ViewBag.InvalidQuantityMessage = $"Quantity cannot be greater than available stock for Product:  {releaseProduct.ProductId}. Available quantity: {stockQuantities.GetValueOrDefault(releaseProduct.ProductId, 0)}";

                }
            }

            if (ModelState.IsValid)
            {
                // Utwórz nowy obiekt Protocol
                var newProtocol = new Protocol
                {
                    ProtocolDate = DateTime.Now, // Ustaw datę protokołu na aktualny czas
                    Comment = "YourCommentHere", // Ustaw komentarz na odpowiednią wartość
                    ProtocolType = ProtocolType.Release, // Ustaw typ protokołu na odpowiednią wartość
                    ProtocolFilePath = "YourFilePathHere" // Ustaw ścieżkę pliku protokołu na odpowiednią wartość
                };


                // Utwórz nową dostawę na podstawie danych z widoku
                var newRelease = new Release
                {
                    ReleaseDate = viewModel.DeliveryTime, // Ustaw datę dostawy na podstawie danych z widoku
                    Comment = viewModel.Comment, // Ustaw komentarz na podstawie danych z widoku
                    ContractorId = viewModel.ContractorId, // Ustaw identyfikator kontrahenta na podstawie danych z widoku
                    Protocol = newProtocol, // Ustaw protokół na nowy protokół
                    ReleaseProducts = viewModel.Products.Where(p => p.Quantity > 0).Select(p => new ReleaseProducts
                    {
                        ProductId = p.ProductId,
                        Quantity = p.Quantity
                    }).ToList()
                };

                // Dodaj nową dostawę do kontekstu bazy danych
                _context.Releases.Add(newRelease);

                // Zapisz zmiany w celu nadania dostawie identyfikatora
                _context.SaveChanges();

                // Aktualizuj stan magazynu na podstawie nowej dostawy
                UpdateStock(newRelease);


                return RedirectToAction("Index", "Home"); // Przekierowanie po dodaniu dostawy
            }

            // Jeśli ModelState nie jest poprawne, zwróć widok z powrotem z błędami
            // Pobieranie listy produktów z bazy danych
            var products = _context.Products.ToList();
            var contractors = _context.Contractors.ToList();
            //var protocols = _context.Protocols.ToList();

            ViewBag.Contractors = contractors;
            ViewBag.Products = products;
            ViewBag.StockQuantities = _context.Stocks.ToList();
            return View(viewModel);
        }

        private void UpdateStock(Release newDelivery)
        {
            foreach (var releaseProduct in newDelivery.ReleaseProducts)
            {
                // Sprawdź, czy produkt istnieje już w magazynie
                var stockItem = _context.Stocks.FirstOrDefault(s => s.ProductId == releaseProduct.ProductId);

                if (stockItem != null)
                {
                    // Produkt istnieje w magazynie - zaktualizuj ilość
                    stockItem.Quantity -= releaseProduct.Quantity;
                }
            }
            // Zapisz zmiany w magazynie
            _context.SaveChanges();
        }

    }
}
