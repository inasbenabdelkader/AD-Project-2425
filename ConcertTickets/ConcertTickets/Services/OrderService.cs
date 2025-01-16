using ConcertTickets.Models.Entities;
using ConcertTickets.Models.ViewModel;
using ConcertTickets.Repositories.Interfaces;
using ConcertTickets.Services.Interfaces;

namespace ConcertTickets.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _orderRepository;
		private readonly ITicketOfferRepository _ticketOfferRepository;

		public OrderService(IOrderRepository orderRepository, ITicketOfferRepository ticketOfferRepository)
		{
			_orderRepository = orderRepository;
			_ticketOfferRepository = ticketOfferRepository;
		}

		public int CreateOrder(OrderFormViewModel model)
		{
			var ticketOffer = _ticketOfferRepository.GetTicketOfferById(model.TicketOfferId);

			var order = new Order
			{
				UserId = model.UserId,
				UserName = model.UserName,
				NumTickets = model.NumTickets,
				TotalPrice = model.Price * model.NumTickets,
				Paid = false,
				DiscountApplied = model.DiscountApplied,
				TicketOfferId = model.TicketOfferId,
			};

			_orderRepository.AddOrder(order); 
			_orderRepository.SaveChanges();

			return order.Id;
		}

		public IEnumerable<OrderViewModel> GetOrdersByStatus(bool paid)
		{
			var orders = _orderRepository.GetOrdersByStatus(paid);
			return orders.Select(o => new OrderViewModel
			{
				Id = o.Id,
				UserName = o.UserName,
				ConcertInfo = $"{o.TicketOffer.Concert.Artist} - {o.TicketOffer.Concert.Date:dd/MM/yyyy}",
				TicketType = o.TicketOffer.TicketType,
				NumTickets = o.NumTickets,
				TotalPrice = o.TotalPrice,
				Paid = o.Paid,
				DiscountApplied = o.DiscountApplied
			});
		}

		public OrderViewModel GetOrderById(int orderId)
		{
			var order = _orderRepository.GetOrderById(orderId);
			return new OrderViewModel
			{
				Id = order.Id,
				UserName = order.UserName,
				ConcertInfo = $"{order.TicketOffer.Concert.Artist} - {order.TicketOffer.Concert.Date:dd/MM/yyyy}",
				TicketType = order.TicketOffer.TicketType,
				NumTickets = order.NumTickets,
				TotalPrice = order.TotalPrice,
				Paid = order.Paid,
				DiscountApplied = order.DiscountApplied
			};
		}

		public void UpdatePaid(int orderId, bool paid)
		{
			var order = _orderRepository.GetOrderById(orderId);
			order.Paid = paid;
			_orderRepository.SaveChanges();
		}
	}
}