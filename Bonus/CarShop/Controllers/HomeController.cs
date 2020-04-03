using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CarShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace CarShop.Controllers
{
    public class HomeController : Controller
    {
        private CarContext db;

        public HomeController(CarContext context)
        {
            db = context;
            if (db.Cars.Count() == 0)
            {
                Brand PG = new Brand { Name = "Audi", Year = 1885 };
                Brand BB = new Brand { Name = "Chevrolet", Year = 1918 };
                Brand PL = new Brand { Name = "Ford", Year = 1903 };
                Brand SO = new Brand { Name = "Toyota", Year = 1937 };
                db.Brands.AddRange(
                    new Brand { Name = "Acura", Year = 1980 },
                    new Brand { Name = "Alfa Romeo", Year = 1906 },
                    new Brand { Name = "Aston Martin", Year = 1913 },
                    PG,
                    new Brand { Name = "Bentley", Year = 1919 },
                    new Brand { Name = "BMW", Year = 1916 },
                    new Brand { Name = "Renault", Year = 1898 },
                    new Brand { Name = "Buick", Year = 1899 },
                    new Brand { Name = "Cadillac", Year = 1902 },
                    BB,
                    new Brand { Name = "Chrysler", Year = 1925 },
                    new Brand { Name = "Citroen", Year = 1919 },
                    new Brand { Name = "Dodge", Year = 1900 },
                    new Brand { Name = "Ferrari", Year = 1929 },
                    new Brand { Name = "Fiat", Year = 1899 },
                    PL,
                    new Brand { Name = "Geely", Year = 1986 },
                    new Brand { Name = "General Motors", Year = 1908 },
                    new Brand { Name = "GMC", Year = 1902 },
                    new Brand { Name = "Honda", Year = 1948 },
                    new Brand { Name = "Hyundai", Year = 1947 },
                    new Brand { Name = "Infiniti", Year = 1989 },
                    new Brand { Name = "Jaguar", Year = 1922 },
                    new Brand { Name = "Jeep", Year = 1941 },
                    new Brand { Name = "Kia", Year = 1952 },
                    new Brand { Name = "Koenigsegg", Year = 1994 },
                    new Brand { Name = "Lamborghini", Year = 1963 },
                    new Brand { Name = "Land Rover", Year = 1947 },
                    new Brand { Name = "Lexus", Year = 2005 },
                    new Brand { Name = "Maserati", Year = 1926 },
                    new Brand { Name = "Mazda", Year = 1920 },
                    new Brand { Name = "McLaren", Year = 1985 },
                    new Brand { Name = "Mercedes-Benz", Year = 1926 },
                    new Brand { Name = "Mini", Year = 1969 },
                    new Brand { Name = "Mitsubishi", Year = 1870 },
                    new Brand { Name = "Nissan", Year = 1911 },
                    new Brand { Name = "Pagani", Year = 1992 },
                    new Brand { Name = "Peugeot", Year = 1810 },
                    new Brand { Name = "Porsche", Year = 1931 },
                    new Brand { Name = "Ram Trucks", Year = 1981 },
                    new Brand { Name = "Renault", Year = 1899 },
                    new Brand { Name = "Rolls-Royce", Year = 1894 },
                    new Brand { Name = "Saab", Year = 1945 },
                    new Brand { Name = "Subaru", Year = 1953 },
                    new Brand { Name = "Suzuki", Year = 1909 },
                    new Brand { Name = "Tata Motors", Year = 1945 },
                    new Brand { Name = "Tesla", Year = 2003 },
                    SO,
                    new Brand { Name = "Toyota", Year = 1937 },
                    new Brand { Name = "Volkswagen", Year = 1937 }
                );
                db.Cars.AddRange(
                    new Car { Name = "Spark", Power = 62, Brand = BB, Year = 2009, Price = 140000 },
                    new Car { Name = "Sonic", Power = 138, Brand = BB, Year = 2011, Price = 250000 },
                    new Car { Name = "Bolt", Power = 200, Brand = BB, Year = 2016, Price = 220000 },
                    new Car { Name = "Audi S5", Power = 329, Brand = PG, Year = 2013, Price = 160000 },
                    new Car { Name = "Audi TT", Power = 268, Brand = PG, Year = 2008, Price = 270000 },
                    new Car { Name = "Land Cruiser", Power = 240, Brand = SO, Year = 2007, Price = 310000 },
                    new Car { Name = "Camry", Power = 200, Brand = SO, Year = 2011, Price = 300000 },
                    new Car { Name = "Focus", Power = 180, Brand = PL, Year = 2018, Price = 230000 },
                    new Car { Name = "Fiesta", Power = 180, Brand = PL, Year = 2013, Price = 150000 }
                );

                db.SaveChanges();
            }
        }

        [Authorize(Roles = "admin, moderator, consultant")]
        public async Task<IActionResult> Index(string name, int? brand, SortState sortOrder = SortState.NameAsc, int page = 1)
        {
            int pageSize = 4;

            IQueryable<Car> songs = db.Cars.Include(x => x.Brand);
            if (brand != null && brand != 0)//brand filter
            {
                songs = songs.Where(p => p.BrandId == brand);
            }
            if (!String.IsNullOrEmpty(name))//song name filter
            {
                songs = songs.Where(p => p.Name.Contains(name));
            }

            songs = sortOrder switch
            {
                SortState.NameDes => songs.OrderByDescending(s => s.Name),
                SortState.PowerAsc => songs.OrderBy(s => s.Power),
                SortState.PowerDes => songs.OrderByDescending(s => s.Power),
                SortState.BrandAsc => songs.OrderBy(s => s.Brand.Name),
                SortState.BrandDes => songs.OrderByDescending(s => s.Brand.Name),
                SortState.PriceAsc => songs.OrderBy(s => s.Price),
                SortState.PriceDes => songs.OrderByDescending(s => s.Price),
                _ => songs.OrderBy(s => s.Name),
            };

            var count = await songs.CountAsync();
            var items = await songs.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(db.Brands.ToList(), brand, name),
                Cars = items
            };
            SetClearance();
            return View(viewModel);
        }
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewBag.Select = new SelectList(db.Brands.ToList(), "Id", "Name");
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Car car)
        {
            if (ModelState.IsValid)
            {
                if (car.BrandId == 0)
                {
                    return BadRequest("Incorrect brand id(==0).");
                }
                car.Brand = db.Brands.FirstOrDefault(x => x.Id == car.BrandId);
                if (car.Brand == null)
                {
                    return BadRequest("Incorrect producer id.");
                }
                db.Cars.Add(car);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Select = new SelectList(db.Brands.ToList(), "Id", "Name");
            return View(car);
        }
        [Authorize(Roles = "admin, moderator, consultant")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                DetailsViewModel detailsView = new DetailsViewModel
                {
                    Car = await db.Cars.FirstOrDefaultAsync(p => p.Id == id)
                };
                if (detailsView.Car != null)
                {
                    detailsView.Select = new SelectViewModel(db.Brands.ToList(), detailsView.Car.BrandId);
                    SetClearance();
                    return View(detailsView);
                }
            }
            return NotFound();
        }
        [Authorize(Roles = "admin, moderator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Car car = await db.Cars.FirstOrDefaultAsync(p => p.Id == id);
                if (car != null)
                {
                    ViewBag.Select = new SelectList(db.Brands.ToList(), "Id", "Name", car.BrandId);
                    return View(car);
                }
                else
                {
                    return NotFound();
                }
            }
            return NotFound();
        }
        [Authorize(Roles = "admin, moderator")]
        [HttpPost]
        public async Task<IActionResult> Edit(Car car, int? id)
        {
            if (ModelState.IsValid)
            {
                Car original = db.Cars.AsNoTracking().FirstOrDefault(x => x.Id == id);
                if (original == null)
                {
                    return BadRequest();
                }
                if (car.BrandId == 0)
                {
                    return BadRequest("Incorrect brand id(==0).");
                }
                car.Brand = db.Brands.FirstOrDefault(x => x.Id == car.BrandId);
                if (car.Brand == null)
                {
                    return BadRequest("Incorrect brand id.");
                }
                db.Cars.Update(car);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Select = new SelectList(db.Brands.ToList(), "Id", "Name", car.BrandId);
            return View(car);
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Car car = await db.Cars.FirstOrDefaultAsync(p => p.Id == id);
                if (car != null)
                    return View(car);
            }
            return NotFound();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Car car = new Car { Id = id.Value };
                db.Entry(car).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        [Authorize(Roles = "admin, moderator, consultant")]

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void SetClearance()
        {
            if (User.IsInRole("admin"))
            {
                ViewBag.CL = 3;
            }
            else if (User.IsInRole("moderator"))
            {
                ViewBag.CL = 2;
            }
            else if (User.IsInRole("consultant"))
            {
                ViewBag.CL = 1;
            }
        }
    }
}
