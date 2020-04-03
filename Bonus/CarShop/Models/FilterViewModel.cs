using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CarShop.Models
{
    public class FilterViewModel : SelectViewModel
    {
        public FilterViewModel(List<Brand> brands, int? brand, string name)
            : base(brands, brand)
        {
            SelectedName = name;
        }
        public string SelectedName { get; private set; }
    }
}