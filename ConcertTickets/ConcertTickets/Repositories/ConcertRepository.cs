using ConcertTickets.Data;
using ConcertTickets.Models.Entities;
using ConcertTickets.Repositories.Interfaces;

namespace ConcertTickets.Repositories
{
	public class ConcertRepository : IConcertRepository
	{
		private readonly ApplicationDbContext _context;

		public ConcertRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Concert> GetConcertsWithTicketOffers()
		{
			return _context.Concerts
				.Include(c => c.TicketOffers)
				.OrderBy(c => c.Date)
				.ToList();
		}

		public Concert GetConcertWithTicketOffers(int id)
		{
			return _context.Concerts
				.Include(c => c.TicketOffers)
				.FirstOrDefault(c => c.Id == id);
		}
	}
}
