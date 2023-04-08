using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using opinions.Data;
using opinions.Models;

namespace opinions.Controllers
{
    public class opinionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public opinionsController(ApplicationDbContext context)
        {
            _context = context;
        }


        
        // GET: opinions
        public async Task<IActionResult> Index()
        {
            
              return _context.opinion != null ? 
                          View(await _context.opinion.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.opinion'  is null.");
        }
        public async Task<IActionResult> SearchOpinions()
        {

            return View();
        }
        public async Task<IActionResult> ShowResult(string SearchPhrase)
        {

            return View("Index" , await _context.opinion.Where(o => o.myopinion.Contains(SearchPhrase)).ToListAsync());
        }
        public async Task<IActionResult> myOpinions()
        {
            return _context.opinion != null ?
                        View("IndexMyOp", await _context.opinion.Where(op=>op.username == HttpContext.User.Identity.Name).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.opinion'  is null.");
        }
        // GET: opinions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.opinion == null)
            {
                return NotFound();
            }

            var opinion = await _context.opinion
                .FirstOrDefaultAsync(m => m.id == id);
            if (opinion == null)
            {
                return NotFound();
            }

            return View(opinion);
        }

        // GET: opinions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: opinions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,username,partOfCourse,myopinion")] opinion opinion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(opinion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(opinion);
        }

        // GET: opinions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.opinion == null)
            {
                return NotFound();
            }

            var opinion = await _context.opinion.FindAsync(id);
            if (opinion == null)
            {
                return NotFound();
            }
            return View(opinion);
        }

        // POST: opinions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,username,partOfCourse,myopinion")] opinion opinion)
        {
            if (id != opinion.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(opinion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!opinionExists(opinion.id))
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
            return View(opinion);
        }

        // GET: opinions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.opinion == null)
            {
                return NotFound();
            }

            var opinion = await _context.opinion
                .FirstOrDefaultAsync(m => m.id == id);
            if (opinion == null)
            {
                return NotFound();
            }

            return View(opinion);
        }

        // POST: opinions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.opinion == null)
            {
                return Problem("Entity set 'ApplicationDbContext.opinion'  is null.");
            }
            var opinion = await _context.opinion.FindAsync(id);
            if (opinion != null)
            {
                _context.opinion.Remove(opinion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool opinionExists(int id)
        {
          return (_context.opinion?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
