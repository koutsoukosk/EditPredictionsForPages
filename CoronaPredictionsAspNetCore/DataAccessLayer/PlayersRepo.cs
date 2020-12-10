using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaPredictionsAspNetCore.Models;

namespace CoronaPredictionsAspNetCore.DataAccessLayer
{
    public class PlayersRepo : IPlayersRepo
    {
        private readonly PredictCoronaCasesDBContext _context;
        private readonly AuthDbContext _AuthContext;

        public PlayersRepo(PredictCoronaCasesDBContext context, AuthDbContext authContext)
        {
            _context = context;
            _AuthContext = authContext;
        }
        public string authenticatedPlayerNameByUserEmail(string email)
        {
            var fullNameByEmail = _AuthContext.Users.ToList();
            foreach (var item in fullNameByEmail)
            {
                if (item.UserName == email)
                {
                    return item.FullName;
                }
            }
            return null;
        }

        public List<Player> GetAllPlayers()
        {
            return _context.Players.ToList();
        }
    }
}
