using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coolio.Models
{
    public class SortViewModel
    {
        public SortState NameSort { get; set; }
        public SortState RatingSort { get; set; }
        public SortState ProducerSort { get; set; }
        public SortState Current { get; set; }

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDes : SortState.NameAsc;
            RatingSort = sortOrder == SortState.RatingAsc ? SortState.RatingDes : SortState.RatingAsc;
            ProducerSort = sortOrder == SortState.ProducerAsc ? SortState.ProducerDes : SortState.ProducerAsc;
            Current = sortOrder;
        }
    }
}
