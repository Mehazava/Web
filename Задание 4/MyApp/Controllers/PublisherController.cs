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
    public class PublisherController : Controller
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
                result = HomeController.info.items[ItemType.publisher];
            }
            else
            {
                result = HomeController.info.items[ItemType.publisher].Where(cool =>
                    (cool as PublisherInfo).Name.ToLower().Contains(search.ToLower())).ToList<InfoItem>();
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
            PublisherInfo publisher;
            try
            {
                publisher = (PublisherInfo)HomeController.info.GetByID(ItemType.publisher, id);
                if (publisher == null)
                {
                    return BadRequest();
                }
                ViewBag.Stuff = publisher;
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
        public ActionResult Create(PublisherModel model)
        {
            if (ModelState.IsValid)
                try
                {
                    HomeController.info.Add(new PublisherInfo(model.Name));
                    return RedirectToAction("List", "Publisher");
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
            PublisherInfo publisher;
            try
            {
                publisher = (PublisherInfo)HomeController.info.GetByID(ItemType.publisher, id);
                if (publisher == null)
                {
                    return BadRequest();
                }
                PublisherModel model = new PublisherModel();
                model.Name = publisher.Name;
                return View(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, PublisherModel model)
        {
            if (ModelState.IsValid)
                try
                {
                    PublisherInfo replacement = new PublisherInfo(model.Name, id);
                    if (HomeController.info.CheckItem(replacement) == null)
                    {
                        HomeController.info.Remove(ItemType.publisher, id);
                        HomeController.info.Add(replacement);
                    }
                    else
                    {
                        throw new Exception("Invalid data.");
                    }
                    return RedirectToAction("List", "Publisher");
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
            GenreInfo publisher;
            try
            {
                publisher = (GenreInfo)HomeController.info.GetByID(ItemType.publisher, id);
                if (publisher == null)
                {
                    return BadRequest();
                }
                ViewBag.Stuff = publisher;
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
                HomeController.info.Remove(ItemType.publisher, id);
                return RedirectToAction("List", "Publisher");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}