using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoronaPredictionsAspNetCore.Models;
using CoronaPredictionsAspNetCore.DataAccessLayer;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Authorization;

namespace CoronaPredictionsAspNetCore.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class PredictionsController : Controller
    {
        private readonly PredictCoronaCasesDBContext _context;
        private readonly IPlayersRepo _repository;

        public PredictionsController(PredictCoronaCasesDBContext context, IPlayersRepo repository)
        {
            _context = context;
            _repository = repository;
        }
       
        // GET: Predictions
        public async Task<IActionResult> Index( DateTime searchDate, int page)
        {
           
            var pageIndex = (page == 0) ? 1 : page;              
            var pageQuery =  await _context.Prediction.OrderBy(x => x.DateOfPrediction).ToListAsync();
            if (searchDate != DateTime.MinValue) {
                pageQuery = pageQuery.Where(c => c.DateOfPrediction.Date == searchDate.Date).OrderBy(x => x.DateOfPrediction).ToList();
            }
           
            foreach (var item in pageQuery)
            {
                item.AuthenticatedUserName = _repository.authenticatedPlayerNameByUserEmail(User.Identity.Name);
                item.AuthenticatedUserEmail = User.Identity.Name;
            }

            return View(pageQuery);
        }
       
        // GET: Predictions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predictions = await _context.Prediction
                .FirstOrDefaultAsync(m => m.PredictionID == id);
            if (predictions == null)
            {
                return NotFound();
            }

            return View(predictions);
        }
        public IEnumerable<Player> displayData { get; set; }
        public async Task OnGet() {
            displayData = await _context.Players.ToListAsync();
        }
        // GET: Predictions/Create
        public IActionResult Create()
        {
            var list = new List<User>();
            if (User.IsInRole("Admin"))
            {
                var playerNames = _context.Players.OrderBy(y => y.Name).Select(x => x.Name).ToList();
                foreach (var item in playerNames)
                {
                    list.Add(new User
                    {
                        Key = item,
                        Display = item
                    });
                }
            }
            else {
                list.Add(new User
                {
                    Key = _repository.authenticatedPlayerNameByUserEmail(User.Identity.Name),
                    Display = _repository.authenticatedPlayerNameByUserEmail(User.Identity.Name)
            });
            }
            var model = new Predictions();
            model.CasesOfPrediction = 0;
            model.DateOfPrediction =  DateTime.Parse(DateTime.Now.ToString("dddd, dd MMMM yyyy"));
            model.DayOfPrediction = DateTime.Today.DayOfWeek;
            if (list.Count()==0) {
                list.Add(new User
                {
                    Key = "",
                    Display = ""
                });
            }
            model.PlayersList = new SelectList(list, "Key", "Display");
            return View(model);
        }

        // POST: Predictions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PredictionID,DayOfPrediction,DateOfPrediction,PlayerName,CasesOfPrediction")] Predictions predictions)
        {
            var DateOfRealCase = predictions.DateOfPrediction.DayOfWeek;
            var DayOfRealCase = predictions.DayOfPrediction;
            if (DateOfRealCase == DayOfRealCase)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(predictions);
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        if (ex.InnerException.Message.Contains("IndexPredictionDate"))
                        {
                            return View("UniquePredictionDate", predictions);
                        }

                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                ModelState.AddModelError("DateOfPrediction", "Date of Prediction does not match with its date of week");
            }
            var list = new List<User>();
            var playerNames = _context.Players.OrderBy(y => y.Name).Select(x => x.Name).ToList();
            foreach (var item in playerNames)
            {
                list.Add(new User
                {
                    Key = item,
                    Display = item
                });
            }
            predictions.PlayersList = new SelectList(list, "Key", "Display");
            return View(predictions);
        }

        // GET: Predictions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predictions = await _context.Prediction.FindAsync(id);
            if (predictions == null)
            {
                return NotFound();
            }
            var list = new List<User>();
         
            if (User.IsInRole("Admin"))
            {
                var playerNames = _context.Players.OrderBy(y => y.Name).Select(x => x.Name).ToList();
                foreach (var item in playerNames)
                {
                    list.Add(new User
                    {
                        Key = item,
                        Display = item
                    });
                }
            }
            else
            {
                list.Add(new User
                {
                    Key = _repository.authenticatedPlayerNameByUserEmail(User.Identity.Name),
                    Display = _repository.authenticatedPlayerNameByUserEmail(User.Identity.Name)
                });
            }
         
            predictions.PlayersList = new SelectList(list, "Key", "Display");
            return View(predictions);
        }

        // POST: Predictions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PredictionID,DayOfPrediction,DateOfPrediction,PlayerName,CasesOfPrediction")] Predictions predictions)
        {
            if (id != predictions.PredictionID)
            {
                return NotFound();
            }
            var DateOfRealCase = predictions.DateOfPrediction.DayOfWeek;
            var DayOfRealCase = predictions.DayOfPrediction;
            if (DateOfRealCase == DayOfRealCase)
            {
                if (ModelState.IsValid)
                {                   
                    try
                    {
                        _context.Update(predictions);
                        try
                        {
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            if (ex.InnerException.Message.Contains("IndexPredictionDate"))
                            {
                                return View("UniquePredictionDate", predictions);
                            }
                        }
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PredictionsExists(predictions.PredictionID))
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
            else
            {
                ModelState.AddModelError("DateOfPrediction", "Date of Prediction does not match with its date of week");
            }
            var list = new List<User>();
            var playerNames = _context.Players.OrderBy(y => y.Name).Select(x => x.Name).ToList();
            foreach (var item in playerNames)
            {
                list.Add(new User
                {
                    Key = item,
                    Display = item
                });
            }
            predictions.PlayersList = new SelectList(list, "Key", "Display");
            return View(predictions);
        }

        // GET: Predictions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predictions = await _context.Prediction
                .FirstOrDefaultAsync(m => m.PredictionID == id);
            if (predictions == null)
            {
                return NotFound();
            }

            return View(predictions);
        }

        // POST: Predictions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var predictions = await _context.Prediction.FindAsync(id);
            _context.Prediction.Remove(predictions);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PredictionsExists(int id)
        {
            return _context.Prediction.Any(e => e.PredictionID == id);
        }
    }
}
