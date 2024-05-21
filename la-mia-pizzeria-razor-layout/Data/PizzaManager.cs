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

        public static List<Ingredient> GetAllIngredients()
        {
            using PizzaContext db = new PizzaContext();
            return db.Ingredients.ToList();
        }

        public static Pizza GetPizza(int id, bool includeReferences = true)
        {
            using PizzaContext db = new PizzaContext();
            if (includeReferences)
                return db.Pizzas.Where(p => p.Id == id).Include(p => p.Category).Include(i => i.Ingredients).FirstOrDefault();
            return db.Pizzas.FirstOrDefault(p => p.Id == id);
        }

        public static Ingredient GetIngredientById(int id)
        {
            using PizzaContext db = new PizzaContext();
            return db.Ingredients.FirstOrDefault(i => i.Id == id);
        }

        public static void InsertPizza(Pizza Pizza, List<string> SelectedIngredients = null)
        {
            using PizzaContext db = new PizzaContext();
            if(SelectedIngredients != null)
            {
                Pizza.Ingredients = new List<Ingredient>();
                foreach(var ingredientId in SelectedIngredients)
                {
                    int id = int.Parse(ingredientId);
                    var ingredient = db.Ingredients.FirstOrDefault(i => i.Id == id);
                    Pizza.Ingredients.Add(ingredient);
                }
            }
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

        public static bool UpdatePizza(int id, string name, string overview, int? categoryId, List<string> ingredients)
        {
            using PizzaContext db = new PizzaContext();
            var pizza = db.Pizzas.Where(x => x.Id == id).Include(x => x.Ingredients).FirstOrDefault(p => p.Id == id);

            if (pizza == null)
                return false;

            pizza.Name = name;
            pizza.Overview = overview;
            pizza.CategoryId = categoryId;

            pizza.Ingredients.Clear();
            if(ingredients != null)
            {
                foreach(var ingredient in ingredients)
                {
                    int ingredientId = int.Parse(ingredient);
                    var ingredientFromDb = db.Ingredients.FirstOrDefault(x => x.Id == ingredientId);
                    pizza.Ingredients.Add(ingredientFromDb);
                }
            }

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