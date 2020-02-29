using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DiskLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyApp.Models;

namespace MyApp.Controllers
{
    public class HomeController : Controller
    {
        static public InfoManager info;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            if(info == null)
            {
                info = new InfoManager();
                info.Add(new GenreInfo("Rock"), (unnecessary, outps) => _logger.LogInformation(outps));//id:1
                info.Add(new GenreInfo("Pop"));//2
                info.Add(new GenreInfo("Electro"));//3
                info.Add(new GenreInfo("Dubstep"));//4

                info.Add(new PublisherInfo("Lacree"));//5
                info.Add(new PublisherInfo("Topless"));//6

                info.Add(new AuthorInfo("Brandon Peterson 20/10/93"));//7
                info.Add(new AuthorInfo("Randy Watson 15/03/91"));//8
                info.Add(new AuthorInfo("Lee Clinton 11/11/11"));//9

                info.Add(new DiskInfo("Broken_twigs 7 6 20/04/16 2 3"));//10
                info.Add(new DiskInfo("Dreams 8 6 20/02/13 3 4"));//11
                info.Add(new DiskInfo("Terror 9 5 21/12/19 1"));//12

            }
        }

        public IActionResult Index()
        {
            return View();
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
