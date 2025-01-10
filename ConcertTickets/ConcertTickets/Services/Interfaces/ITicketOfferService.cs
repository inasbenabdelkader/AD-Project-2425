namespace ConcertTickets.Services.Interfaces
{
	public interface ITicketOfferService
	{
		OrderFormViewModel GetTicketOfferForOrder(int id, bool hasMemberCard);
		void UpdateTicketOffer(TicketOfferUpdateViewModel model);
	}
}
