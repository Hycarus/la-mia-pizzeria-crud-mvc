using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        private readonly PizzaContext _context;

        public PizzaController(PizzaContext context)
        {
            _context = context;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var pizzas = _context.Pizzas.ToList();
            return View(pizzas);
        }

        public IActionResult Details(int id)
        {
            Pizza pizza = _context.Pizzas.Find(id);
            if(pizza == null)
            {
                return NotFound();
            }
            return View(pizza);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                _context.Pizzas.Add(pizza);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(pizza);
        }
    }
}

