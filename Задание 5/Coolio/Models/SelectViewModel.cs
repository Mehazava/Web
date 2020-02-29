using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Coolio.Models
{
    public class SelectViewModel
    {
        public SelectViewModel(List<Producer> producers, int? producer)
        {
            producers.Insert(0, new Producer { Name = "Undefined", Id = 0 });
            Producers = new SelectList(producers, "Id", "Name", producer);
            SelectedProducer = producer;
        }
        public SelectList Producers { get; private set; }
        public int? SelectedProducer { get; private set; }
    }
}