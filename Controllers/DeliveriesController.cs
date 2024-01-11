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
    public class DeliveriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DeliveriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Deliveries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Deliveries.Include(d => d.Contractor).Include(d => d.Protocol);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Deliveries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries
                .Include(d => d.Contractor)
                .Include(d => d.Protocol)
                .FirstOrDefaultAsync(m => m.DeliveryId == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        [Authorize(Roles = "User")]
        // GET: Deliveries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }
            ViewData["ContractorId"] = new SelectList(_context.Contractors, "ContractorId", "ContractorEmail", delivery.ContractorId);
            ViewData["ProtocolId"] = new SelectList(_context.Protocols, "ProtocolId", "ProtocolId", delivery.ProtocolId);
            return View(delivery);
        }

        // POST: Deliveries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int id, [Bind("DeliveryId,DeliveryDate,Comment,ContractorId,ProtocolId")] Delivery delivery)
        {
            if (id != delivery.DeliveryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(delivery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryExists(delivery.DeliveryId))
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
            ViewData["ContractorId"] = new SelectList(_context.Contractors, "ContractorId", "ContractorEmail", delivery.ContractorId);
            ViewData["ProtocolId"] = new SelectList(_context.Protocols, "ProtocolId", "ProtocolId", delivery.ProtocolId);
            return View(delivery);
        }

        // GET: Deliveries/Delete/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries
                .Include(d => d.Contractor)
                .Include(d => d.Protocol)
                .FirstOrDefaultAsync(m => m.DeliveryId == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // POST: Deliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery != null)
            {
                _context.Deliveries.Remove(delivery);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryExists(int id)
        {
            return _context.Deliveries.Any(e => e.DeliveryId == id);
        }


        // GET: Delivery/Add
        [Authorize(Roles = "User")]
        public IActionResult Add()
        {
            // Pobieranie listy produktów z bazy danych (może być inna metoda pobierania)
            var products = _context.Products.ToList();
            var contractors = _context.Contractors.ToList();
            //var protocols = _context.Protocols.ToList();

            ViewBag.Contractors = contractors;
            ViewBag.Products = products;
            return View();
        }

        // POST: Delivery/Add
        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public IActionResult Add(AddDeliveryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Utwórz nowy obiekt Protocol
                var newProtocol = new Protocol
                {
                    ProtocolDate = DateTime.Now, // Ustaw datę protokołu na aktualny czas
                    Comment = "YourCommentHere", // Ustaw komentarz na odpowiednią wartość
                    ProtocolType = ProtocolType.Delivery, // Ustaw typ protokołu na odpowiednią wartość
                    ProtocolFilePath = "YourFilePathHere" // Ustaw ścieżkę pliku protokołu na odpowiednią wartość
                };


                // Utwórz nową dostawę na podstawie danych z widoku
                var newDelivery = new Delivery
                {
                    DeliveryDate = viewModel.DeliveryTime, // Ustaw datę dostawy na podstawie danych z widoku
                    Comment = viewModel.Comment, // Ustaw komentarz na podstawie danych z widoku
                    ContractorId = viewModel.ContractorId, // Ustaw identyfikator kontrahenta na podstawie danych z widoku
                    Protocol = newProtocol, // Ustaw protokół na nowy protokół
                    DeliveryProducts = viewModel.Products.Where(p => p.Quantity > 0).Select(p => new DeliveryProducts
                    {
                        ProductId = p.ProductId,
                        Quantity = p.Quantity
                    }).ToList()
                };

                // Dodaj nową dostawę do kontekstu bazy danych
                _context.Deliveries.Add(newDelivery);

                // Zapisz zmiany w celu nadania dostawie identyfikatora
                _context.SaveChanges();

                // Aktualizuj stan magazynu na podstawie nowej dostawy
                UpdateStock(newDelivery);


                return RedirectToAction("Index", "Home"); // Przekierowanie po dodaniu dostawy
            }

            // Jeśli ModelState nie jest poprawne, zwróć widok z powrotem z błędami
            // Pobieranie listy produktów z bazy danych
            var products = _context.Products.ToList();
            var contractors = _context.Contractors.ToList();
            //var protocols = _context.Protocols.ToList();

            ViewBag.Contractors = contractors;
            ViewBag.Products = products;
            return View(viewModel);
        }

        private void UpdateStock(Delivery newDelivery)
        {
            foreach (var deliveryProduct in newDelivery.DeliveryProducts)
            {
                // Sprawdź, czy produkt istnieje już w magazynie
                var stockItem = _context.Stocks.FirstOrDefault(s => s.ProductId == deliveryProduct.ProductId);

                if (stockItem != null)
                {
                    // Produkt istnieje w magazynie - zaktualizuj ilość
                    stockItem.Quantity += deliveryProduct.Quantity;
                }
                else
                {
                    // Produkt nie istnieje w magazynie - dodaj nowy wpis
                    _context.Stocks.Add(new Stock
                    {
                        ProductId = deliveryProduct.ProductId,
                        Quantity = deliveryProduct.Quantity
                    });
                }
            }
            // Zapisz zmiany w magazynie
            _context.SaveChanges();
        }
    }
}
