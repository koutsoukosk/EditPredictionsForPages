using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaPredictionsAspNetCore.Models;

namespace CoronaPredictionsAspNetCore.DataAccessLayer
{
    public class SystemPointsRepo : ISystemPointsRepo
    {
        private readonly PredictCoronaCasesDBContext _context;
       

        public SystemPointsRepo(PredictCoronaCasesDBContext context)
        {
            _context = context;
         
        }
        public List<PointSystem> AllSystemPoints()
        {
            return _context.PointSystem.ToList();
        }
    }
}
