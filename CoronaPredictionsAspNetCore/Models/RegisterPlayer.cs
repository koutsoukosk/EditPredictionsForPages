using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaPredictionsAspNetCore.Models
{
    public class RegisterPlayer
    {
        [Key]
        [Required(ErrorMessage="Please enter your username")]
        [DisplayName("Username")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please enter your name")]
        [DisplayName("Player Name")]
        public string PlayerName { get; set; }
        [Required(ErrorMessage = "Please enter a strong password")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("Password", ErrorMessage = "Password does not match")]
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }
        [Required]
        public DateTime DateTimeRegister { get; set; }
    }
}
