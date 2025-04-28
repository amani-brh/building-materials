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
    public class CaracteristiqueEnvironnementaleController : Controller
    {
        private readonly BuildingMaterialsContext _context;

        public CaracteristiqueEnvironnementaleController(BuildingMaterialsContext context)
        {
            _context = context;
        }

        // GET: CaracteristiqueEnvironnementale
        public async Task<IActionResult> Index()
        {
            var buildingMaterialsContext = _context.CaracteristiqueEnvironnementales.Include(c => c.Materiau);
            return View(await buildingMaterialsContext.ToListAsync());
        }

        // GET: CaracteristiqueEnvironnementale/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caracteristiqueEnvironnementale = await _context.CaracteristiqueEnvironnementales
                .Include(c => c.Materiau)
                .FirstOrDefaultAsync(m => m.IdCaract == id);
            if (caracteristiqueEnvironnementale == null)
            {
                return NotFound();
            }

            return View(caracteristiqueEnvironnementale);
        }

        // GET: CaracteristiqueEnvironnementale/Create
        public IActionResult Create()
        {
            ViewData["IdMateriau"] = new SelectList(_context.Materiaux, "IdMateriau", "Nom");
            return View();
        }

        // POST: CaracteristiqueEnvironnementale/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCaract,IdMateriau,EmissionCO2,PollutionEau,PollutionAir,ConsommationEau,UniteFonctionnelle,UtilisationNetteEauDouce")] CaracteristiqueEnvironnementale caracteristiqueEnvironnementale)
        {
          //  if (ModelState.IsValid)
           // {
                _context.Add(caracteristiqueEnvironnementale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
           /* }
            ViewData["IdMateriau"] = new SelectList(_context.Materiaux, "IdMateriau", "Nom", caracteristiqueEnvironnementale.IdMateriau);
            return View(caracteristiqueEnvironnementale);*/
        }

        // GET: CaracteristiqueEnvironnementale/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caracteristiqueEnvironnementale = await _context.CaracteristiqueEnvironnementales.FindAsync(id);
            if (caracteristiqueEnvironnementale == null)
            {
                return NotFound();
            }
            ViewData["IdMateriau"] = new SelectList(_context.Materiaux, "IdMateriau", "Nom", caracteristiqueEnvironnementale.IdMateriau);
            return View(caracteristiqueEnvironnementale);
        }

        // POST: CaracteristiqueEnvironnementale/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCaract,IdMateriau,EmissionCO2,PollutionEau,PollutionAir,ConsommationEau,UniteFonctionnelle,UtilisationNetteEauDouce")] CaracteristiqueEnvironnementale caracteristiqueEnvironnementale)
        {
            if (id != caracteristiqueEnvironnementale.IdCaract)
            {
                return NotFound();
            }

           // if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(caracteristiqueEnvironnementale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CaracteristiqueEnvironnementaleExists(caracteristiqueEnvironnementale.IdCaract))
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
          //  ViewData["IdMateriau"] = new SelectList(_context.Materiaux, "IdMateriau", "Nom", caracteristiqueEnvironnementale.IdMateriau);
          //  return View(caracteristiqueEnvironnementale);
        }

        // GET: CaracteristiqueEnvironnementale/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var caracteristiqueEnvironnementale = await _context.CaracteristiqueEnvironnementales
                .Include(c => c.Materiau)
                .FirstOrDefaultAsync(m => m.IdCaract == id);
            if (caracteristiqueEnvironnementale == null)
            {
                return NotFound();
            }

            return View(caracteristiqueEnvironnementale);
        }

        // POST: CaracteristiqueEnvironnementale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var caracteristiqueEnvironnementale = await _context.CaracteristiqueEnvironnementales.FindAsync(id);
            if (caracteristiqueEnvironnementale != null)
            {
                _context.CaracteristiqueEnvironnementales.Remove(caracteristiqueEnvironnementale);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CaracteristiqueEnvironnementaleExists(int id)
        {
            return _context.CaracteristiqueEnvironnementales.Any(e => e.IdCaract == id);
        }
    }
}
