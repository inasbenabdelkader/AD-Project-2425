using ConcertTickets.Models.Identity;
using ConcertTickets.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConcertTickets.Controllers
{
	public class ConcertController : Controller
	{
		private readonly IConcertService _concertService;
		private readonly ITicketOfferService _ticketOfferService;
		private readonly UserManager<CustomUser> _userManager;

		public ConcertController(
			IConcertService concertService,
			ITicketOfferService ticketOfferService,
			UserManager<CustomUser> userManager)
		{
			_concertService = concertService;
			_ticketOfferService = ticketOfferService;
			_userManager = userManager;
		}

		public IActionResult Index()
		{
			var concerts = _concertService.GetAllConcerts();
			return View(concerts);
		}

		public IActionResult Buy(int id)
		{
			var concert = _concertService.GetConcertById(id);
			return View(concert);
		}
	}
}
