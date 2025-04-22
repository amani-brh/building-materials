using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using building_materials.Data;
using building_materials.Models;

namespace building_materials.Controllers
{
    public class TransportController : Controller
    {
        private readonly BuildingMaterialsContext _context;

        public TransportController(BuildingMaterialsContext context)
        {
            _context = context;
        }

        // GET: Transport
        public async Task<IActionResult> Index()
        {
            var buildingMaterialsContext = _context.Transports.Include(t => t.Materiau).Include(t => t.MoyenTransport);
            return View(await buildingMaterialsContext.ToListAsync());
        }

        // GET: Transport/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _context.Transports
                .Include(t => t.Materiau)
                .Include(t => t.MoyenTransport)
                .FirstOrDefaultAsync(m => m.IdTransport == id);
            if (transport == null)
            {
                return NotFound();
            }

            return View(transport);
        }

        // GET: Transport/Create
        public IActionResult Create()
        {
            ViewData["IdMateriau"] = new SelectList(_context.Materiaux, "IdMateriau", "Nom");
            ViewData["IdMoyenTransport"] = new SelectList(_context.FacteurTransports, "IdMoyenTransport", "Nom");
            return View();
        }

        // POST: Transport/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTransport,IdMateriau,IdMoyenTransport,DistanceKm,EmissionCO2")] Transport transport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMateriau"] = new SelectList(_context.Materiaux, "IdMateriau", "Nom", transport.IdMateriau);
            ViewData["IdMoyenTransport"] = new SelectList(_context.FacteurTransports, "IdMoyenTransport", "Nom", transport.IdMoyenTransport);
            return View(transport);
        }

        // GET: Transport/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _context.Transports.FindAsync(id);
            if (transport == null)
            {
                return NotFound();
            }
            ViewData["IdMateriau"] = new SelectList(_context.Materiaux, "IdMateriau", "Nom", transport.IdMateriau);
            ViewData["IdMoyenTransport"] = new SelectList(_context.FacteurTransports, "IdMoyenTransport", "Nom", transport.IdMoyenTransport);
            return View(transport);
        }

        // POST: Transport/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTransport,IdMateriau,IdMoyenTransport,DistanceKm,EmissionCO2")] Transport transport)
        {
            if (id != transport.IdTransport)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransportExists(transport.IdTransport))
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
            ViewData["IdMateriau"] = new SelectList(_context.Materiaux, "IdMateriau", "Nom", transport.IdMateriau);
            ViewData["IdMoyenTransport"] = new SelectList(_context.FacteurTransports, "IdMoyenTransport", "Nom", transport.IdMoyenTransport);
            return View(transport);
        }

        // GET: Transport/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transport = await _context.Transports
                .Include(t => t.Materiau)
                .Include(t => t.MoyenTransport)
                .FirstOrDefaultAsync(m => m.IdTransport == id);
            if (transport == null)
            {
                return NotFound();
            }

            return View(transport);
        }

        // POST: Transport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transport = await _context.Transports.FindAsync(id);
            if (transport != null)
            {
                _context.Transports.Remove(transport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransportExists(int id)
        {
            return _context.Transports.Any(e => e.IdTransport == id);
        }
    }
}
