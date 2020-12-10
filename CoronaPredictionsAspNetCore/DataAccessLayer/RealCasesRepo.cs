using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaPredictionsAspNetCore.Models;

namespace CoronaPredictionsAspNetCore.DataAccessLayer
{
    public class RealCasesRepo : IRealCasesRepo
    {
        private readonly PredictCoronaCasesDBContext _context;
        

        public RealCasesRepo(PredictCoronaCasesDBContext context)
        {
            _context = context;
         
        }
        public IEnumerable<RealCases> GetAllRealCases()
        {
            return _context.RealCasesEachDay.ToList();
        }
    }
}
