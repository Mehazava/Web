using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrate
{
    class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public int Approval { get; set; }
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }
    }
}
