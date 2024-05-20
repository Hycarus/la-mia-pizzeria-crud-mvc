using la_mia_pizzeria_static.Data;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace la_mia_pizzeria_static.Controllers
{
    public class pizzaController : Controller
    {
      
        public IActionResult Index()
        {
            return View(PizzaManager.GetAllPizzas());
        }

        public IActionResult Details(int id)
        {
            var pizza = PizzaManager.GetPizza(id, true);
            if (pizza != null)
                return View(pizza);
            else
                return View("errore");
        }


        [HttpGet]
        public IActionResult Create()
        {
            PizzaFormModel model = new PizzaFormModel();
            model.Pizza = new Pizza();
            model.Categories = PizzaManager.GetAllCategories();
            ViewBag.Categories = new SelectList(model.Categories, "Id", "Name");
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(data.Categories, "Id", "Name");
                return View("Create", data);
            }

            PizzaManager.InsertPizza(data.Pizza);
            return RedirectToAction("Index");
        }

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
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(data.Categories, "Id", "Name", data.Pizza.CategoryId);
                return View("Update", data);
            }

            bool result = PizzaManager.UpdatePizza(id, pizzaToEdit =>
            {
                pizzaToEdit.Name = data.Pizza.Name;
                pizzaToEdit.Overview = data.Pizza.Overview;
                pizzaToEdit.CategoryId = data.Pizza.CategoryId;
            });

            if (result == true)
                return RedirectToAction("Index");
            else
                return NotFound();
        }

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