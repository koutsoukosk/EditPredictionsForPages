using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaPredictionsAspNetCore.DataAccessLayer;
using CoronaPredictionsAspNetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoronaPredictionsAspNetCore.Controllers
{
    [Authorize(Roles = "Admin,User")]
    [Route("[controller]")]
    [ApiController]
    public class PlayerOfDayController : Controller
    {
     
        private readonly IPredictionsRepo _repository;
        public PlayerOfDayController(IPredictionsRepo repository)
        {
            _repository = repository;
        }
        // GET: PlayerOfDay
        [HttpGet]
        public ActionResult<IEnumerable<PlayerOfDay>> PlayersOfTheDay()
        {         
                var bestPlayersByDate = _repository.BestPlayersByDate();        
            return View(bestPlayersByDate);
        }
    }
}