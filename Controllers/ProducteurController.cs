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
    public class ProducteurController : Controller
    {
        private readonly BuildingMaterialsContext _context;

        public ProducteurController(BuildingMaterialsContext context)
        {
            _context = context;
        }

        // GET: Producteur
        public async Task<IActionResult> Index()
        {
            return View(await _context.Producteurs.ToListAsync());
        }

        // GET: Producteur/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producteur = await _context.Producteurs
                .FirstOrDefaultAsync(m => m.IdProducteur == id);
            if (producteur == null)
            {
                return NotFound();
            }

            return View(producteur);
        }

        // GET: Producteur/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producteur/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducteur,Nom,LieuProduction")] Producteur producteur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producteur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producteur);
        }

        // GET: Producteur/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producteur = await _context.Producteurs.FindAsync(id);
            if (producteur == null)
            {
                return NotFound();
            }
            return View(producteur);
        }

        // POST: Producteur/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducteur,Nom,LieuProduction")] Producteur producteur)
        {
            if (id != producteur.IdProducteur)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producteur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProducteurExists(producteur.IdProducteur))
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
            return View(producteur);
        }

        // GET: Producteur/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producteur = await _context.Producteurs
                .FirstOrDefaultAsync(m => m.IdProducteur == id);
            if (producteur == null)
            {
                return NotFound();
            }

            return View(producteur);
        }

        // POST: Producteur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producteur = await _context.Producteurs.FindAsync(id);
            if (producteur != null)
            {
                _context.Producteurs.Remove(producteur);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProducteurExists(int id)
        {
            return _context.Producteurs.Any(e => e.IdProducteur == id);
        }
    }
}
