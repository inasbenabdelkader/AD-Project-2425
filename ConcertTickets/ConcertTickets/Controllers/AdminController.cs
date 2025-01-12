using ConcertTickets.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConcertTickets.Controllers
{
	[Authorize(Policy = "AdminOnly")]
	public class AdminController : Controller
	{
		private readonly IOrderService _orderService;

		public AdminController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		public IActionResult Orders()
		{
			var orders = _orderService.GetOrdersByStatus(false);
			return View(orders);
		}

		public IActionResult SetPaid(int id)
		{
			_orderService.UpdatePaid(id, true);
			return RedirectToAction(nameof(Orders));
		}
	}
}
