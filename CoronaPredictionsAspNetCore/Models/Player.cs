using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaPredictionsAspNetCore.Models
{
    public class Player
    {
        [Key]
        [Required]        
        public int PlayerID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Nickname { get; set; }
        [DisplayName("Work Description")]
        [MaxLength(100)]
        public string WorkDescription { get; set; }
    }
}
