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
    public class MaterialTypeController : Controller
    {
        private readonly BuildingMaterialsContext _context;

        public MaterialTypeController(BuildingMaterialsContext context)
        {
            _context = context;
        }

        // GET: MaterialType
        public async Task<IActionResult> Index()
        {
            return View(await _context.Types.ToListAsync());
        }

        // GET: MaterialType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialType = await _context.Types
                .FirstOrDefaultAsync(m => m.IdType == id);
            if (materialType == null)
            {
                return NotFound();
            }

            return View(materialType);
        }

        // GET: MaterialType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaterialType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdType,NomType")] MaterialType materialType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materialType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(materialType);
        }

        // GET: MaterialType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialType = await _context.Types.FindAsync(id);
            if (materialType == null)
            {
                return NotFound();
            }
            return View(materialType);
        }

        // POST: MaterialType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdType,NomType")] MaterialType materialType)
        {
            if (id != materialType.IdType)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materialType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialTypeExists(materialType.IdType))
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
            return View(materialType);
        }

        // GET: MaterialType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var materialType = await _context.Types
                .FirstOrDefaultAsync(m => m.IdType == id);
            if (materialType == null)
            {
                return NotFound();
            }

            return View(materialType);
        }

        // POST: MaterialType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var materialType = await _context.Types.FindAsync(id);
            if (materialType != null)
            {
                _context.Types.Remove(materialType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialTypeExists(int id)
        {
            return _context.Types.Any(e => e.IdType == id);
        }
    }
}
