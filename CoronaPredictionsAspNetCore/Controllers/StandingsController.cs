using CoronaPredictionsAspNetCore.DataAccessLayer;
using CoronaPredictionsAspNetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaPredictionsAspNetCore.Controllers
{
    [Authorize(Roles = "Admin,User")]
    [Route("[controller]")]
    [ApiController]
    public class StandingsController :Controller
    {
        private readonly IPredictionsRepo _repository;
        private readonly IStandingsRepo _StandingsRepository;
        private readonly IRealCasesRepo _RealCasesRepository;
        private readonly ISystemPointsRepo _SystemPointsRepository;
        public StandingsController(IPredictionsRepo repository, IStandingsRepo standingsRepo, 
            IRealCasesRepo realCasesRepo, ISystemPointsRepo systemPointsRepo )
        {
            _repository = repository;
            _StandingsRepository = standingsRepo;
            _RealCasesRepository = realCasesRepo;
            _SystemPointsRepository = systemPointsRepo;
        }
        // GET: Standings
        [HttpGet]
        public ActionResult<IEnumerable<Standings>> OverallStandings()
        {
            List<Standings> standings = new List<Standings>();
            List<Standings> standingsDynamically = new List<Standings>();
            standingsDynamically.AddRange(_StandingsRepository.playersInStandings());
            var allPredictions = _repository.GetAllPredictions().OrderBy(y => y.DateOfPrediction);
            var allRealCases = _RealCasesRepository.GetAllRealCases().OrderBy(x=>x.DateOfRealCases);

            var viewStand = populateStandings(standingsDynamically, allPredictions, allRealCases);
            return View(viewStand);
        }
        List<Standings> populateStandings(List<Standings> standingsDynamically, IEnumerable<Predictions> allPredictions, IEnumerable<RealCases> allRealCases)
        {
            var SystemPoints = _SystemPointsRepository.AllSystemPoints();
            int maxIntSystemPoint = 0;
            if (SystemPoints.Count>0) {
                maxIntSystemPoint = SystemPoints.OrderByDescending(x => x.LessOrEqualThanDif).First().LessOrEqualThanDif;
            } 
           
            foreach (var realCase in allRealCases)
            {
                var sth = _repository.BestPredictNoByDate().Where(b => b.DateOfPrediction.Date == realCase.DateOfRealCases.Date).ToList();
                if (sth.Count>0) {
                    var bestPredByDate = _repository.BestPredictNoByDate().Where(b => b.DateOfPrediction.Date == realCase.DateOfRealCases.Date).Select(x=>x.CasesOfPrediction).ToList();
                    var PredictionsBydate = _repository.PredictionsByDate(realCase.DateOfRealCases.Date);
                    foreach (var predictionItem in PredictionsBydate)
                    {
                            foreach (var standingItem in standingsDynamically)
                            {
                                if (predictionItem.PlayerName == standingItem.PlayerName)
                                {
                                    standingItem.PredictionsNo += 1;
                                    standingItem.DifFromRealCases += Math.Abs(predictionItem.CasesOfPrediction - realCase.RealCasesNo);
                                    if (bestPredByDate.Contains(predictionItem.CasesOfPrediction))
                                    {
                                    int diffFromRealCase = Math.Abs(predictionItem.CasesOfPrediction - realCase.RealCasesNo);
                                        standingItem.CloserPredictionsNo += 1;
                                        if (diffFromRealCase > maxIntSystemPoint) {
                                            standingItem.Points += 1;
                                            break;
                                        }
                                        else {
                                            foreach (var item in SystemPoints)
                                            {
                                                if (diffFromRealCase >= item.MoreOrEqualThanDif &&
                                                    diffFromRealCase <= item.LessOrEqualThanDif)
                                                {
                                                    standingItem.Points += item.CategoryPoints;
                                                    break;
                                                }
                                            }
                                        }                                       
                                    }
                                }
                            }
                    }
                }               
            }
            int positionCnt = 0;
            List<Standings> standings = standingsDynamically.OrderBy(m => m.PredictionsNo).OrderBy(w => w.DifFromRealCases).OrderByDescending(p => p.CloserPredictionsNo).OrderByDescending(p => p.Points).ToList();
            foreach (var item in standings)
            {
                item.PositionNo = positionCnt + 1;
                positionCnt += 1;
            }
            return standings;
        }
    }
}
