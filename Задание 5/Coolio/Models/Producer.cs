using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coolio.Models
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Song> Songs { get; set; }
        public Producer()
        {
            Songs = new List<Song>();
        }
    }
}
