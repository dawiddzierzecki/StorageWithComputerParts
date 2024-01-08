using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoragewithComputerParts.Data;
using StoragewithComputerParts.Models;

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
            var applicationDbContext = _context.Deliveries.Include(d => d.Protocol);
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
                .Include(d => d.Protocol)
                .FirstOrDefaultAsync(m => m.DeliveryId == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // GET: Deliveries/Create
        public IActionResult Create()
        {
            ViewData["ProtocolId"] = new SelectList(_context.Set<Protocol>(), "ProtocolId", "ProtocolId");
            ViewBag.Products = _context.Products.ToList();

            var contractors = _context.Contractors.ToList();

            var contractorList = contractors.Select(c => new SelectListItem
            {
                Text = c.ContractorName,
                Value = c.ContractorId.ToString()
            }).ToList();

            ViewBag.ContractorList = contractorList;

            return View();
        }

        // POST: Deliveries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeliveryId,DeliveryDate,Comment,ContractorId,ProtocolId")] Delivery delivery, List<int> productIds, List<int> quantities)
        {
            if (ModelState.IsValid && productIds != null && quantities != null && productIds.Count == quantities.Count)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Add(delivery);
                        await _context.SaveChangesAsync();

                        for (int i = 0; i < productIds.Count; i++)
                        {
                            var productId = productIds[i];
                            var quantity = quantities[i];

                            // Tworzenie wpisu w tabeli DeliveryProducts
                            var deliveryProduct = new DeliveryProducts
                            {
                                DeliveryId = delivery.DeliveryId,
                                ProductId = productId,
                                Quantity = quantity
                            };

                            _context.Add(deliveryProduct);

                            // Aktualizacja ilości w tabeli Stock
                            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.ProductId == productId);
                            if (stock != null)
                            {
                                stock.Quantity += quantity;
                                _context.Update(stock);
                            }
                            else
                            {
                                // Jeśli produkt nie istnieje w magazynie, możesz utworzyć nowy wpis w Stock
                                var newStock = new Stock
                                {
                                    ProductId = productId,
                                    Quantity = quantity
                                };
                                _context.Add(newStock);
                            }
                        }

                        await _context.SaveChangesAsync();
                        transaction.Commit();

                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception)
                    {
                        // Obsługa błędu - cofnięcie transakcji w przypadku niepowodzenia
                        transaction.Rollback();
                        ModelState.AddModelError(string.Empty, "Failed to create delivery.");
                    }
                }
            }

            // Jeśli coś poszło nie tak, zwróć widok z błędem
            ViewData["ProtocolId"] = new SelectList(_context.Set<Protocol>(), "ProtocolId", "ProtocolId", delivery.ProtocolId);
            ViewBag.Products = _context.Products.ToList();
            //ViewBag.Contractors = _context.Contractors.ToList();
            return View(delivery);
        }



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
            ViewData["ProtocolId"] = new SelectList(_context.Set<Protocol>(), "ProtocolId", "ProtocolId", delivery.ProtocolId);
            return View(delivery);
        }

        // POST: Deliveries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            ViewData["ProtocolId"] = new SelectList(_context.Set<Protocol>(), "ProtocolId", "ProtocolId", delivery.ProtocolId);
            return View(delivery);
        }

        // GET: Deliveries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries
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
    }
}
