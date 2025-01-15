using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ConcertTickets.Models.Validation;
using System.ComponentModel.DataAnnotations;

namespace ConcertTickets.Models.ViewModel
{
	public class OrderFormViewModel
	{
		public int TicketOfferId { get; set; }
		public string ConcertInfo { get; set; }
		public string TicketType { get; set; }
		public double Price { get; set; }
		public bool DiscountApplied { get; set; }

		[Required(ErrorMessage = "Aantal tickets is verplicht")]
		[Range(1, 5, ErrorMessage = "Minimum 1 en maximum 5 tickets")]
		public int NumTickets { get; set; }

		[Required(ErrorMessage = "Je moet akkoord gaan met de algemene voorwaarden")]
		[MustBeTrue(ErrorMessage = "Je moet akkoord gaan met de algemene voorwaarden")]
		public bool AcceptTerms { get; set; }

		[ValidateNever]
		public string UserId { get; set; }

		[ValidateNever]
		public string UserName { get; set; }
	}
}

namespace ConcertTickets.Models.Validation
{
	public class MustBeTrueAttribute : ValidationAttribute, IClientModelValidator
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is bool boolValue && boolValue)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult(ErrorMessage);
		}

		public void AddValidation(ClientModelValidationContext context)
		{
			context.Attributes.Add("data-val", "true");
			context.Attributes.Add("data-val-mustbetrue", ErrorMessage);
		}
	}
}