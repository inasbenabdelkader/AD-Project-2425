using ConcertTickets.Models.Entities;

namespace ConcertTickets.Repositories.Interfaces
{
	public interface IConcertRepository
	{
		IEnumerable<Concert> GetConcertsWithTicketOffers();
		Concert GetConcertWithTicketOffers(int id);
	}
}
