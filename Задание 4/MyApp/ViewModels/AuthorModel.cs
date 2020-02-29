using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyApp.ViewModels
{
    public class AuthorModel
    {
        [Required(ErrorMessage = "Enter the first name")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Wrong format")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Enter the surname")]
        [RegularExpression(@"[A-Za-z]+", ErrorMessage = "Wrong format")]
        public string SName { get; set; }

        [Required(ErrorMessage = "Enter the date of birth")]
        [RegularExpression(@"[0-3][0-9]([\/.])[0-1][0-9]\1[0-9]{2,4}", ErrorMessage = "Format is dd/mm/yy or dd.mm.yyyy")]
        public string BirthDate { get; set; }
    }
}
