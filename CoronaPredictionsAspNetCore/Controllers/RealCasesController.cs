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
    public class RealCasesController : Controller
    {
        private readonly PredictCoronaCasesDBContext _context;

        public RealCasesController(PredictCoronaCasesDBContext context)
        {
            _context = context;
        }

        // GET: RealCases
        public async Task<IActionResult> Index()
        {
            return View(await _context.RealCasesEachDay.OrderBy(x => x.DateOfRealCases).ToListAsync());
        }

        // GET: RealCases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realCases = await _context.RealCasesEachDay
                .FirstOrDefaultAsync(m => m.RealCaseID == id);
            if (realCases == null)
            {
                return NotFound();
            }

            return View(realCases);
        }

        // GET: RealCases/Create
        public IActionResult Create()
        {
            var list = new List<User>();
            var playerNames = _context.Players.Select(x => x.Name).ToList();
            foreach (var item in playerNames)
            {
                list.Add(new User
                {
                    Key = item,
                    Display = item
                });
            }
            var model = new RealCases();
            model.RealCasesNo = 0;
            model.DateOfRealCases = DateTime.Today;
            model.DayOfRealCases = DateTime.Today.DayOfWeek;
            return View(model);
        }

        // POST: RealCases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RealCaseID,DayOfRealCases,DateOfRealCases,RealCasesNo")] RealCases realCases)
        {
            var DateOfRealCase = realCases.DateOfRealCases.DayOfWeek;
            var DayOfRealCase = realCases.DayOfRealCases;
            if (DateOfRealCase == DayOfRealCase)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(realCases);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            else {
                ModelState.AddModelError("DateOfRealCases", "Date of Real Case does not match with its date of week");
            }
            
            return View(realCases);
        }

        // GET: RealCases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realCases = await _context.RealCasesEachDay.FindAsync(id);
            if (realCases == null)
            {
                return NotFound();
            }
            return View(realCases);
        }

        // POST: RealCases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RealCaseID,DayOfRealCases,DateOfRealCases,RealCasesNo")] RealCases realCases)
        {
            if (id != realCases.RealCaseID)
            {
                return NotFound();
            }
            var DateOfRealCase = realCases.DateOfRealCases.DayOfWeek;
            var DayOfRealCase = realCases.DayOfRealCases;
            if (DateOfRealCase == DayOfRealCase)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(realCases);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!RealCasesExists(realCases.RealCaseID))
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
            }
            else {
                ModelState.AddModelError("DateOfRealCases", "Date of Real Case does not match with its date of week");
            }
               
            return View(realCases);
        }

        // GET: RealCases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realCases = await _context.RealCasesEachDay
                .FirstOrDefaultAsync(m => m.RealCaseID == id);
            if (realCases == null)
            {
                return NotFound();
            }

            return View(realCases);
        }

        // POST: RealCases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var realCases = await _context.RealCasesEachDay.FindAsync(id);
            _context.RealCasesEachDay.Remove(realCases);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RealCasesExists(int id)
        {
            return _context.RealCasesEachDay.Any(e => e.RealCaseID == id);
        }
    }
}
