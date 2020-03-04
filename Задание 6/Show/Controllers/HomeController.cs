using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Show.Models;

namespace Show.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationContext db;
        LazyContext Ldb;

        public HomeController(ApplicationContext context, LazyContext lazyContext, ILogger<HomeController> logger)
        {
            db = context;
            Ldb = lazyContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Eager()
        {
            ViewBag.Title = $"Songs: {db.Producers.FirstOrDefault(x => x.Id == 1).Name}";
            var songs = db.Songs.Where(x => x.ProducerId == 1).Include(x => x.Producer);
            return View(songs);
        }

        [HttpGet]
        public IActionResult Explicit()
        {
            ViewBag.Title = $"Songs: {db.Producers.FirstOrDefault(x => x.Id == 2).Name}";
            var songs = db.Songs.Where(x => x.ProducerId == 2);
            db.Entry(songs.First<Song>()).Reference("Producer").Load();
            return View(songs);
        }

        [HttpGet]
        public IActionResult Lazy()
        {
            ViewBag.Title = $"Songs: {Ldb.Producers.FirstOrDefault(x => x.Id == 3).Name}";
            var songs = Ldb.Songs.Where(x => x.ProducerId == 3);
            return View(songs);
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
