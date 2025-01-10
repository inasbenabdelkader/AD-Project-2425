namespace ConcertTickets.Models.Entities
{
	public class Concert : BaseEntity
	{
		public string Artist { get; set; }
		public string Location { get; set; }
		public DateTime Date { get; set; }
		public ICollection<TicketOffer> TicketOffers { get; set; }
	}
}
