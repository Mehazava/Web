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
    public class DiskController : Controller
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
                result = HomeController.info.items[ItemType.disk];
            }
            else
            {
                result = HomeController.info.items[ItemType.disk].Where(cool =>
                    (cool as DiskInfo).Name.ToLower().Contains(search.ToLower())).ToList<InfoItem>();
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
            DiskInfo disk;
            try
            {
                disk = (DiskInfo)HomeController.info.GetByID(ItemType.disk, id);
                if (disk == null)
                {
                    return BadRequest();
                }
                ViewBag.Stuff = disk;
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
        public ActionResult Create(DiskModel model)
        {
            if (ModelState.IsValid)
                try
                {
                    HomeController.info.Add(new DiskInfo(model.AuthorID, 
                        model.GenreID.Split(' ', StringSplitOptions.RemoveEmptyEntries)?.Select(int.Parse)?.ToList(),
                            model.PublisherID, model.Name, DateTime.Parse(model.ReleaseDate)));
                    return RedirectToAction("List", "Disk");
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            else
                return View(model);

        }

        public ActionResult Edit(int id)
        {
            DiskInfo disk;
            try
            {
                disk = (DiskInfo)HomeController.info.GetByID(ItemType.disk, id);
                if (disk == null)
                {
                    return BadRequest();
                }
                DiskModel model = new DiskModel
                {
                    Name = disk.Name,
                    PublisherID = disk.PublisherID,
                    ReleaseDate = disk.ReleaseDate.ToString("d"),
                    AuthorID = disk.AuthorID,
                    GenreID = string.Join(' ', disk.GenreID)
                };
                return View(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, DiskModel model)
        {
            if (ModelState.IsValid)
                try
                {
                    DiskInfo replacement = new DiskInfo(model.AuthorID,
                        model.GenreID.Split(' ', StringSplitOptions.RemoveEmptyEntries)?.Select(int.Parse)?.ToList(),
                            model.PublisherID, model.Name, DateTime.Parse(model.ReleaseDate), id);
                    if (replacement == null)
                    {
                        throw new Exception("Failed to construct modified disk info.");
                    }
                    string NewError = HomeController.info.CheckItem(replacement);
                    if (NewError == null)
                    {
                        HomeController.info.Remove(ItemType.disk, id);
                        HomeController.info.Add(replacement);
                    }
                    else
                    {
                        throw new Exception(NewError);
                    }
                    return RedirectToAction("List", "Disk");
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            else
                return View(model);
        }

        public ActionResult Delete(int id)
        {
            DiskInfo disk;
            try
            {
                disk = (DiskInfo)HomeController.info.GetByID(ItemType.disk, id);
                if (disk == null)
                {
                    return BadRequest();
                }
                ViewBag.Stuff = disk;
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
                HomeController.info.Remove(ItemType.disk, id);
                return RedirectToAction("List", "Disk");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}