namespace ConcertTickets.Models.ViewModel
{
	public class OrderViewModel
	{
		public int Id { get; set; }
		public string UserName { get; set; }
		public string ConcertInfo { get; set; }
		public string TicketType { get; set; }
		public int NumTickets { get; set; }
		public double TotalPrice { get; set; }
		public bool Paid { get; set; }
		public bool DiscountApplied { get; set; }
	}
}
