namespace ConcertTickets.Models.Entities
{
	public class Order : BaseEntity
	{
		public string UserId { get; set; }
		public string UserName { get; set; }
		public int NumTickets { get; set; }
		public double TotalPrice { get; set; }
		public bool Paid { get; set; }
		public bool DiscountApplied { get; set; }
		public int TicketOfferId { get; set; }
		public TicketOffer TicketOffer { get; set; }
	}
}
