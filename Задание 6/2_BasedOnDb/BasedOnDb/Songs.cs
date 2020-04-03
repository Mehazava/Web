using System;
using System.Collections.Generic;

namespace BasedOnDb
{
    public partial class Songs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProducerId { get; set; }
        public int Rating { get; set; }

        public virtual Producers Producer { get; set; }
    }
}
