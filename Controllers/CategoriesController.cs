using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoriesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var ids = HttpContext.Session.GetInt32("AdminId");
            var user = _context.Users.Where(x => x.Id == ids).FirstOrDefault();
            ViewBag.userAdmin = user;
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            var ids = HttpContext.Session.GetInt32("AdminId");
            var user = _context.Users.Where(x => x.Id == ids).FirstOrDefault();
            ViewBag.userAdmin = user;
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            var ids = HttpContext.Session.GetInt32("AdminId");
            var user = _context.Users.Where(x => x.Id == ids).FirstOrDefault();
            ViewBag.userAdmin = user;
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Imagepath, ImageFile")] Category category)
        {
            var ids = HttpContext.Session.GetInt32("AdminId");
            var user = _context.Users.Where(x => x.Id == ids).FirstOrDefault();
            ViewBag.userAdmin = user;
            if (ModelState.IsValid)
            {
                if (category.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnvironment.WebRootPath; // wwwrootpath
                    string imageName = Guid.NewGuid().ToString() + "_" + category.ImageFile.FileName; // image name
                    string path = Path.Combine(wwwrootPath + "/Images/", imageName); // wwwroot/Image/imagename

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await category.ImageFile.CopyToAsync(filestream);
                    }
                    category.Imagepath = imageName;
                    _context.Add(category);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            var ids = HttpContext.Session.GetInt32("AdminId");
            var user = _context.Users.Where(x => x.Id == ids).FirstOrDefault();
            ViewBag.userAdmin = user;
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Imagepath, ImageFile")] Category category)
        {
            var ids = HttpContext.Session.GetInt32("AdminId");
            var user = _context.Users.Where(x => x.Id == ids).FirstOrDefault();
            ViewBag.userAdmin = user;

            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (category.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + category.ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await category.ImageFile.CopyToAsync(fileStream);
                        }
                        category.Imagepath = fileName;

                    }
                    else
                    {
                        category.Imagepath = _context.Categories.Where(x => x.Id == category.Id).AsNoTracking<Category>().SingleOrDefault().Imagepath;
                    }
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            var ids = HttpContext.Session.GetInt32("AdminId");
            var user = _context.Users.Where(x => x.Id == ids).FirstOrDefault();
            ViewBag.userAdmin = user;
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var ids = HttpContext.Session.GetInt32("AdminId");
            var user = _context.Users.Where(x => x.Id == ids).FirstOrDefault();
            ViewBag.userAdmin = user;
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(decimal id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
