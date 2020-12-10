﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaPredictionsAspNetCore.Models;

namespace CoronaPredictionsAspNetCore.DataAccessLayer
{
    public class PredictionsRepo : IPredictionsRepo
    {
        private readonly PredictCoronaCasesDBContext _context;
        private readonly AuthDbContext _AuthContext;

        public PredictionsRepo(PredictCoronaCasesDBContext context, AuthDbContext authContext)
        {
            _context = context;
            _AuthContext = authContext;
        }
        public List<PlayerOfDay> BestPlayersByDate()
        {
            List<PlayerOfDay> bestPlayerByDate = new List<PlayerOfDay>();
            var bestPredNoDate = BestPredictNoByDate();
            var allPred = _context.Prediction.ToList();
            foreach (var bestPredItem in bestPredNoDate)
            {              
                int realCasesByDate = _context.RealCasesEachDay.Where(x => x.DateOfRealCases.Date == bestPredItem.DateOfPrediction.Date).Select(y => y.RealCasesNo).FirstOrDefault();
                foreach (var predItem in allPred.Where(x => x.DateOfPrediction.Date == bestPredItem.DateOfPrediction.Date))
                {
                    if (predItem.CasesOfPrediction == bestPredItem.CasesOfPrediction)
                    {
                        PlayerOfDay bestPlayerInDay = new PlayerOfDay
                        {
                            DateOfPrediction = predItem.DateOfPrediction.Date,
                            DayOfPrediction = predItem.DateOfPrediction.DayOfWeek,
                            PlayerName = predItem.PlayerName,
                            RealCasesNo = realCasesByDate,
                            CasesOfPrediction = predItem.CasesOfPrediction,
                            DifFromRealCases = Math.Abs(bestPredItem.CasesOfPrediction - realCasesByDate)
                        };
                        bestPlayerByDate.Add(bestPlayerInDay);
                    };
                }
            }
            return bestPlayerByDate;
        }
        //public List<PointSystem> AllSystemPoints()
        //{
        //    return _context.PointSystem.ToList();
        //}

            public List<Predictions> BestPredictNoByDate()
        {
            List<Predictions> bestPredByDate = new List<Predictions>();
            var predictionList= _context.Prediction.ToList();
            var realCasesList = _context.RealCasesEachDay.ToList();
            foreach (var realCaseItem in realCasesList)
            {
               
                List<Predictions> PredByDate = predictionList.Where(x => x.DateOfPrediction.Date == realCaseItem.DateOfRealCases.Date).ToList();
                if (PredByDate.Count>0) { 
                int difByDate = PredByDate.Max(dif=>dif.CasesOfPrediction);
                foreach (var predItem in predictionList)
                {
                    if (realCaseItem.DateOfRealCases.Date == predItem.DateOfPrediction.Date) {
                        if (Math.Abs(predItem.CasesOfPrediction- realCaseItem.RealCasesNo)< difByDate)
                        {
                            difByDate = Math.Abs(predItem.CasesOfPrediction - realCaseItem.RealCasesNo);
                        }
                    }
                }
                        Predictions topPredMinus = new Predictions() { CasesOfPrediction = realCaseItem.RealCasesNo- difByDate, DateOfPrediction = realCaseItem.DateOfRealCases.Date };
                    bestPredByDate.Add(topPredMinus);
                    Predictions topPredPlus = new Predictions() { CasesOfPrediction = realCaseItem.RealCasesNo + difByDate, DateOfPrediction = realCaseItem.DateOfRealCases.Date };
                    bestPredByDate.Add(topPredPlus);
                                 
                };
            }
            return bestPredByDate;
        }

        public IEnumerable<Predictions> GetAllPredictions()
        {
            return _context.Prediction.ToList();
        }

        //public IEnumerable<RealCases> GetAllRealCases()
        //{
        //    return _context.RealCasesEachDay.ToList();
        //}

        //public List<Standings> playersInStandings()
        //{
        //    List<string> dbPlayers = new List<string>();
        //    var sth = _context.Players.Select(x => x.Name).ToList();
        //    dbPlayers.AddRange(sth);
        //    int PositionNum = 0;
        //    List<Standings> playersStandings = new List<Standings>();
        //    foreach (string item in dbPlayers)
        //    {
        //        playersStandings.Add(new Standings()
        //        {
        //            PositionNo = ++PositionNum,
        //            PredictionsNo = 0,
        //            PlayerName = item,
        //            Points=0,
        //            CloserPredictionsNo = 0,
        //            DifFromRealCases = 0
        //        });
        //    }
        //    return playersStandings;
        //}

        public List<Predictions> PredictionsByDate(DateTime realCaseDate)
        {
            return _context.Prediction.Where(x => x.DateOfPrediction.Date == realCaseDate.Date).ToList();
        }

        //public string authenticatedPlayerNameByUserEmail(string email)
        //{
        //    var fullNameByEmail = _AuthContext.Users.ToList();
        //    foreach (var item in fullNameByEmail)
        //    {
        //        if (item.UserName==email)
        //        {
        //            return item.FullName;
        //        }
        //    }
        //    return null;
        //}

        //public List<Player> GetAllPlayers()
        //{
        //    return _context.Players.ToList();
        //}
    }
}
