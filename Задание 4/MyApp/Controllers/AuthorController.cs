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
    public class AuthorController : Controller
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
                result = HomeController.info.items[ItemType.author];
            }
            else
            {
                result = HomeController.info.items[ItemType.author].Where(cool =>
                    (cool as AuthorInfo).FName.ToLower().Contains(search.ToLower()) ||
                        (cool as AuthorInfo).SName.ToLower().Contains(search.ToLower())).ToList<InfoItem>();
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
            AuthorInfo author;
            try
            {
                author = (AuthorInfo)HomeController.info.GetByID(ItemType.author, id);
                if (author == null)
                {
                    return BadRequest();
                }
                ViewBag.Stuff = author;
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
        public ActionResult Create(AuthorModel model)
        {
            if (ModelState.IsValid)
                try
                {
                    HomeController.info.Add(new AuthorInfo(model.SName, model.FName, DateTime.Parse(model.BirthDate)));
                    return RedirectToAction("List", "Author");
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
            AuthorInfo author;
            try
            {
                author = (AuthorInfo)HomeController.info.GetByID(ItemType.author, id);
                if (author == null)
                {
                    return BadRequest();
                }
                AuthorModel model = new AuthorModel
                {
                    FName = author.FName,
                    SName = author.SName,
                    BirthDate = author.BirthDate.ToString("d")
                };
                return View(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, AuthorModel model)
        {
            if (ModelState.IsValid)
                try
                {
                    AuthorInfo replacement = new AuthorInfo(model.SName, model.FName, DateTime.Parse(model.BirthDate), id);
                    string NewError = HomeController.info.CheckItem(replacement);
                    if (NewError == null)
                    {
                        HomeController.info.Remove(ItemType.author, id);
                        HomeController.info.Add(replacement);
                    }
                    else
                    {
                        throw new Exception(NewError);
                    }
                    return RedirectToAction("List", "Author");
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
            AuthorInfo author;
            try
            {
                author = (AuthorInfo)HomeController.info.GetByID(ItemType.author, id);
                if (author == null)
                {
                    return BadRequest();
                }
                ViewBag.Stuff = author;
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
                HomeController.info.Remove(ItemType.author, id);
                return RedirectToAction("List", "Author");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}