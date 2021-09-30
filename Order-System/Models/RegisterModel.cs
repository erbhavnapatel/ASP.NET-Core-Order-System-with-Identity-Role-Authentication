using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Order_System.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Id is required")]
        public string AppUserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public int Name { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string AppUserPassword { get; set; }
    }
}
