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
    public class FacteurTransportController : Controller
    {
        private readonly BuildingMaterialsContext _context;

        public FacteurTransportController(BuildingMaterialsContext context)
        {
            _context = context;
        }

        // GET: FacteurTransport
        public async Task<IActionResult> Index()
        {
            return View(await _context.FacteurTransports.ToListAsync());
        }

        // GET: FacteurTransport/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facteurTransport = await _context.FacteurTransports
                .FirstOrDefaultAsync(m => m.IdMoyenTransport == id);
            if (facteurTransport == null)
            {
                return NotFound();
            }

            return View(facteurTransport);
        }

        // GET: FacteurTransport/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FacteurTransport/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMoyenTransport,Nom,FacteurCO2")] FacteurTransport facteurTransport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facteurTransport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facteurTransport);
        }

        // GET: FacteurTransport/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facteurTransport = await _context.FacteurTransports.FindAsync(id);
            if (facteurTransport == null)
            {
                return NotFound();
            }
            return View(facteurTransport);
        }

        // POST: FacteurTransport/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMoyenTransport,Nom,FacteurCO2")] FacteurTransport facteurTransport)
        {
            if (id != facteurTransport.IdMoyenTransport)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facteurTransport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacteurTransportExists(facteurTransport.IdMoyenTransport))
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
            return View(facteurTransport);
        }

        // GET: FacteurTransport/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facteurTransport = await _context.FacteurTransports
                .FirstOrDefaultAsync(m => m.IdMoyenTransport == id);
            if (facteurTransport == null)
            {
                return NotFound();
            }

            return View(facteurTransport);
        }

        // POST: FacteurTransport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facteurTransport = await _context.FacteurTransports.FindAsync(id);
            if (facteurTransport != null)
            {
                _context.FacteurTransports.Remove(facteurTransport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacteurTransportExists(int id)
        {
            return _context.FacteurTransports.Any(e => e.IdMoyenTransport == id);
        }
    }
}
