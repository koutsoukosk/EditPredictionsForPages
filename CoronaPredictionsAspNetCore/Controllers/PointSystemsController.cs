using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoronaPredictionsAspNetCore.Models;

namespace CoronaPredictionsAspNetCore.Controllers
{
    public class PointSystemsController : Controller
    {
        private readonly PredictCoronaCasesDBContext _context;

        public PointSystemsController(PredictCoronaCasesDBContext context)
        {
            _context = context;
        }

        // GET: PointSystems
        public async Task<IActionResult> Index()
        {
            return View(await _context.PointSystem.ToListAsync());
        }

        // GET: PointSystems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointSystem = await _context.PointSystem
                .FirstOrDefaultAsync(m => m.PointSystemID == id);
            if (pointSystem == null)
            {
                return NotFound();
            }

            return View(pointSystem);
        }

        // GET: PointSystems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PointSystems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PointSystemID,CategoryName,MoreOrEqualThanDif,LessOrEqualThanDif,CategoryPoints")] PointSystem pointSystem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pointSystem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pointSystem);
        }

        // GET: PointSystems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointSystem = await _context.PointSystem.FindAsync(id);
            if (pointSystem == null)
            {
                return NotFound();
            }
            return View(pointSystem);
        }

        // POST: PointSystems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PointSystemID,CategoryName,MoreOrEqualThanDif,LessOrEqualThanDif,CategoryPoints")] PointSystem pointSystem)
        {
            if (id != pointSystem.PointSystemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pointSystem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PointSystemExists(pointSystem.PointSystemID))
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
            return View(pointSystem);
        }

        // GET: PointSystems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pointSystem = await _context.PointSystem
                .FirstOrDefaultAsync(m => m.PointSystemID == id);
            if (pointSystem == null)
            {
                return NotFound();
            }

            return View(pointSystem);
        }

        // POST: PointSystems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pointSystem = await _context.PointSystem.FindAsync(id);
            _context.PointSystem.Remove(pointSystem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PointSystemExists(int id)
        {
            return _context.PointSystem.Any(e => e.PointSystemID == id);
        }
    }
}
