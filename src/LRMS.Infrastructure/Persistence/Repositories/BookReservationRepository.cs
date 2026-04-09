using LRMS.Application.Repositories;

namespace LRMS.Infrastructure.Persistence.Repositories;

public class BookReservationRepository : IBookReservationRepository
{
    public Task<IReadOnlyCollection<DateOnly>> GetAvailableDates(int bookId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
