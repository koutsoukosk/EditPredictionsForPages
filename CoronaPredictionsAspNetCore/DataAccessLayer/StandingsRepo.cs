using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaPredictionsAspNetCore.Models;

namespace CoronaPredictionsAspNetCore.DataAccessLayer
{
    public class StandingsRepo : IStandingsRepo
    {
        private readonly PredictCoronaCasesDBContext _context;
 

        public StandingsRepo(PredictCoronaCasesDBContext context)
        {
            _context = context;
        }
        public List<Standings> playersInStandings()
        {
            List<string> dbPlayers = new List<string>();
            var sth = _context.Players.Select(x => x.Name).ToList();
            dbPlayers.AddRange(sth);
            int PositionNum = 0;
            List<Standings> playersStandings = new List<Standings>();
            foreach (string item in dbPlayers)
            {
                playersStandings.Add(new Standings()
                {
                    PositionNo = ++PositionNum,
                    PredictionsNo = 0,
                    PlayerName = item,
                    Points = 0,
                    CloserPredictionsNo = 0,
                    DifFromRealCases = 0
                });
            }
            return playersStandings;
        }
    }
}
