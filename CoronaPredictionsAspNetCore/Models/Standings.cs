using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaPredictionsAspNetCore.Models
{
    public class Standings
    {
        [DisplayName("Position#")]
        public int PositionNo { get; set; }
        [DisplayName("Predictions#")]
        public int PredictionsNo { get; set; }
        [DisplayName("Player Name")]
        public string PlayerName { get; set; }
        public int Points { get; set; }
        [DisplayName("Closer Predictions#")]
        public int CloserPredictionsNo { get; set; }
        [DisplayName("Difference From RealCases")]
        public int DifFromRealCases { get; set; }
    }
}
