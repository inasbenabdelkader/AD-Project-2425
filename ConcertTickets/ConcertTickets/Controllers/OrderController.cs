using ConcertTickets.Models.Identity;
using ConcertTickets.Models.ViewModel;
using ConcertTickets.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConcertTickets.Controllers
{
	public class OrderController : Controller
	{
		private readonly IOrderService _orderService;
		private readonly ITicketOfferService _ticketOfferService;
		private readonly UserManager<CustomUser> _userManager;

		public OrderController(
			IOrderService orderService,
			ITicketOfferService ticketOfferService,
			UserManager<CustomUser> userManager)
		{
			_orderService = orderService;
			_ticketOfferService = ticketOfferService;
			_userManager = userManager;
		}

		[Authorize]
		public async Task<IActionResult> Create(int id)
		{
			var user = await _userManager.GetUserAsync(User);
			var hasMemberCard = !string.IsNullOrEmpty(user.MemberCardNumber); // Check if user has a member card
			var model = _ticketOfferService.GetTicketOfferForOrder(id, hasMemberCard);
			model.UserId = user.Id;
			model.UserName = $"{user.FirstName} {user.LastName}";
			return View(model);
		}

		[HttpPost]
		[Authorize]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(OrderFormViewModel model)
		{
			if (!ModelState.IsValid)
			{
				var user = await _userManager.GetUserAsync(User);
				var hasMemberCard = !string.IsNullOrEmpty(user.MemberCardNumber); // Check if user has a member card
				model = _ticketOfferService.GetTicketOfferForOrder(model.TicketOfferId, hasMemberCard);
				model.UserId = user.Id;
				model.UserName = $"{user.FirstName} {user.LastName}";
				return View(model);
			}

			var orderId = _orderService.CreateOrder(model);

			var updateModel = new TicketOfferUpdateViewModel
			{
				Id = model.TicketOfferId,
				NumTicketsOrdered = model.NumTickets
			};
			_ticketOfferService.UpdateTicketOffer(updateModel);

			return RedirectToAction(nameof(Success), new { id = orderId });
		}

		public IActionResult Success(int id)
		{
			var order = _orderService.GetOrderById(id);
			return View(order);
		}
	}
}
