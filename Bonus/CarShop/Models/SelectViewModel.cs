using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CarShop.Models
{
    public class SelectViewModel
    {
        public SelectViewModel(List<Brand> brands, int? brand)
        {
            brands.Insert(0, new Brand { Name = "Not selected", Id = 0 });
            Brands = new SelectList(brands, "Id", "Name", brand);
            SelectedBrand = brand;
        }
        public SelectList Brands { get; private set; }
        public int? SelectedBrand { get; private set; }
    }
}