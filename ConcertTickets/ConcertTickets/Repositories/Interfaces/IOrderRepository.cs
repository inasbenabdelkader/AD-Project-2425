using ConcertTickets.Models.Entities;

namespace ConcertTickets.Repositories.Interfaces
{
	public interface IOrderRepository
	{
		IEnumerable<Order> GetOrdersByStatus(bool paid);
		Order GetOrderById(int id);
		void AddOrder(Order order);
		void SaveChanges();
	}
}
