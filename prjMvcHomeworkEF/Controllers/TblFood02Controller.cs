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
    public class TblFood02Controller : Controller
    {
        private readonly HomeworkDBContext _context;

        public TblFood02Controller(HomeworkDBContext context)
        {
            _context = context;
        }

        // GET: TblFood02
        public async Task<IActionResult> Index()
        {
              return _context.TblFood02s != null ? 
                          View(await _context.TblFood02s.ToListAsync()) :
                          Problem("Entity set 'HomeworkDBContext.TblFood02s'  is null.");
        }

        // GET: TblFood02/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TblFood02s == null)
            {
                return NotFound();
            }

            var tblFood02 = await _context.TblFood02s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblFood02 == null)
            {
                return NotFound();
            }

            return View(tblFood02);
        }

        // GET: TblFood02/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblFood02/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Style,Stars,Price,Comment")] TblFood02 tblFood02)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblFood02);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblFood02);
        }

        // GET: TblFood02/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TblFood02s == null)
            {
                return NotFound();
            }

            var tblFood02 = await _context.TblFood02s.FindAsync(id);
            if (tblFood02 == null)
            {
                return NotFound();
            }
            return View(tblFood02);
        }

        // POST: TblFood02/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Style,Stars,Price,Comment")] TblFood02 tblFood02)
        {
            if (id != tblFood02.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblFood02);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblFood02Exists(tblFood02.Id))
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
            return View(tblFood02);
        }

        // GET: TblFood02/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TblFood02s == null)
            {
                return NotFound();
            }

            var tblFood02 = await _context.TblFood02s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblFood02 == null)
            {
                return NotFound();
            }

            return View(tblFood02);
        }

        // POST: TblFood02/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TblFood02s == null)
            {
                return Problem("Entity set 'HomeworkDBContext.TblFood02s'  is null.");
            }
            var tblFood02 = await _context.TblFood02s.FindAsync(id);
            if (tblFood02 != null)
            {
                _context.TblFood02s.Remove(tblFood02);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblFood02Exists(int id)
        {
          return (_context.TblFood02s?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
