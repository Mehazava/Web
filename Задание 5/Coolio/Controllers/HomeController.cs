using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Coolio.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Coolio.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
            if (db.Producers.Count() == 0)
            {
                Producer BB = new Producer { Name = "Bad boys" };
                Producer PG = new Producer { Name = "Pentagram" };
                Producer SO = new Producer { Name = "Sendoff" };
                Producer PL = new Producer { Name = "Palette" };
                db.Producers.AddRange(BB, PG, SO, PL);

                db.Songs.AddRange(
                    new Song { Name = "Fallthrough", Rating = 9, Producer = BB, CreationDate = DateTime.Now },
                    new Song { Name = "Patriot", Rating = 7, Producer = BB, CreationDate = DateTime.Now },
                    new Song { Name = "Broken bones", Rating = 8, Producer = BB, CreationDate = DateTime.Now },
                    new Song { Name = "Last mass", Rating = 5, Producer = PG, CreationDate = DateTime.Now },
                    new Song { Name = "Demonic burn", Rating = 7, Producer = PG, CreationDate = DateTime.Now },
                    new Song { Name = "Without context", Rating = 6, Producer = SO, CreationDate = DateTime.Now },
                    new Song { Name = "Last chance", Rating = 8, Producer = SO, CreationDate = DateTime.Now },
                    new Song { Name = "Creamy", Rating = 2, Producer = PL, CreationDate = DateTime.Now },
                    new Song { Name = "Stain on my life", Rating = 9, Producer = PL, CreationDate = DateTime.Now }
                );

                db.SaveChanges();
            }
        }

        public async Task<IActionResult> Index(string name, int? producer, SortState sortOrder = SortState.NameAsc, int page = 1)
        {
            int pageSize = 4;

            IQueryable<Song> songs = db.Songs.Include(x => x.Producer);
            if (producer != null && producer != 0)
            {
                songs = songs.Where(p => p.ProducerId == producer);
            }
            if (!String.IsNullOrEmpty(name))
            {
                songs = songs.Where(p => p.Name.Contains(name));
            }

            songs = sortOrder switch
            {
                SortState.NameDes => songs.OrderByDescending(s => s.Name),
                SortState.RatingAsc => songs.OrderBy(s => s.Rating),
                SortState.RatingDes => songs.OrderByDescending(s => s.Rating),
                SortState.ProducerAsc => songs.OrderBy(s => s.Producer.Name),
                SortState.ProducerDes => songs.OrderByDescending(s => s.Producer.Name),
                _ => songs.OrderBy(s => s.Name),
            };

            var count = await songs.CountAsync();
            var items = await songs.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(db.Producers.ToList(), producer, name),
                Songs = items
            };
            return View(viewModel);
        }

        public IActionResult Create()
        {
            ViewBag.Select = new SelectList(db.Producers.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Song song)
        {
            if (ModelState.IsValid)
            {
                song.CreationDate = DateTime.Now;
                song.Producer = db.Producers.FirstOrDefault(x => x.Id == song.ProducerId);
                if (song.ProducerId == 0)
                {
                    return BadRequest("Incorrect producer id(==0).");
                }
                if (song.Producer == null)
                {
                    return BadRequest("Incorrect producer id.");
                }
                db.Songs.Add(song);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Select = new SelectList(db.Producers.ToList(), "Id", "Name");
            return View(song);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                DetailsViewModel detailsView = new DetailsViewModel
                {
                    Song = await db.Songs.FirstOrDefaultAsync(p => p.Id == id)
                };
                if (detailsView.Song != null)
                {
                    detailsView.Select = new SelectViewModel(db.Producers.ToList(), detailsView.Song.ProducerId);
                    return View(detailsView);
                }
            }
            return NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Song song = await db.Songs.FirstOrDefaultAsync(p => p.Id == id);
                if (song != null)
                {
                    ViewBag.Select = new SelectList(db.Producers.ToList(), "Id", "Name", song.ProducerId);
                    return View(song);
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Song song, int? id)
        {
            if (ModelState.IsValid)
            {
                Song original = db.Songs.AsNoTracking().FirstOrDefault(x => x.Id == id);
                if (original == null)
                {
                    return BadRequest();
                }
                song.CreationDate = original.CreationDate;
                song.Producer = db.Producers.FirstOrDefault(x => x.Id == song.ProducerId);
                if (song.ProducerId == 0)
                {
                    return BadRequest("Incorrect producer id(==0).");
                }
                if (song.Producer == null)
                {
                    return BadRequest("Incorrect producer id.");
                }
                db.Songs.Update(song);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Select = new SelectList(db.Producers.ToList(), "Id", "Name", song.ProducerId);
            return View(song);
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Song song = await db.Songs.FirstOrDefaultAsync(p => p.Id == id);
                if (song != null)
                    return View(song);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Song song = new Song { Id = id.Value };
                db.Entry(song).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
