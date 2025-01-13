using ConcertTickets.Models.ViewModel;
using ConcertTickets.Repositories.Interfaces;
using ConcertTickets.Services.Interfaces;

namespace ConcertTickets.Services
{
	public class ConcertService : IConcertService
	{
		private readonly IConcertRepository _concertRepository;

		public ConcertService(IConcertRepository concertRepository)
		{
			_concertRepository = concertRepository;
		}

		public IEnumerable<ConcertViewModel> GetAllConcerts()
		{
			var concerts = _concertRepository.GetConcertsWithTicketOffers();
			return concerts.Select(c => new ConcertViewModel
			{
				Id = c.Id,
				Artist = c.Artist,
				Location = c.Location,
				Date = c.Date,
				TicketOffers = c.TicketOffers
			});
		}

		public ConcertViewModel GetConcertById(int id)
		{
			var concert = _concertRepository.GetConcertWithTicketOffers(id);
			return new ConcertViewModel
			{
				Id = concert.Id,
				Artist = concert.Artist,
				Location = concert.Location,
				Date = concert.Date,
				TicketOffers = concert.TicketOffers
			};
		}
	}
}
