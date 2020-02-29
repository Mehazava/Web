using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Coolio.Models
{
    public class Song
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter the name")]
        [RegularExpression(@"[A-Za-z_\s]+", ErrorMessage = "Incorrect name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter the rating")]
        [RegularExpression(@"[1-9]", ErrorMessage = "Incorrect rating (1 to 9)")]
        public int Rating { get; set; }
        [Required(ErrorMessage = "Choose producer")]
        [RegularExpression(@"[1-9][0-9]*", ErrorMessage = "Choose producer")]
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }
        public DateTime CreationDate { get; set; }
        public string Lifetime{
            get
            {
                TimeSpan span = (DateTime.Now - CreationDate);
                int hours = (int)span.TotalHours;
                return $"{hours} hour{(((hours % 10 == 1) && (hours != 11)) ? ' ' : 's')}, " +
                    $"{span.Minutes} minute{(((span.Minutes % 10 == 1) && (span.Minutes != 11)) ? ' ' : 's')}, " +
                    $"{span.Seconds} second{(((span.Seconds % 10 == 1) && (span.Seconds != 11)) ? ' ' : 's')}.";
            }
        }
    }
}
