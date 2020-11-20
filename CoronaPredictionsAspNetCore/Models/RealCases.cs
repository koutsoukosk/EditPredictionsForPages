using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaPredictionsAspNetCore.Models
{
    public class RealCases
    {
        [Key]
        [Required]
        public int RealCaseID { get; set; }
        [Required]
        [DisplayName("Day Of Real Cases")]
        public DayOfWeek DayOfRealCases { get; set; }
        [Required]
        [DisplayName("Date Of Real Cases")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DateOfRealCases { get; set; }
        [Required]
        [DisplayName("Real Cases#")]
        public int RealCasesNo { get; set; }
    }
}
