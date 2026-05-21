using LRMS.Infrastructure.ReservationManagerApi.Books.Dto;

namespace LRMS.Infrastructure.ReservationManagerApi.Books;

public interface IBookReservationRepository
{
    Task CreateBookReservation(CreateBookReservationCommand bookReservationDto, CancellationToken ct = default);
    Task<GetBookReservationsResponse> GetBookReservations(long? bookId, DateTime? date, int? pageNumber, int? pageSize,
        CancellationToken ct = default);
    Task DeleteBookReservation(long id, CancellationToken ct = default);
}
