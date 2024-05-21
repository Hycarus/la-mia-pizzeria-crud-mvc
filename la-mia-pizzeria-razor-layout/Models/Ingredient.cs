using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
	[Table("ingredients")]
	public class Ingredient
	{
		[Key] public int Id { get; set; }
		public string Name { get; set; }

		public List<Pizza> Pizzas { get; set; }

		public Ingredient()
		{
		}
	}
}

