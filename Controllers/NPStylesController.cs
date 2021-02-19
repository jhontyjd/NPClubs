using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NPClubs.Models;

namespace NPClubs.Controllers
{
    // NPStylesController accesses and maintains the styles table, The database connection is provided to the controlled by dependency injection 
    public class NPStylesController : Controller
    {
        private readonly ClubsContext _context;

        public NPStylesController(ClubsContext context)
        {
            _context = context;
        }

        // GET: NPStyles
        // This action will retrives and display all the styles records from style table and pass them to the view
        public async Task<IActionResult> Index()
        {
            return View(await _context.Style.ToListAsync());
        }

        // GET: NPStyles/Details/5
        // This action will retrives and display selected  styles record from Styles table and pass them to the view, There are some exception if id or style is null it will display not found message
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var style = await _context.Style
                .FirstOrDefaultAsync(m => m.StyleName == id);
            if (style == null)
            {
                return NotFound();
            }

            return View(style);
        }

        // GET: NPStyles/Create
        // this actin will provide us an empty page for user to enter a new Styles record, we are not passing any model to the view
        public IActionResult Create()
        {
            return View();
        }

        // POST: NPStyles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // this action will post  a new style record if it passes the edit, otherwise it will return it to the new page with eror message 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StyleName,Description")] Style style)
        {
            if (ModelState.IsValid)
            {
                _context.Add(style);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(style);
        }

        // GET: NPStyles/Edit/5
        //This action will retrives the selected style record and display it to for update, as a exception if id or style is null, will return not found
      
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var style = await _context.Style.FindAsync(id);
            if (style == null)
            {
                return NotFound();
            }
            return View(style);
        }

        // POST: NPStyles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // This action is post, when we click save button on a webpage it will save the data and pass it to the view and will return back to index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StyleName,Description")] Style style)
        
        {
         if (id != style.StyleName)
         {
         return NotFound();
        }

         if (ModelState.IsValid)
        {
            try
          {
           _context.Update(style);
            await _context.SaveChangesAsync();
        }
         catch (DbUpdateConcurrencyException)
         {
          if (!StyleExists(style.StyleName))
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
         return View(style);
         }

        // GET: NPStyles/Delete/5
        //This action will display the selected style record for delete confirmation
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var style = await _context.Style
                .FirstOrDefaultAsync(m => m.StyleName == id);
            if (style == null)
            {
                return NotFound();
            }

            return View(style);
        }

        // POST: NPStyles/Delete/5
        //Post action will allow delete style record from the database, and it will redirect us to index webpage
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var style = await _context.Style.FindAsync(id);
            _context.Style.Remove(style);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StyleExists(string id)
        {
            return _context.Style.Any(e => e.StyleName == id);
        }
    }
}

