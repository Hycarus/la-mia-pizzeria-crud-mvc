using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace la_mia_pizzeria_static.Models
{
	[Table("pizzas")]
	public class Pizza
	{
		[Key] public int Id { get; set; }
		public string? Name { get; set; }
		public string? Overview { get; set; }
		public string? Image { get; set; }
		public decimal Price { get; set; }
	}

	public class PizzaContext : DbContext
	{
		//public PizzaContext(DbContextOptions<PizzaContext> options) : base(options) { }
		public DbSet<Pizza>? Pizzas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost,1433;Database=pizzeria_db;User Id=sa;Password=dockerStrongPwd123;TrustServerCertificate=True");
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Pizza>().HasData(
                new Pizza { Id = 1, Name = "Margherita", Overview = "Passata di pomodoro, Mozzarella", Image = "https://www.finedininglovers.it/sites/g/files/xknfdk1106/files/fdl_content_import_it/margherita-50kalo.jpg", Price = 6.50m },
				new Pizza { Id = 2, Name = "4 Stagioni", Overview = "Passata di pomodoro, Mozzarella, Prosciutto cotto, Carcofini, Funghi, Pomodorini", Image = "https://www.petitchef.it/imgupl/recipe/pizza-4-stagioni--449891p695427.jpg", Price = 8.00m },
				new Pizza { Id = 3, Name = "Marinara", Overview = "Passata di pomodoro, Aglio, Olio", Image = "https://www.pizzarecipe.org/wp-content/uploads/2019/01/Pizza-Marinara.jpg", Price = 5.50m },
				new Pizza { Id = 4, Name = "Prosciutto e Funghi", Overview = "Passata di pomodoro, Mozzarella, Prosciutto cotto, Funghi", Image = "https://i1.wp.com/www.piccolericette.net/piccolericette/wp-content/uploads/2019/10/4102_Pizza.jpg?resize=895%2C616&ssl=1", Price = 7.50m }
				);
		}
    }
}

