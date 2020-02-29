using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MyApp.ViewModels
{
    public class DiskModel
    {
        [Required(ErrorMessage = "Enter genres' id")]
        [RegularExpression(@"[0-9\s]+", ErrorMessage = "Wrong format")]
        public string GenreID { get; set; }

        [Required(ErrorMessage = "Enter the author's id")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Wrong format")]
        public int AuthorID { get; set; }

        [Required(ErrorMessage = "Enter the publisher's id")]
        [RegularExpression(@"[0-9]+", ErrorMessage = "Wrong format")]
        public int PublisherID { get; set; }

        [Required(ErrorMessage = "Enter the name")]
        [RegularExpression(@"[A-Za-z_]+", ErrorMessage = "Wrong format")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter the release date")]
        [RegularExpression(@"[0-3][0-9]([\/.])[0-1][0-9]\1[0-9]{2,4}", ErrorMessage = "Format is dd/mm/yy or dd.mm.yyyy")]
        public string ReleaseDate { get; set; }
    }
}
