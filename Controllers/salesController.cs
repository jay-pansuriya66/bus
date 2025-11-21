using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bus.Models;

namespace bus.Controllers
{
    public class salesController : Controller
    {
        private readonly AppDbContext _context;

        public salesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: sales
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Sales.Include(s => s.customer).Include(s => s.vehicle);
            return View(await appDbContext.ToListAsync());
        }

        // GET: sales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales
                .Include(s => s.customer)
                .Include(s => s.vehicle)
                .FirstOrDefaultAsync(m => m.sid == id);
            if (sales == null)
            {
                return NotFound();
            }

            return View(sales);
        }

        // GET: sales/Create
        public IActionResult Create()
        {
            ViewData["cid"] = new SelectList(_context.Customers, "cid", "cid");
            ViewData["vid"] = new SelectList(_context.Vehicles, "id", "id");
            return View();
        }

        // POST: sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("sid,date,cid,vid,onroadprice,gst")] salesDto sales)
        {
            var vehicle = await _context.Vehicles
                .FirstOrDefaultAsync(m => m.id == sales.vid);

            sales.gst = 10 * vehicle.xshowroomprice / 100;
            sales.onroadprice = vehicle.xshowroomprice + 1000 + 1000 + sales.gst;

            if (ModelState.IsValid)
            {
                _context.Add(sales);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["cid"] = new SelectList(_context.Customers, "cid", "cid", sales.cid);
            ViewData["vid"] = new SelectList(_context.Vehicles, "id", "id", sales.vid);
            return View(sales);
        }

        // GET: sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales.FindAsync(id);
            if (sales == null)
            {
                return NotFound();
            }
            ViewData["cid"] = new SelectList(_context.Customers, "cid", "cid", sales.cid);
            ViewData["vid"] = new SelectList(_context.Vehicles, "id", "id", sales.vid);
            return View(sales);
        }

        // POST: sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("sid,date,cid,vid,onroadprice,gst")] sales sales)
        {
            if (id != sales.sid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sales);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!salesExists(sales.sid))
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
            ViewData["cid"] = new SelectList(_context.Customers, "cid", "cid", sales.cid);
            ViewData["vid"] = new SelectList(_context.Vehicles, "id", "id", sales.vid);
            return View(sales);
        }

        // GET: sales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = await _context.Sales
                .Include(s => s.customer)
                .Include(s => s.vehicle)
                .FirstOrDefaultAsync(m => m.sid == id);
            if (sales == null)
            {
                return NotFound();
            }

            return View(sales);
        }

        // POST: sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sales = await _context.Sales.FindAsync(id);
            if (sales != null)
            {
                _context.Sales.Remove(sales);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool salesExists(int id)
        {
            return _context.Sales.Any(e => e.sid == id);
        }
    }
}
