using CoronaPredictionsAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaPredictionsAspNetCore.DataAccessLayer
{
    public interface IPredictionsRepo
    {
        IEnumerable<Predictions> GetAllPredictions();
        IEnumerable<RealCases> GetAllRealCases();
        List<Standings> playersInStandings();
        List<PointSystem> AllSystemPoints();
        List<Predictions> BestPredictNoByDate();
        List<Predictions> PredictionsByDate(DateTime realCaseDate);
        List<PlayerOfDay> BestPlayersByDate();
    }
}
