using System.ComponentModel.DataAnnotations;

namespace ConcertTickets.Models.Validation
{
	public class MemberCardNumberValidation : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null) return ValidationResult.Success;

			string cardNumber = value.ToString();

			if (!cardNumber.StartsWith("ODI") ||
				cardNumber.Length != 13 ||
				!int.TryParse(cardNumber.Substring(3), out _))
			{
				return new ValidationResult("Lidkaartnummer moet starten met 'ODI' gevolgd door 10 cijfers");
			}

			return ValidationResult.Success;
		}
	}
}
