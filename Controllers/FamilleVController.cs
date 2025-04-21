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
    public class FamilleVController : Controller
    {
        private readonly BuildingMaterialsContext _context;

        public FamilleVController(BuildingMaterialsContext context)
        {
            _context = context;
        }

        // GET: FamilleV
        public async Task<IActionResult> Index()
        {
            var buildingMaterialsContext = _context.Familles.Include(f => f.Type);
            return View(await buildingMaterialsContext.ToListAsync());
        }

        // GET: FamilleV/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var famille = await _context.Familles
                .Include(f => f.Type)
                .FirstOrDefaultAsync(m => m.IdFamille == id);
            if (famille == null)
            {
                return NotFound();
            }

            return View(famille);
        }

        // GET: FamilleV/Create
        public IActionResult Create()
        {
            ViewData["IdType"] = new SelectList(_context.Types, "IdType", "NomType");
            return View();
        }

        // POST: FamilleV/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFamille,NomFamille,IdType")] Famille famille)
        {
            if (ModelState.IsValid)
            {
                _context.Add(famille);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdType"] = new SelectList(_context.Types, "IdType", "NomType", famille.IdType);
            return View(famille);
        }

        // GET: FamilleV/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var famille = await _context.Familles.FindAsync(id);
            if (famille == null)
            {
                return NotFound();
            }
            ViewData["IdType"] = new SelectList(_context.Types, "IdType", "NomType", famille.IdType);
            return View(famille);
        }

        // POST: FamilleV/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFamille,NomFamille,IdType")] Famille famille)
        {
            if (id != famille.IdFamille)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(famille);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FamilleExists(famille.IdFamille))
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
            ViewData["IdType"] = new SelectList(_context.Types, "IdType", "NomType", famille.IdType);
            return View(famille);
        }

        // GET: FamilleV/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var famille = await _context.Familles
                .Include(f => f.Type)
                .FirstOrDefaultAsync(m => m.IdFamille == id);
            if (famille == null)
            {
                return NotFound();
            }

            return View(famille);
        }

        // POST: FamilleV/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var famille = await _context.Familles.FindAsync(id);
            if (famille != null)
            {
                _context.Familles.Remove(famille);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FamilleExists(int id)
        {
            return _context.Familles.Any(e => e.IdFamille == id);
        }
    }
}
