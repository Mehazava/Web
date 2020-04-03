using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Year")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Enter Power")]
        public int Power { get; set; }
        [Required(ErrorMessage = "Select brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        [Required(ErrorMessage = "Enter Price")]
        public int Price { get; set; }
    }
}
