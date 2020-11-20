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
    public class Predictions
    {
        [Key]
        [Required]
        public int PredictionID { get; set; }
        [Required]
        [DisplayName("Day Of Prediction")]
        public DayOfWeek DayOfPrediction { get; set; }
        [Required]
        [DisplayName("Date Of Prediction")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DateOfPrediction { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Player Name")]
        public string PlayerName { get; set; }
        [Required]
        [DisplayName("Cases Of Prediction")]
        public int CasesOfPrediction { get; set; }
        [NotMapped]
        public SelectList PlayersList { get; set; }
    }
    public class User
    {
        public string Key { get; set; }
        public string Display { get; set; }
    }
}
