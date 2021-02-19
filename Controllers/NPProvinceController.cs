using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPClubs.Models;
using Microsoft.EntityFrameworkCore;

namespace NPClubs.Controllers
{
    public class NPProvinceController : Controller
    {
        private readonly ClubsContext _context;

        public NPProvinceController(ClubsContext context)
        {
            _context = context;
        }

        // GET: NPCountryController
        // This action will retrives and display all the country records from country table and pass them to the view
        public async Task<IActionResult> Index()
        {
            var countries = await _context.Province.ToListAsync();
            return View(countries);
        }


        /*public IActionResult Index()
        {
            return View();
        }*/
    }
}
