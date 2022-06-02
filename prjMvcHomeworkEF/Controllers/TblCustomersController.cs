using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prjMvcHomeworkEF.Models;

namespace prjMvcHomeworkEF.Controllers
{
    public class TblCustomersController : Controller
    {
        private readonly HomeworkDBContext _context;

        public TblCustomersController(HomeworkDBContext context)
        {
            _context = context;
        }

        // GET: TblCustomers
        public async Task<IActionResult> Index()
        {
              return _context.TblCustomers != null ? 
                          View(await _context.TblCustomers.ToListAsync()) :
                          Problem("Entity set 'HomeworkDBContext.TblCustomers'  is null.");
        }

        // GET: TblCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblCustomers == null)
            {
                return NotFound();
            }

            var tblCustomer = await _context.TblCustomers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblCustomer == null)
            {
                return NotFound();
            }

            return View(tblCustomer);
        }

        // GET: TblCustomers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Money")] TblCustomer tblCustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblCustomer);
        }

        // GET: TblCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblCustomers == null)
            {
                return NotFound();
            }

            var tblCustomer = await _context.TblCustomers.FindAsync(id);
            if (tblCustomer == null)
            {
                return NotFound();
            }
            return View(tblCustomer);
        }

        // POST: TblCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Money")] TblCustomer tblCustomer)
        {
            if (id != tblCustomer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCustomerExists(tblCustomer.Id))
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
            return View(tblCustomer);
        }

        // GET: TblCustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblCustomers == null)
            {
                return NotFound();
            }

            var tblCustomer = await _context.TblCustomers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblCustomer == null)
            {
                return NotFound();
            }

            return View(tblCustomer);
        }

        // POST: TblCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblCustomers == null)
            {
                return Problem("Entity set 'HomeworkDBContext.TblCustomers'  is null.");
            }
            var tblCustomer = await _context.TblCustomers.FindAsync(id);
            if (tblCustomer != null)
            {
                _context.TblCustomers.Remove(tblCustomer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblCustomerExists(int id)
        {
          return (_context.TblCustomers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
