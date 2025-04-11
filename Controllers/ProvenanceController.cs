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
    public class ProvenanceController : Controller
    {
        private readonly BuildingMaterialsContext _context;

        public ProvenanceController(BuildingMaterialsContext context)
        {
            _context = context;
        }

        // GET: Provenance
        public async Task<IActionResult> Index()
        {
            return View(await _context.Provenances.ToListAsync());
        }

        // GET: Provenance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provenance = await _context.Provenances
                .FirstOrDefaultAsync(m => m.IdProvenance == id);
            if (provenance == null)
            {
                return NotFound();
            }

            return View(provenance);
        }

        // GET: Provenance/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Provenance/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProvenance,Pays,Region,DistanceKm")] Provenance provenance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provenance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(provenance);
        }

        // GET: Provenance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provenance = await _context.Provenances.FindAsync(id);
            if (provenance == null)
            {
                return NotFound();
            }
            return View(provenance);
        }

        // POST: Provenance/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProvenance,Pays,Region,DistanceKm")] Provenance provenance)
        {
            if (id != provenance.IdProvenance)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(provenance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvenanceExists(provenance.IdProvenance))
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
            return View(provenance);
        }

        // GET: Provenance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provenance = await _context.Provenances
                .FirstOrDefaultAsync(m => m.IdProvenance == id);
            if (provenance == null)
            {
                return NotFound();
            }

            return View(provenance);
        }

        // POST: Provenance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var provenance = await _context.Provenances.FindAsync(id);
            if (provenance != null)
            {
                _context.Provenances.Remove(provenance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProvenanceExists(int id)
        {
            return _context.Provenances.Any(e => e.IdProvenance == id);
        }
    }
}
