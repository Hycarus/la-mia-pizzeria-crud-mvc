using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Controllers
{
    public class pizzaController : Controller
    {

        [Authorize(Roles = "ADMIN, USER")]
        public IActionResult Index()
        {
            return View(PizzaManager.GetAllPizzas());
        }

        [Authorize(Roles = "ADMIN, USER")]
        public IActionResult Details(int id)
        {
            var pizza = PizzaManager.GetPizza(id, true);
            if (pizza != null)
                return View(pizza);
            else
                return View("errore");
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Create()
        {
            PizzaFormModel model = new PizzaFormModel();
            model.Pizza = new Pizza();
            model.Categories = PizzaManager.GetAllCategories();
            model.CreateIngredients();
            ViewBag.Categories = new SelectList(model.Categories, "Id", "Name");
            return View(model);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(data.Categories, "Id", "Name");
                data.CreateIngredients();
                return View("Create", data);
            }

            PizzaManager.InsertPizza(data.Pizza, data.SelectedIngredients);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Edit(int id)
        {

            var pizzaToEdit = PizzaManager.GetPizza(id);

            if (pizzaToEdit == null)
            {
                return NotFound();
            }
            else
            {
                PizzaFormModel model = new PizzaFormModel(pizzaToEdit, PizzaManager.GetAllCategories());
                ViewBag.Categories = new SelectList(model.Categories, "Id", "Name", pizzaToEdit.CategoryId);
                model.CreateIngredients();
                return View(model);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PizzaFormModel data)
        {
            
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(PizzaManager.GetAllCategories(), "Id", "Name", data.Pizza.CategoryId);
                //data.Categories = PizzaManager.GetAllCategories();
                data.CreateIngredients();
                return View("Edit", data);
            }

            var pizzaToEdit = PizzaManager.UpdatePizza(id, data.Pizza, data.SelectedIngredients);

            if (pizzaToEdit)
                return RedirectToAction("Index");
            else
                return NotFound();
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (PizzaManager.DeletePizza(id))
                return RedirectToAction("Index");
            else
                return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}