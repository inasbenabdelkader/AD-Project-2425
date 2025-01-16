using ConcertTickets.Models.Entities;

namespace ConcertTickets.Models.ViewModel
{
	public class ConcertViewModel
	{
		public int Id { get; set; }
		public string Artist { get; set; }
		public string Location { get; set; }
		public DateTime Date { get; set; }
		public string ArtistPicture => $"/img/{Artist.Replace(" ", "")}.png";
		public int TotalAvailableTickets => TicketOffers?.Sum(t => t.NumTickets) ?? 0;
		public IEnumerable<TicketOffer> TicketOffers { get; set; }
	}
}
