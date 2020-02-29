using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyApp.ViewModels
{
    public class PublisherModel
    {
        [Required (ErrorMessage = "Enter the name")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+", ErrorMessage = "Wrong format")]
        public string Name { get; set; }
    }
}
