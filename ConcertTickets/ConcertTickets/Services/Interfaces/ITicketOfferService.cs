using ConcertTickets.Models.ViewModel;

namespace ConcertTickets.Services.Interfaces
{
	public interface ITicketOfferService
	{
		OrderFormViewModel GetTicketOfferForOrder(int id, bool hasMemberCard);
		OrderFormViewModel GetTicketOfferForOrder(int ticketOfferId, object hasMemberCard);
		void UpdateTicketOffer(TicketOfferUpdateViewModel model);
	}
}