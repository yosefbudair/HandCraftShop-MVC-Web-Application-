using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using System.Collections.Generic;

namespace Project.Controllers
{

    public class AdminController : Controller
    {

        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public AdminController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        public IActionResult HomeAdmin()
        {
            var id  = HttpContext.Session.GetInt32("AdminId");
            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.userAdmin = user;

            ViewBag.numofcrafts = _context.Crafts.Count();
            ViewBag.numofcategory = _context.Categories.Count();
            ViewBag.numofusers = _context.Users.Count(x=> x.RoleId == 3);
            ViewBag.numofvendor = _context.Users.Count(x => x.RoleId == 2);
            ViewBag.numofsales = _context.Crafts.Count(x=> x.Sales > 0 );

            var categoryname = _context.Categories.Select(x => x.Name).ToList();

            List<int> count = new List<int>();
            foreach(var item in categoryname)
            {
                count.Add(_context.Crafts.Count(x => x.Category.Name == item));
            }
            ViewBag.count = count;
            ViewBag.categoryname = categoryname;
            
            var craftsale = _context.Crafts.Where(x => x.Sales > 0).ToList();
           
            double Amountearned = 0;
            int allseals = 0;


            foreach(var item in craftsale)
            {
                Amountearned += Convert.ToDouble(item.Sales * item.Price);
                allseals += Convert.ToInt32(item.Sales);
            }

            ViewBag.Amountearned = Amountearned;
            ViewBag.allseals = allseals;
            
            var crafts =  _context.Crafts.OrderByDescending(x => x.Sales).ToList();



            var vendor = _context.Users.Where(x => x.RoleId == 2 && x.Isaccepted == 1);



            var final = Tuple.Create<IEnumerable<Craft>, IEnumerable<User>>( crafts , vendor);

            return View(final);         
        }


        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        public async Task<IActionResult> Requisites()
        {
            var id = HttpContext.Session.GetInt32("AdminId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userAdmin = user;           
            return View(await _context.Users.Where(x => x.RoleId == 2 && x.Isaccepted == -1).ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Requisites(decimal Id , decimal Isaccepted)
        {
            var item = _context.Users.Where(x => x.Id == Id).SingleOrDefault();
           

            if (ModelState.IsValid)
            {
                try
                {
                    MimeMessage message = new MimeMessage();
                    MailboxAddress from = new MailboxAddress("HandCraft", "yosefbudair22@outlook.com");
                    message.From.Add(from);
                    MailboxAddress to = new MailboxAddress("Vendor", item.Email);
                    message.To.Add(to);
                    message.Subject = "Verefication";
                    BodyBuilder bodyBuilder = new BodyBuilder();

                    if (Isaccepted == 1)
                    {
                        item.Isaccepted = 1;
                        bodyBuilder.HtmlBody = "<p sty1e=\"color:blue\">HandCraft</p>" + "<p>We accept you as Vendor in HandCraft Website</p>";
                    }
                    else
                    {
                        item.Isaccepted = 0;
                        bodyBuilder.HtmlBody = "<p sty1e=\"color:blue\">HandCraft</p>" + "<p>Sorry We denied you as Vendor in HandCraft Website</p>";
                    }
                    _context.Update(item);
                    await _context.SaveChangesAsync();
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
                    if (!UserExists(Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Requisites));
            }
            return View(item);
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------




        public async Task<IActionResult> RequisitesComment()
        {
            var id = HttpContext.Session.GetInt32("AdminId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userAdmin = user;

            return View(await _context.Testimonials.Where(x => x.Visable == -1).Include(x => x.Craft).Include(x => x.User).ToListAsync());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RequisitesComment(decimal Id, decimal visable)
        {
            var item = _context.Testimonials.Where(x => x.Id == Id).SingleOrDefault();


            if (ModelState.IsValid)
            {
                try
                {
                   

                    if (visable == 1)
                    {
                        item.Visable = 1;
                        _context.Update(item);
                        await _context.SaveChangesAsync();

                    }
                    else
                    {
                        _context.Testimonials.Remove(item);
                        await _context.SaveChangesAsync();

                    }
                   
                    

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(RequisitesComment));
            }
            return View(item);
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<IActionResult> Profile(decimal? Id)
        {
            var id = HttpContext.Session.GetInt32("AdminId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userAdmin = user;


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
            var ids = HttpContext.Session.GetInt32("AdminId");
            var user = _context.Users.Where(x => x.Id == ids).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userAdmin = user;

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








        private bool UserExists(decimal id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
