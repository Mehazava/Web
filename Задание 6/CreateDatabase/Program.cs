using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MusicContext db = new MusicContext("DBConnection"))
            {
                if (db.Songs.Any())
                {
                    Console.WriteLine("Database already has some elements.");
                } else
                {
                    Producer P1 = new Producer { Name = "Divines" };
                    Producer P2 = new Producer { Name = "Bad songs" };
                    Producer P3 = new Producer { Name = "UlTraShy" };
                    Producer P4 = new Producer { Name = "Zanny" };
                    db.Producers.Add(P1);
                    db.Producers.Add(P2);
                    db.Producers.Add(P3);
                    db.Producers.Add(P4);

                    db.Songs.Add(new Song { Name = "Catacombs", Producer = P1 });
                    db.Songs.Add(new Song { Name = "Panthera", Producer = P1 });
                    db.Songs.Add(new Song { Name = "Torn parachute", Producer = P2 });
                    db.Songs.Add(new Song { Name = "Salami", Producer = P2 });
                    db.Songs.Add(new Song { Name = "Cricket", Producer = P3 });
                    db.Songs.Add(new Song { Name = "Feel", Producer = P3 });
                    db.Songs.Add(new Song { Name = "Exterminate", Producer = P4 });
                    db.Songs.Add(new Song { Name = "Chukachanga", Producer = P4 });
                    db.SaveChanges();
                    Console.WriteLine("Database created and seeded.");
                }

                var songs = db.Songs;
                Console.WriteLine("Current song list:");
                foreach (Song s in songs)
                {
                    Console.WriteLine("[{0}] {1} - by {2}", s.Id, s.Name, s.Producer.Name);
                }
            }
            Console.Read();
        }
    }
}
