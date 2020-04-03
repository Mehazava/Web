using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CarShop.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Enter email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter the password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password is wrong")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Choose role")]
        public int Role { get; set; }
    }
}
