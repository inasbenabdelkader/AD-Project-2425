using Microsoft.AspNetCore.Identity;

namespace ConcertTickets.Models.Identity
{
	public class CustomUser : IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MemberCardNumber { get; set; }
	}
}
