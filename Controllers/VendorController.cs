using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Models;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using MimeKit;
using MailKit.Net.Smtp;

namespace Project.Controllers
{
    public class VendorController : Controller
    {

        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public VendorController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        public async Task<IActionResult> HomeVendor()
        {
            var id = HttpContext.Session.GetInt32("VendorId");
            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.userVendor = user;

            var category = await _context.Categories.ToListAsync();
            var crafts = await _context.Crafts.OrderByDescending(x => x.Sales).ToListAsync();
            var final = Tuple.Create<IEnumerable<Category>, IEnumerable<Craft>>(category, crafts);

            return View(final);
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<IActionResult> Crafts()
        {
            var id = HttpContext.Session.GetInt32("VendorId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userVendor = user;


            return View(await _context.Crafts.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Crafts(string searchName, DateTime? datefrom, DateTime? dateto)
        {
            var id = HttpContext.Session.GetInt32("VendorId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userVendor = user;


            if (String.IsNullOrEmpty(searchName))
            {
                if (datefrom == null && dateto == null)
                {
                    return View(await _context.Crafts.ToListAsync());
                }
                else if (datefrom == null && dateto != null)
                {
                    var craft = await _context.Crafts.Where(x => x.Datecreated.Value.Date <= dateto).ToListAsync();

                    return View(craft);
                }
                else if (datefrom != null && dateto == null)
                {
                    var craft = await _context.Crafts.Where(x => x.Datecreated.Value.Date >= datefrom).ToListAsync();

                    return View(craft);

                }
                else
                {
                    var craft = await _context.Crafts.Where(x => x.Datecreated.Value.Date >= datefrom && x.Datecreated.Value.Date <= dateto).ToListAsync();

                    return View(craft);
                }
            }
            else
            {
                if (datefrom == null && dateto == null)
                {
                    return View(await _context.Crafts.Where(x => x.Name.Contains(searchName)).ToListAsync());
                }
                else if (datefrom == null && dateto != null)
                {
                    var craft = await _context.Crafts.Where(x => x.Datecreated.Value.Date <= dateto && x.Name.Contains(searchName)).ToListAsync();

                    return View(craft);
                }
                else if (datefrom != null && dateto == null)
                {
                    var craft = await _context.Crafts.Where(x => x.Datecreated.Value.Date >= datefrom && x.Name.Contains(searchName)).ToListAsync();

                    return View(craft);

                }
                else
                {
                    var craft = await _context.Crafts.Where(x => x.Datecreated.Value.Date >= datefrom && x.Datecreated.Value.Date <= dateto && x.Name.Contains(searchName)).ToListAsync();

                    return View(craft);
                }
            }


        }








        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        public async Task<IActionResult> InfoCraft(decimal? Id)
        {
            var id = HttpContext.Session.GetInt32("VendorId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userVendor = user;

            if (Id == null)
            {
                return NotFound();
            }

            var adminu = await _context.Crafts.Include(p => p.Category).Where(x=> x.Id == Id).FirstOrDefaultAsync();
            if (adminu == null)
            {
                return NotFound();
            }


            var test = await _context.Testimonials.Where(x => x.CraftId == Id && x.Visable == 1).Include(x => x.Craft).Include(x => x.User).ToListAsync();
            var final = Tuple.Create<Craft, IEnumerable<Testimonial>>(adminu, test);

            return View(final);
        }



        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        [HttpGet]
        public IActionResult CreateCrafts()
        {
            var id = HttpContext.Session.GetInt32("VendorId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userVendor = user;

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCrafts([Bind("Name,Price,Description,Imagepath,CategoryId,ImageFile")] Craft craft)
        {
            var id = HttpContext.Session.GetInt32("VendorId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userVendor = user;

            if (ModelState.IsValid)
            {
                if (craft.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnviroment.WebRootPath; // wwwrootpath
                    string imageName = Guid.NewGuid().ToString() + "_" + craft.ImageFile.FileName; // image name
                    string path = Path.Combine(wwwrootPath + "/Images/", imageName); // wwwroot/Image/imagename

                    using (var filestream = new FileStream(path, FileMode.Create))
                    {
                        await craft.ImageFile.CopyToAsync(filestream);
                    }
                    craft.Imagepath = imageName;
                    craft.Sales = 0;
                    craft.Datecreated  = DateTime.Now;
                    craft.UserId = id;
                    _context.Add(craft);
                    await _context.SaveChangesAsync();
                }
                
                return RedirectToAction(nameof(MyCrafts));
            }
            return View(craft);
        }










        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        [HttpGet]
        public async Task<IActionResult> MyCrafts()
        {
            var id = HttpContext.Session.GetInt32("VendorId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userVendor = user;
           

            var adminu = await _context.Crafts.Where(x => x.UserId == id).ToListAsync();
            if (adminu == null)
            {
                return NotFound();
            }
            return View(adminu);

        }



        [HttpPost]
        public async Task<IActionResult> MyCrafts(string searchName)
        {
            var id = HttpContext.Session.GetInt32("VendorId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userVendor = user;


            if (String.IsNullOrEmpty(searchName))
            {
                return View(await _context.Crafts.Where(x => x.UserId == id).ToListAsync());
            }
            // Pass your list out to your view
            return View(await _context.Crafts.Where(p => p.Name.Contains(searchName) || p.Price.ToString().Contains(searchName) &&  p.UserId == id).ToListAsync());

        }










        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<IActionResult> EditCraft(decimal? Id)
        {
            var id = HttpContext.Session.GetInt32("VendorId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userVendor = user;


            
            ViewData["catid"] = new SelectList(_context.Categories, "Id", "Name");

            if (Id == null)
            {
                return NotFound();
            }

            var adminu = await _context.Crafts.Include(p => p.Category).Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (adminu == null)
            {
                return NotFound();
            }


            var test = await _context.Testimonials.Where(x => x.CraftId == Id && x.Visable == 1).Include(x => x.Craft).Include(x => x.User).ToListAsync();
            var final = Tuple.Create<Craft, IEnumerable<Testimonial>>(adminu, test);

            return View(final);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCraft(decimal id, [Bind("Id,ImagePath,ImageFile,Name,CategoryId,Price,Description")] Craft craft)
        {
            var ids = HttpContext.Session.GetInt32("VendorId");
            var user = _context.Users.Where(x => x.Id == ids).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userVendor = user;

            craft.Sales = _context.Crafts.Where(x => x.Id == id).AsNoTracking<Craft>().SingleOrDefault().Sales;
            craft.UserId = ids;
            craft.Datecreated = _context.Crafts.Where(x => x.Id == id).AsNoTracking<Craft>().SingleOrDefault().Datecreated;

            if (id != craft.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (craft.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + craft.ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await craft.ImageFile.CopyToAsync(fileStream);
                        }
                        craft.Imagepath = fileName;
                            
                    }
                    else
                    {
                        craft.Imagepath = _context.Crafts.Where(x => x.Id == craft.Id).AsNoTracking<Craft>().SingleOrDefault().Imagepath;
                    }
                    _context.Update(craft);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(craft.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MyCrafts));
            }
            return View(craft);
        }



        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCrafts(decimal id)
        {
            var order = await _context.Crafts.FindAsync(id);
            _context.Crafts.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MyCrafts));
        }


        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<IActionResult> Profile(decimal? Id)
        {
            var id = HttpContext.Session.GetInt32("VendorId");  
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userVendor = user;


            if (Id == null)
            {
                return NotFound();
            }

            var adminu = await _context.Users.FindAsync(Id);
            if (adminu == null)
            {
                return NotFound();
            }
            return View(adminu);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(decimal id, [Bind("Id,Fname,Lname,Username,Email,Passwordd,ImagePath,ImageFile")] User users, string newpass)
        {
            var ids = HttpContext.Session.GetInt32("VendorId");
            var user = _context.Users.Where(x => x.Id == ids).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userVendor = user;

            users.Isaccepted = user.Isaccepted;
            users.Datecreated = user.Datecreated;
            users.RoleId = user.RoleId;

            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (newpass != null)
                {
                    if (users.Passwordd == user.Passwordd)
                    {
                        users.Passwordd = newpass;
                    }
                    else
                    {
                        ViewBag.pass = "Wrong Passowrd";
                        return View();
                    }
                }
                else
                {
                    if (users.Passwordd == user.Passwordd)
                    {
                        users.Passwordd = user.Passwordd;
                    }
                    else
                    {
                        ViewBag.pass = "Wrong Passowrd";
                        return View();
                    }

                }

                try
                {
                    if (users.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + users.ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await users.ImageFile.CopyToAsync(fileStream);
                        }
                        users.Imagepath = fileName;

                    }
                    else
                    {
                        users.Imagepath = _context.Users.Where(x => x.Id == users.Id).AsNoTracking<User>().SingleOrDefault().Imagepath;
                    }


                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(users.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Profile));
            }
            return View(users);
        }



        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public IActionResult About()
        {
            var ids = HttpContext.Session.GetInt32("VendorId");
            var user = _context.Users.Where(x => x.Id == ids).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userVendor = user;

            return View();
        }
        [HttpPost]
        public IActionResult About(string name, string email, string subject, string messagee)
        {
            var ids = HttpContext.Session.GetInt32("VendorId");
            var user = _context.Users.Where(x => x.Id == ids).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userVendor = user;

            if (ModelState.IsValid)
            {

                try
                {
                    MimeMessage message = new MimeMessage();
                    MailboxAddress from = new MailboxAddress("HandCraft", email);
                    message.From.Add(from);
                    MailboxAddress to = new MailboxAddress("User", "yosefbudair22@outlook.com");
                    message.To.Add(to);
                    message.Subject = subject;
                    BodyBuilder bodyBuilder = new BodyBuilder();
                    bodyBuilder.HtmlBody = "<p sty1e=\"color:blue\">HandCraft</p>" + "<p>We accept you as Vendor in HandCraft Website</p>";

                    message.Body = bodyBuilder.ToMessageBody();
                    using (var client = new SmtpClient())
                    {

                        client.Connect("smtp-mail.outlook.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                        client.Authenticate("yosefbudair22@outlook.com", "yosef123");
                        client.Send(message);
                        client.Disconnect(true);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;

                }


                return RedirectToAction(nameof(Index));
            }



            return View();
        }







        private bool UserExists(decimal id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

    }
}
