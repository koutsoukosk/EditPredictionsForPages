using CoronaPredictionsAspNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaPredictionsAspNetCore.DataAccessLayer
{
    public interface IPlayersRepo
    {
        
        List<Player> GetAllPlayers();
        string authenticatedPlayerNameByUserEmail(string email);
    }
}
