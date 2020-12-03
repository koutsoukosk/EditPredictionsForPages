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
    public class RegisterPlayersController : Controller
    {
        private readonly PredictCoronaCasesDBContext _context;

        public RegisterPlayersController(PredictCoronaCasesDBContext context)
        {
            _context = context;
        }

        // GET: RegisterPlayers
        public async Task<IActionResult> Index()
        {
            var returnRegistration = await _context.RegisterPlayer.ToListAsync();

            return View(returnRegistration);
        }

        // GET: RegisterPlayers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RegisterPlayers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,PlayerName,Password,confirmPassword,DateTimeRegister")] RegisterPlayer registerPlayer)
        {
            if (ModelState.IsValid)
            {
                registerPlayer.DateTimeRegister = DateTime.Now;
                _context.Add(registerPlayer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registerPlayer);
        }
      

        private bool RegisterPlayerExists(string id)
        {
            return _context.RegisterPlayer.Any(e => e.UserName == id);
        }
    }
}
