using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ModelContext _context;
       
        private readonly ILogger<HomeController> _logger;

        public HomeController(ModelContext context, ILogger<HomeController> logger)
        {
            _context = context;           
            _logger = logger;
        }
        


        public async Task<IActionResult> Index()
        {
            var category = await _context.Categories.ToListAsync();
            var crafts = await _context.Crafts.OrderByDescending(x => x.Sales).ToListAsync();
            var final = Tuple.Create<IEnumerable<Category>, IEnumerable<Craft>>(category, crafts);

            return View(final);

           
        }


        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<IActionResult> Crafts()
        {
            
            return View(await _context.Crafts.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Crafts(string searchName , DateTime? datefrom, DateTime? dateto )
        {
            

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



        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public IActionResult About()
        {

          return View();
        }
        [HttpPost]
        public IActionResult About(string name , string email , string subject , string messagee)
        {

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

        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
