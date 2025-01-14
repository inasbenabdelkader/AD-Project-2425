using ConcertTickets.Models.ViewModel;

namespace ConcertTickets.Services.Interfaces
{
	public interface IOrderService
	{
		int CreateOrder(OrderFormViewModel model);
		OrderViewModel GetOrderById(int orderId);
		IEnumerable<OrderViewModel> GetOrdersByStatus(bool paid);
		void UpdatePaid(int orderId, bool paid);
	}
}