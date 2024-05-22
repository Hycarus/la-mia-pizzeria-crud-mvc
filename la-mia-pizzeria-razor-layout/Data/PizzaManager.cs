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

        public static void InsertPizza(Pizza pizza, List<string> SelectedIngredients)
        {
            using PizzaContext db = new PizzaContext();
            pizza.Ingredients = new List<Ingredient>();
            if (SelectedIngredients != null)
            {
                foreach(var ingredientId in SelectedIngredients)
                {
                    int id = int.Parse(ingredientId);
                    var ingredient = db.Ingredients.FirstOrDefault(i => i.Id == id);
                    if(ingredient != null)
                        pizza.Ingredients.Add(ingredient);
                }
            }
            db.Pizzas.Add(pizza);
            db.SaveChanges();
        }

        public static bool UpdatePizza(int id, Pizza pizza, List<string> selectedIngredients)
        {
            try
            {
                using PizzaContext db = new PizzaContext();
                var pizzaToEdit = db.Pizzas.Where(p => p.Id == id).Include(p => p.Ingredients).FirstOrDefault();
                if (pizzaToEdit == null)
                    return false;
                pizzaToEdit.Name = pizza.Name;
                pizzaToEdit.Overview = pizza.Overview;
                pizzaToEdit.Price = pizza.Price;
                pizzaToEdit.CategoryId = pizza.CategoryId;
                pizzaToEdit.Ingredients.Clear();
                if (selectedIngredients != null)
                {
                    foreach (var ingredient in selectedIngredients)
                    {
                        int ingredientId = int.Parse(ingredient);
                        var ingredientFromDb = db.Ingredients.FirstOrDefault(x => x.Id == ingredientId);
                        if (ingredientFromDb != null)
                            pizzaToEdit.Ingredients.Add(ingredientFromDb);
                    }
                }

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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