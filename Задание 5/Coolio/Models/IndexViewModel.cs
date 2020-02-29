using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coolio.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Song> Songs { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
    }
}
