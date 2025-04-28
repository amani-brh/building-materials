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
    public class MateriauController : Controller
    {
        private readonly BuildingMaterialsContext _context;

        public MateriauController(BuildingMaterialsContext context)
        {
            _context = context;
        }

        // GET: Materiau
        public async Task<IActionResult> Index()
        {
            var buildingMaterialsContext = _context.Materiaux.Include(m => m.Famille).Include(m => m.Producteur).Include(m => m.Provenance);
            return View(await buildingMaterialsContext.ToListAsync());
        }

        // GET: Materiau/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiau = await _context.Materiaux
                .Include(m => m.Famille)
                .Include(m => m.Producteur)
                .Include(m => m.Provenance)
                .FirstOrDefaultAsync(m => m.IdMateriau == id);
            if (materiau == null)
            {
                return NotFound();
            }

            return View(materiau);
        }

        // GET: Materiau/Create
        public IActionResult Create()
        {
            ViewData["IdFamille"] = new SelectList(_context.Familles, "IdFamille", "NomFamille");
            ViewData["IdProducteur"] = new SelectList(_context.Producteurs, "IdProducteur", "Nom");
            ViewData["IdProvenance"] = new SelectList(_context.Provenances, "IdProvenance", "Pays");
            return View();
        }

        // POST: Materiau/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMateriau,Nom,Origine,IdFamille,IdProvenance,IdProducteur")] Materiau materiau)
        {
            
                _context.Add(materiau);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            //ViewData["IdFamille"] = new SelectList(_context.Familles, "IdFamille", "NomFamille", materiau.IdFamille);
            //ViewData["IdProducteur"] = new SelectList(_context.Producteurs, "IdProducteur", "LieuProduction", materiau.IdProducteur);
            //
            //ViewData["IdProvenance"] = new SelectList(_context.Provenances, "IdProvenance", "Pays", materiau.IdProvenance);
            //return View(materiau);
        }

        // GET: Materiau/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiau = await _context.Materiaux.FindAsync(id);
            if (materiau == null)
            {
                return NotFound();
            }
            ViewData["IdFamille"] = new SelectList(_context.Familles, "IdFamille", "NomFamille", materiau.IdFamille);
            ViewData["IdProducteur"] = new SelectList(_context.Producteurs, "IdProducteur", "Nom", materiau.IdProducteur);
            ViewData["IdProvenance"] = new SelectList(_context.Provenances, "IdProvenance", "Pays", materiau.IdProvenance);
            return View(materiau);
        }

        // POST: Materiau/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMateriau,Nom,Origine,IdFamille,IdProvenance,IdProducteur")] Materiau materiau)
        {
            if (id != materiau.IdMateriau)
            {
                return NotFound();
            }

           // if (ModelState.IsValid)
           // {
                try
                {
                    _context.Update(materiau);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MateriauExists(materiau.IdMateriau))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
           // }
           // ViewData["IdFamille"] = new SelectList(_context.Familles, "IdFamille", "NomFamille", materiau.IdFamille);
           // ViewData["IdProducteur"] = new SelectList(_context.Producteurs, "IdProducteur", "LieuProduction", materiau.IdProducteur);
           // ViewData["IdProvenance"] = new SelectList(_context.Provenances, "IdProvenance", "Pays", materiau.IdProvenance);
           // return View(materiau);
        }

        // GET: Materiau/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materiau = await _context.Materiaux
                .Include(m => m.Famille)
                .Include(m => m.Producteur)
                .Include(m => m.Provenance)
                .FirstOrDefaultAsync(m => m.IdMateriau == id);
            if (materiau == null)
            {
                return NotFound();
            }

            return View(materiau);
        }

        // POST: Materiau/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materiau = await _context.Materiaux.FindAsync(id);
            if (materiau != null)
            {
                _context.Materiaux.Remove(materiau);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MateriauExists(int id)
        {
            return _context.Materiaux.Any(e => e.IdMateriau == id);
        }
    }
}
