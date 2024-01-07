using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using System.IO;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Microsoft.Extensions.Logging;
using MimeKit;
using MailKit.Net.Smtp;

namespace Project.Controllers
{
    public class UserController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly ILogger<UserController> _logger;


        public UserController(ModelContext context, IWebHostEnvironment webHostEnviroment , ILogger<UserController> logger)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
            _logger = logger;
        }
        public async Task<IActionResult> HomeUser()
        {
            var id = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            ViewBag.userUser = user;

            var category = await _context.Categories.ToListAsync();
            var crafts = await _context.Crafts.OrderByDescending(x => x.Sales).ToListAsync();
            var final = Tuple.Create<IEnumerable<Category>,IEnumerable<Craft>>(category, crafts);

            return View(final);
        }


        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------




        public async Task<IActionResult> Crafts()
        {
            var id = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userUser = user;


            return View(await _context.Crafts.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Crafts(string searchName, DateTime? datefrom, DateTime? dateto)
        {
            var id = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userUser = user;


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
            var id = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userUser = user;

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
        public async Task<IActionResult> InfoCraft(decimal? Id , string comment )
        {
            var id = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userUser = user;

            Testimonial tes = new Testimonial();

            tes.CraftId = Id;
            tes.UserId = id;
            tes.Coomment = comment;
            tes.Visable = -1;

            _context.Add(tes);
            await _context.SaveChangesAsync();

            ViewBag.test_warning = "We have received your comment, if appropriate, it will be published";

            var adminu = await _context.Crafts.Include(p => p.Category).Where(x => x.Id == Id).FirstOrDefaultAsync();
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrder(  decimal UserId , decimal CraftId , decimal Price , decimal quantity)
        {
            //[Bind("Id,UserId,CraftId,Price,Numpieces,Dateorders")] Order order
            Order order = new Order();
            order.UserId = UserId;
            order.CraftId = CraftId;
            order.Price = Price* quantity;
            order.Numpieces = quantity;
            order.Dateorders = DateTime.Now;


            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Crafts));
            }
            ViewData["CraftId"] = new SelectList(_context.Crafts, "Id", "Name", order.CraftId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Passwordd", order.UserId);
            return View(order);
        }




        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------




        public async Task<IActionResult> EditOrder(decimal? id)
        {
            var ids = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Where(x => x.Id == ids).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userUser = user;

            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(p => p.Craft.Category).Include(p => p.Craft).Include(p => p.User).Where(x => x.Id == id).FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }
            
            return View(order);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditOrder(decimal id, [Bind("Id,UserId,CraftId")] Order order,decimal quantity)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    order.Numpieces = quantity;
                    order.Price = (_context.Crafts.Where(x => x.Id == order.CraftId).AsNoTracking<Craft>().SingleOrDefault().Price) * order.Numpieces;
                    order.Dateorders = _context.Orders.Where(x => x.Id == id).AsNoTracking<Order>().SingleOrDefault().Dateorders;
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MyOrder));
            }
            
            return View(order);
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOrder(decimal id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(MyOrder));
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<IActionResult> MyOrder()
        {
            var id = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userUser = user;

            var modelContext = _context.Orders.Include(o => o.Craft).Include(o => o.User).Where(x=> x.UserId == id);
            return View(await modelContext.ToListAsync());
        }




        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        [HttpGet]
        public IActionResult Credit(string price , string Address)
        {
            var id = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userUser = user;

            
            HttpContext.Session.SetString("Price", price);
            HttpContext.Session.SetString("Address", Address);

            return View(model: price);  
        }

        [HttpPost]
        public async Task<IActionResult> Credit(string cardnumber , string cardname , string expire , decimal? cvv )
        {
            
            var id = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userUser = user;

            var item = _context.Creditcards.Where(x => x.CardNumber == cardnumber && x.Cvv == cvv && x.Name == cardname && x.Expire == expire).SingleOrDefault();
            if (item != null)
            {
                double price = Convert.ToDouble(HttpContext.Session.GetString("Price"));
                double credit = Convert.ToDouble(item.Credit);
                
                if(price < credit)
                {

                   
                    var order = _context.Orders.Where(x => x.UserId == id);
                   
                    foreach(var i in order)
                    {

                        var car = _context.Crafts.Where(x => x.Id == i.CraftId).SingleOrDefault();

                        car.Sales += i.Numpieces;

                        _context.Crafts.Update(car);
                        await _context.SaveChangesAsync();

                        
                    }
                    item.Credit = Convert.ToDecimal(credit - price);
                    _context.Creditcards.Update(item);
                    await _context.SaveChangesAsync();

                    

                    return RedirectToAction("Bill", "User");
                }
                else
                {
                    ViewBag.error = "You don't have enough balance";
                    return View(model: Convert.ToString(price));
                }

            }
            else
            {
                ViewBag.error =  "not found this credit card";
                return View(model: HttpContext.Session.GetString("Price"));
            }

            //ModelState.AddModelError("", "incorrect user name or password");
           

        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        public async Task<IActionResult> Bill()
        {
            var id = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userUser = user;
            ViewBag.Address = HttpContext.Session.GetString("Address");


            var modelContext = _context.Orders.Include(o => o.Craft).Include(o => o.User).Where(x => x.UserId == id);

            return View(await modelContext.ToListAsync());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Bill(decimal Address)
        {
            var id = HttpContext.Session.GetInt32("UserId");
            var order = _context.Orders.Where(x => x.UserId == id);
            foreach(var item in order)
            {
                _context.Remove(item);
                await _context.SaveChangesAsync();

            }

            
            return RedirectToAction(nameof(MyOrder));
        }

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<IActionResult> Profile(decimal? Id)
        {
            var id = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userUser = user;


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
        public async Task<IActionResult> Profile(decimal id, [Bind("Id,Fname,Lname,Username,Email,Passwordd,ImagePath,ImageFile")] User users , string  newpass)
        {
            var ids = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Where(x => x.Id == id).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userUser = user;

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
            var ids = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Where(x => x.Id == ids).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userUser = user;

            return View();
        }
        [HttpPost]
        public IActionResult About(string name, string email, string subject, string messagee)
        {
            var ids = HttpContext.Session.GetInt32("UserId");
            var user = _context.Users.Where(x => x.Id == ids).AsNoTracking<User>().FirstOrDefault();
            ViewBag.userUser = user;

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





        private bool OrderExists(decimal id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }



        private bool UserExists(decimal id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

    }
}
