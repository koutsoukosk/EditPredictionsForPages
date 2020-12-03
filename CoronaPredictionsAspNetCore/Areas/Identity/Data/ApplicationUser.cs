﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaPredictionsAspNetCore.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName="nvarchar(50)")]
        public string FullName { get; set; }     
        public DateTime DateTimeRegister { get; set; }
    }
}
