using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaPredictionsAspNetCore.Models
{
    public class UserClaimsModel
    {
        public UserClaimsModel() {
            ClaimsOfUser = new List<UserClaim>();
        }
        public string UserId { get; set; }
        public List<UserClaim> ClaimsOfUser { get; set; }
    }
}
