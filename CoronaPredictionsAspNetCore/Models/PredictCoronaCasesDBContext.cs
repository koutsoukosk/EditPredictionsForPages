using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaPredictionsAspNetCore.Models;

namespace CoronaPredictionsAspNetCore.Models
{
    public class PredictCoronaCasesDBContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<RealCases> RealCasesEachDay { get; set; }
        public DbSet<Predictions> Prediction { get; set; }
        public PredictCoronaCasesDBContext(DbContextOptions<PredictCoronaCasesDBContext> options):base(options)
        { 
        
        }
        public DbSet<CoronaPredictionsAspNetCore.Models.PointSystem> PointSystem { get; set; }
    }
}
