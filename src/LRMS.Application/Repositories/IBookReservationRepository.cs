namespace LRMS.Application.Repositories;

public interface IBookReservationRepository
{
    Task<IReadOnlyCollection<DateOnly>> GetAvailableDates(int bookId, CancellationToken ct = default);
}
