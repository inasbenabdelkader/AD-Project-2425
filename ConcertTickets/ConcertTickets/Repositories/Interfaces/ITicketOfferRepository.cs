using ConcertTickets.Models.Entities;

namespace ConcertTickets.Repositories.Interfaces
{
	public interface ITicketOfferRepository
	{
		TicketOffer GetTicketOfferById(int id);
	}
}
