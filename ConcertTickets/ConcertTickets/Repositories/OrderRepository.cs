using ConcertTickets.Data;
using ConcertTickets.Models.Entities;
using ConcertTickets.Repositories.Interfaces;

namespace ConcertTickets.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly ApplicationDbContext _context;

		public OrderRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public IEnumerable<Order> GetOrdersByStatus(bool paid)
		{
			return _context.Orders
				.Include(o => o.TicketOffer)
				.ThenInclude(t => t.Concert)
				.Where(o => o.Paid == paid)
				.ToList();
		}

		public Order GetOrderById(int id)
		{
			return _context.Orders
				.Include(o => o.TicketOffer)
				.ThenInclude(t => t.Concert)
				.FirstOrDefault(o => o.Id == id);
		}
	}
}
