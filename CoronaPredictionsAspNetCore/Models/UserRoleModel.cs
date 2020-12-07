using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaPredictionsAspNetCore.Models
{
    public class UserRoleModel
    {
        [DisplayName("User Id")]
        public string UserId { get; set; }
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [DisplayName("Is Selected")]
        public bool IsSelected { get; set; }
    }
}
