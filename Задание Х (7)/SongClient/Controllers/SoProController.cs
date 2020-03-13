using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SongClient.Models;

namespace SongClient.Controllers
{
    [ApiController]
    [Route("/song")]
    public class SoProController : ControllerBase
    {
        SoProContext db;
        public SoProController(SoProContext context)
        {
            db = context;
            if (!(db.Songs.Any()))
            {
                Producer P1 = new Producer { Name = "Charlie Jonson" };
                Producer P2 = new Producer { Name = "Percy Qlinton" };
                Producer P3 = new Producer { Name = "Garry Kreston" };
                db.Producers.AddRange(P1, P2, P3);
                db.Songs.AddRange(
                    new Song { Name = "Carlone", Rating = 8, Producer = P1, CreationDate = DateTime.Now },
                    new Song { Name = "Trenko", Rating = 4, Producer = P1, CreationDate = DateTime.Now },
                    new Song { Name = "Party", Rating = 5, Producer = P2, CreationDate = DateTime.Now },
                    new Song { Name = "Selerene", Rating = 3, Producer = P2, CreationDate = DateTime.Now },
                    new Song { Name = "Protiny", Rating = 9, Producer = P3, CreationDate = DateTime.Now },
                    new Song { Name = "Cazar", Rating = 2, Producer = P3, CreationDate = DateTime.Now },
                    new Song { Name = "Cryptopress", Rating = 8, Producer = P3, CreationDate = DateTime.Now },
                    new Song { Name = "Querty", Rating = 5, Producer = P3, CreationDate = DateTime.Now }
                );
                //P1.Songs.AddRange(db.Songs.Where(x => x.Producer == P1));
                //P2.Songs.AddRange(db.Songs.Where(x => x.Producer == P2));
                //P3.Songs.AddRange(db.Songs.Where(x => x.Producer == P3));
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> Get()
        {
            return await db.Songs.Include(g => g.Producer).ToListAsync();
        }

        [HttpGet("/producers")]
        public async Task<ActionResult<IEnumerable<Producer>>> Producers()
        {
            //return await db.Producers.Select(x => Tuple.Create(x.Id, x.Name)).ToListAsync();
            return await db.Producers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> Get(int id)
        {
            Song song = await db.Songs.FirstOrDefaultAsync(x => x.Id == id);
            if (song == null)
                return NotFound();
            return new ObjectResult(song);
        }

        [HttpPost]
        public async Task<ActionResult<Song>> Post(Song song)
        {
            if (song == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            song.CreationDate = DateTime.Now;
            song.Producer = db.Producers.FirstOrDefault(x => x.Id == song.ProducerId);
            if (song.Producer == null)
            {
                ModelState.AddModelError("ProducerId", "No such producer");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            db.Songs.Add(song);
            await db.SaveChangesAsync();
            return Ok(song);
        }

        [HttpPut]
        public async Task<ActionResult<Song>> Put(Song song)
        {
            if (song == null)
            {
                return BadRequest();
            }
            Song original = db.Songs.AsNoTracking().FirstOrDefault(x => x.Id == song.Id);
            if (original == null)
            {
                return NotFound();
            }
            song.CreationDate = original.CreationDate;
            song.Producer = db.Producers.FirstOrDefault(x => x.Id == song.ProducerId);
            if (song.Producer == null)
            {
                ModelState.AddModelError("ProducerId", "No such producer");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            db.Songs.Update(song);
            await db.SaveChangesAsync();
            return Ok(song);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Song>> Delete(int id)
        {
            Song song = db.Songs.FirstOrDefault(x => x.Id == id);
            if (song == null)
            {
                return NotFound();
            }
            db.Songs.Remove(song);
            await db.SaveChangesAsync();
            return Ok(song);
        }
    }
}
