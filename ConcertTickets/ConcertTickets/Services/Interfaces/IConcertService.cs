namespace ConcertTickets.Services.Interfaces
{
	public interface IConcertService
	{
		IEnumerable<ConcertViewModel> GetAllConcerts();
		ConcertViewModel GetConcertById(int id);
	}
}
