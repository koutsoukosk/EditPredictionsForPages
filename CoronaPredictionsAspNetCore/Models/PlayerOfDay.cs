using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaPredictionsAspNetCore.Models
{
    public class PlayerOfDay
    {
        [Required]
        [DisplayName("Date Of Prediction")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DateOfPrediction { get; set; }
        [Required]
        [DisplayName("Day Of Prediction")]
        public DayOfWeek DayOfPrediction { get; set; }
        [Required]
        [DisplayName("Player Name")]
        public string PlayerName { get; set; }
        [Required]
        [DisplayName("Real Cases#")]
        public int RealCasesNo { get; set; }
        [Required]
        [DisplayName("Cases Of Prediction")]
        public int CasesOfPrediction { get; set; }
        [Required]
        [DisplayName("Difference From RealCases")]
        public int DifFromRealCases { get; set; }
        [NotMapped]
        public SelectList PlayersList { get; set; }
    }
}
