using System;
using System.Collections.Generic;

namespace BasedOnDb
{
    public partial class Producers
    {
        public Producers()
        {
            Songs = new HashSet<Songs>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Songs> Songs { get; set; }
    }
}
