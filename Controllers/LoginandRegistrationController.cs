using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project.Controllers
{
    public class LoginandRegistrationController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public LoginandRegistrationController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        
        [HttpPost]
        public IActionResult Login([Bind("Username , Passwordd")] User Users)
        {
            var item = _context.Users.Where(x => x.Username == Users.Username && x.Passwordd == Users.Passwordd).SingleOrDefault();
            if (item != null)
            {
                if (item.Isaccepted == 1)
                {
                    switch (item.RoleId)
                    {
                        case 1:
                            {
                                HttpContext.Session.SetInt32("AdminId", (int)item.Id);
                                return RedirectToAction("HomeAdmin", "Admin");

                            }
                        case 2:
                            {
                                HttpContext.Session.SetInt32("VendorId", (int)item.Id);
                                return RedirectToAction("HomeVendor", "Vendor");
                            }
                        case 3:
                            {
                                HttpContext.Session.SetInt32("UserId", (int)item.Id);
                                return RedirectToAction("HomeUser", "User");
                            }

                    }
                }

            }
            else
            {
                ViewBag.ErrorMessage = "Email or Password not found";
                return View();
            }

            //ModelState.AddModelError("", "incorrect user name or password");
            return View();


        }


        
        [HttpGet]
        public IActionResult Register()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles.Where(x=> x.Id==2 || x.Id==3), "Id", "Name");
            return View();
            
        }
        
        [HttpPost]
        public async Task<IActionResult> RegisterAsync([Bind("Fname,Lname,Username,Email,Passwordd,RoleId")] User users)
        {
            if (ModelState.IsValid)
            {
                
                    
                   if(users.RoleId == 3)
                    {
                        users.Isaccepted = 1;
                    }
                   else
                    {
                    users.Isaccepted = -1;
                    }

                    users.Imagepath = "user.png";
                    users.Datecreated = DateTime.Now;

                    _context.Add(users);
                    await _context.SaveChangesAsync();
                
                return RedirectToAction("Login");
            }
            return View(users);
        }
        

    }


}