using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarShop.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
    }
}
