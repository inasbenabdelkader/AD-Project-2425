namespace ConcertTickets.Services.Interfaces
{
	public interface IOrderService
	{
		int CreateOrder(OrderFormViewModel model);
		IEnumerable<OrderViewModel> GetOrdersByStatus(bool paid);
		OrderViewModel GetOrderById(int orderId);
		void UpdatePaid(int orderId, bool paid);
	}
}
