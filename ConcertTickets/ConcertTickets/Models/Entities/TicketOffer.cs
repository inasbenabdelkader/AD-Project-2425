namespace ConcertTickets.Models.Entities
{
	public class TicketOffer : BaseEntity
	{
		public string TicketType { get; set; }
		public int NumTickets { get; set; }
		public double Price { get; set; }
		public int ConcertId { get; set; }
		public Concert Concert { get; set; }
	}
}
