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

		[Required(ErrorMessage = "Il nome della pizza è obbligatorio")]
		[StringLength(50, ErrorMessage = "Il nome della pizza non può superare i 50 caratteri")]
		public string? Name { get; set; }

		[Required(ErrorMessage = "La descrizione della pizza è obbligatoria")]
		[StringLength(200, ErrorMessage = "La descrizione della pizza non può superare i 200 caratteri")]
		[MinWords(5, ErrorMessage = "La descrizione della pizza deve contenere almeno 5 parole")]
		public string? Overview { get; set; }

		[Required(ErrorMessage = "L'URL dell'immagine è obbligatorio")]
		[Url(ErrorMessage = "L'URL dell'immagine non è valido")]
		public string? Image { get; set; }

		[Required(ErrorMessage = "Il prezzo della pizza è obbligatorio")]
		[Range(0.01, double.MaxValue, ErrorMessage = "Il prezzo della pizza deve essere maggiore di zero")]
		public decimal? Price { get; set; }
	}

	public class  MinWordsAttribute : ValidationAttribute
	{
		private readonly int _minWords;
		public MinWordsAttribute(int minWords)
		{
			this._minWords = minWords;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if(value != null)
			{
				var valueAsString = value.ToString();
				if(valueAsString.Split(' ').Where(s => s.Length > 0).Count() < _minWords)
				{
					return new ValidationResult(ErrorMessage);
				}
			}

			if(value == " ")
			{
				return new ValidationResult(ErrorMessage);
			}
			return ValidationResult.Success;
		}
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

