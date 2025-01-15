using ConcertTickets.Data;
using ConcertTickets.Models.Entities;
using ConcertTickets.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace ConcertTickets.Repositories
{
	public class TicketOfferRepository : ITicketOfferRepository
	{
		private readonly ApplicationDbContext _context;

		public TicketOfferRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public TicketOffer GetTicketOfferById(int id)
		{
			return _context.TicketOffers
				.Include(t => t.Concert)
				.FirstOrDefault(t => t.Id == id);
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}
	}
}
