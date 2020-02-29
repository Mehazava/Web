using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Coolio.Models
{
    public class FilterViewModel : SelectViewModel
    {
        public FilterViewModel(List<Producer> producers, int? producer, string name)
            : base(producers, producer)
        {
            SelectedName = name;
        }
        public string SelectedName { get; private set; }
    }
}