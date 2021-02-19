using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPClubs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NPClubs.Controllers 
    // NPCountryController accesses and maintains the country table, The database connection is provided to the controlled by dependency injection 
{
    public class NPCountryController : Controller
    {
        private readonly ClubsContext _context;

        public NPCountryController(ClubsContext context)
        {
            _context = context;
        }


        // GET: NPCountryController
        // This action will retrives and display all the country records from country table and pass them to the view
        public async Task<IActionResult> Index()
        {
            var countries =await _context.Country.ToListAsync();
            return View(countries);
        }

        // GET: NPCountryController/Details/5
        //This action will retrives only one coutnry and displays province and country code related to that country and pass them to the view. There are some exception if id or country is null will return not found message
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var country = _context.Country.Find(id);
            var country = await _context.Country.Include(c=> c.Province).FirstOrDefaultAsync(c => c.CountryCode.Equals(id));
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: NPCountryController/Create
        // this actin will provide us an empty page for user to enter a new country record, we are not passing any model to the view
        public ActionResult Create()
        {
            return View();
        }

        // POST: NPCountryController/Create
        // this action will insert a new country record if it passes the edit, otherwise it will return it to the new page with eror message 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Country country)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(country);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(country);
            }
            catch
            {
                return View(country);
            }
        }

        // GET: NPCountryController/Edit/5
        //This action will retrives the selected country data and display it to for update, as a exception if id is null will throug a not found
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var country = await _context.Country.FirstOrDefaultAsync(c => c.CountryCode.Equals(id));
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: NPCountryController/Edit/5
        // This action is post, when we click save button on a webpage it will save the data and pass it to theview and will return back to index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Country country)
        {
            if (ModelState.IsValid)
            {
              _context.Update(country);
              _context.SaveChanges();
              return RedirectToAction(nameof(Index));
            }
                return View(country);
        }

        // GET: NPCountryController/Delete/5
        //This action will display the selected country record for delete confirmation 
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var country = await _context.Country.FirstOrDefaultAsync(c => c.CountryCode.Equals(id));

            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: NPCountryController/Delete/5
        //Post action will allow delete country from the database, and it will redirect us to index webpage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Country model)
        {
            try
            {
                var country = await _context.Country.FirstOrDefaultAsync(c => c.CountryCode.Equals(model.CountryCode));
                _context.Country.Remove(country);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
