using Microsoft.AspNetCore.Mvc;

namespace ConcertTickets.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
