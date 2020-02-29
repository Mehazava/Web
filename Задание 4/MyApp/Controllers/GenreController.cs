using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DiskLib;
using MyApp.ViewModels;

namespace MyApp.Controllers
{
    public class GenreController : Controller
    {
        public ActionResult List(int? id, string search)
        {
            if (id != null)
            {
                return NotFound();
            }
            List<InfoItem> result;
            if (search == null)
            {
                result = HomeController.info.items[ItemType.genre];
            }
            else
            {
                result = HomeController.info.items[ItemType.genre].Where(cool => 
                    (cool as GenreInfo).Name.ToLower().Contains(search.ToLower())).ToList<InfoItem>();
            }
            if (result == null || result.Count == 0)
            {
                ViewBag.Empty = true;
            }
            else
            {
                ViewBag.Empty = false;
                ViewBag.Stuff = result;
            }
            return View();
        }

        public ActionResult Details(int id)
        {
            GenreInfo genre;
            try
            {
                genre = (GenreInfo)HomeController.info.GetByID(ItemType.genre, id);
                if (genre == null)
                {
                    return BadRequest();
                }
                ViewBag.Genre = genre;
                return View();
            }
            catch
            {
                return BadRequest();
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(GenreModel model)
        {
            if (ModelState.IsValid)
                try
                {
                    HomeController.info.Add(new GenreInfo(model.Name));
                    return RedirectToAction("List", "Genre");
                }
                catch
                {
                    return BadRequest();
                }
            else
                return View(model);
            
        }

        public ActionResult Edit(int id)
        {
            GenreInfo genre;
            try
            {
                genre = (GenreInfo)HomeController.info.GetByID(ItemType.genre, id);
                if (genre == null)
                {
                    return BadRequest();
                }
                GenreModel model = new GenreModel();
                model.Name = genre.Name;
                return View(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, GenreModel model)
        {
            if (ModelState.IsValid)
                try
                {
                    GenreInfo replacement = new GenreInfo(model.Name, id);
                    if (HomeController.info.CheckItem(replacement) == null)
                    {
                        HomeController.info.Remove(ItemType.genre, id);
                        HomeController.info.Add(replacement);
                    }
                    else
                    {
                        throw new Exception("Invalid data.");
                    }
                    return RedirectToAction("List", "Genre");
                }
                catch
                {
                    return BadRequest();
                }
            else
                return View(model);
        }

        public ActionResult Delete(int id)
        {
            GenreInfo genre;
            try
            {
                genre = (GenreInfo)HomeController.info.GetByID(ItemType.genre, id);
                if (genre == null)
                {
                    return BadRequest();
                }
                ViewBag.Genre = genre;
                return View();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                HomeController.info.Remove(ItemType.genre, id);
                return RedirectToAction("List", "Genre");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}