using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order_System.Areas.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
