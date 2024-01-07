using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Models;

namespace Project.Controllers
{
    public class ReCustomersController : Controller
    {
        private readonly ModelContext _context;

        public ReCustomersController(ModelContext context)
        {
            _context = context;
        }

        // GET: ReCustomers
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReCustomers.ToListAsync());
        }

        // GET: ReCustomers/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reCustomer = await _context.ReCustomers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reCustomer == null)
            {
                return NotFound();
            }

            return View(reCustomer);
        }

        // GET: ReCustomers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fname,Lname,ImagePath")] ReCustomer reCust omer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reCustomer);
        }

        // GET: ReCustomers/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reCustomer = await _context.ReCustomers.FindAsync(id);
            if (reCustomer == null)
            {
                return NotFound();
            }
            return View(reCustomer);
        }

        // POST: ReCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Fname,Lname,ImagePath")] ReCustomer reCustomer)
        {
            if (id != reCustomer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReCustomerExists(reCustomer.Id))
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
            return View(reCustomer);
        }

        // GET: ReCustomers/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reCustomer = await _context.ReCustomers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reCustomer == null)
            {
                return NotFound();
            }

            return View(reCustomer);
        }

        // POST: ReCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var reCustomer = await _context.ReCustomers.FindAsync(id);
            _context.ReCustomers.Remove(reCustomer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReCustomerExists(decimal id)
        {
            return _context.ReCustomers.Any(e => e.Id == id);
        }
    }
}
