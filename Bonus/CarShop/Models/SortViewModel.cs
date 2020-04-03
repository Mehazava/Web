using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.Models
{
    public class SortViewModel
    {
        public SortState NameSort { get; set; }
        public SortState PowerSort { get; set; }
        public SortState PriceSort { get; set; }
        public SortState BrandSort { get; set; }
        public SortState Current { get; set; }

        public SortViewModel(SortState sortOrder)
        {
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDes : SortState.NameAsc;
            PowerSort = sortOrder == SortState.PowerAsc ? SortState.PowerDes : SortState.PowerAsc;
            PriceSort = sortOrder == SortState.PriceAsc ? SortState.PriceDes : SortState.PriceAsc;
            BrandSort = sortOrder == SortState.BrandAsc ? SortState.BrandDes : SortState.BrandAsc;
            Current = sortOrder;
        }
    }
}
