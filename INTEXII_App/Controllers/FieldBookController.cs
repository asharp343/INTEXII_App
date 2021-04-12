using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using INTEXII_App.Models;

namespace INTEXII_App.Controllers
{
    public class FieldBookController : Controller
    {
        private readonly BYU_ExcavationContext _context;

        public FieldBookController(BYU_ExcavationContext context)
        {
            _context = context;
        }

        // GET: FieldBook
        public async Task<IActionResult> Index()
        {
            return View(await _context.FieldBooks.ToListAsync());
        }

        // GET: FieldBook/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fieldBook = await _context.FieldBooks
                .FirstOrDefaultAsync(m => m.FieldBookId == id);
            if (fieldBook == null)
            {
                return NotFound();
            }

            return View(fieldBook);
        }

        // GET: FieldBook/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FieldBook/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FieldBookId,Name,Description")] FieldBook fieldBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fieldBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fieldBook);
        }

        // GET: FieldBook/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fieldBook = await _context.FieldBooks.FindAsync(id);
            if (fieldBook == null)
            {
                return NotFound();
            }
            return View(fieldBook);
        }

        // POST: FieldBook/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("FieldBookId,Name,Description")] FieldBook fieldBook)
        {
            if (id != fieldBook.FieldBookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //var test = _context.Model(fieldBook.FieldBookId);


                    _context.Update(fieldBook); ///test
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FieldBookExists(fieldBook.FieldBookId))
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
            return View(fieldBook);
        }

        // GET: FieldBook/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fieldBook = await _context.FieldBooks
                .FirstOrDefaultAsync(m => m.FieldBookId == id);
            if (fieldBook == null)
            {
                return NotFound();
            }

            return View(fieldBook);
        }

        // POST: FieldBook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var fieldBook = await _context.FieldBooks.FindAsync(id);
            _context.FieldBooks.Remove(fieldBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FieldBookExists(decimal id)
        {
            return _context.FieldBooks.Any(e => e.FieldBookId == id);
        }
    }
}
