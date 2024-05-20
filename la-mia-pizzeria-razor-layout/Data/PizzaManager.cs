using la_mia_pizzeria_static.Models;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Data
{
    public static class PizzaManager
    {
        public static int CountAllPizzas()
        {
            using PizzaContext db = new PizzaContext();
            return db.Pizzas.Count();
        }

        public static List<Pizza> GetAllPizzas()
        {
            using PizzaContext db = new PizzaContext();
            return db.Pizzas.Include(c => c.Category).ToList();
        }

        public static List<Category> GetAllCategories()
        {
            using PizzaContext db = new PizzaContext();
            return db.Categories.ToList();
        }

        public static Pizza GetPizza(int id, bool includeReferences = true)
        {
            using PizzaContext db = new PizzaContext();
            if (includeReferences)
                return db.Pizzas.Where(p => p.Id == id).Include(p => p.Category).FirstOrDefault();
            return db.Pizzas.FirstOrDefault(p => p.Id == id);
        }

        public static void InsertPizza(Pizza Pizza)
        {
            using PizzaContext db = new PizzaContext();
            db.Pizzas.Add(Pizza);
            db.SaveChanges();
        }

        public static bool UpdatePizza(int id, Action<Pizza> edit)
        {
            using PizzaContext db = new PizzaContext();
            var pizza = db.Pizzas.FirstOrDefault(p => p.Id == id);

            if (pizza == null)
                return false;

            edit(pizza);

            db.SaveChanges();

            return true;
        }

        public static bool UpdatePizza(int id, string name, string overview, int? categoryId)
        {
            using PizzaContext db = new PizzaContext();
            var pizza = db.Pizzas.FirstOrDefault(p => p.Id == id);

            if (pizza == null)
                return false;

            pizza.Name = name;
            pizza.Overview = overview;
            pizza.CategoryId = categoryId;

            db.SaveChanges();

            return true;
        }

        public static bool DeletePizza(int id)
        {
            using PizzaContext db = new PizzaContext();
            var pizza = db.Pizzas.FirstOrDefault(p => p.Id == id);

            if (pizza == null)
                return false;

            db.Pizzas.Remove(pizza);
            db.SaveChanges();

            return true;
        }
    }
}