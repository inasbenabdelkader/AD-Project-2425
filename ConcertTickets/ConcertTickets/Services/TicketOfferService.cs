using ConcertTickets.Models.ViewModel;
using ConcertTickets.Repositories.Interfaces;
using ConcertTickets.Services.Interfaces;

namespace ConcertTickets.Services
{
	public class TicketOfferService : ITicketOfferService
	{
		private readonly ITicketOfferRepository _ticketOfferRepository;

		public TicketOfferService(ITicketOfferRepository ticketOfferRepository)
		{
			_ticketOfferRepository = ticketOfferRepository;
		}

		public OrderFormViewModel GetTicketOfferForOrder(int id, bool hasMemberCard)
		{
			var ticketOffer = _ticketOfferRepository.GetTicketOfferById(id);
			var price = hasMemberCard ? ticketOffer.Price * 0.9 : ticketOffer.Price;

			return new OrderFormViewModel
			{
				TicketOfferId = ticketOffer.Id,
				ConcertInfo = $"{ticketOffer.Concert.Artist} - {ticketOffer.Concert.Date:dd/MM/yyyy}",
				TicketType = ticketOffer.TicketType,
				Price = price,
				DiscountApplied = hasMemberCard
			};
		}

		public void UpdateTicketOffer(TicketOfferUpdateViewModel model)
		{
			var ticketOffer = _ticketOfferRepository.GetTicketOfferById(model.Id);
			ticketOffer.NumTickets -= model.NumTicketsOrdered;
			_ticketOfferRepository.SaveChanges();
		}
	}

}
