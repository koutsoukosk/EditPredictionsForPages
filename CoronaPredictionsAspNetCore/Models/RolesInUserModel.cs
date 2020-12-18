using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaPredictionsAspNetCore.Models
{
    public class RolesInUserModel
    {
        [DisplayName("Role Id")]
        public string RoleId { get; set; }
        [DisplayName("Role Name")]
        public string RoleName { get; set; }
        [DisplayName("Is Selected")]
        public bool IsSelected { get; set; }
    }
}
