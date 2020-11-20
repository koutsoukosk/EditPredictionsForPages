using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaPredictionsAspNetCore.Models
{
    public class PointSystem
    {
        [Key]
        [Required]
        public int PointSystemID { get; set; }
        [Required]
        [MaxLength(50)]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        [Required]
        [DisplayName("More Or Equal Than Dif")]
        public int MoreOrEqualThanDif { get; set; }
        [Required]
        [DisplayName("Less Or Equal Than Dif")]
        public int LessOrEqualThanDif { get; set; }
        [Required]
        [DisplayName("Category Points")]
        public int CategoryPoints { get; set; }       
    }
}
