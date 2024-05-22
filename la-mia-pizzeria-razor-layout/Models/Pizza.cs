using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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

		public int? CategoryId { get; set; }
		public Category? Category { get; set; }

		public List<Ingredient>? Ingredients { get; set; }
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
}

