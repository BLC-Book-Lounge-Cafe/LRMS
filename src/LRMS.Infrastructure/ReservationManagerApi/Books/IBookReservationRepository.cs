using LRMS.Infrastructure.ReservationManagerApi.Books.Dto;

namespace LRMS.Infrastructure.ReservationManagerApi.Books;

public interface IBookReservationRepository
{
    Task CreateBookReservation(CreateBookReservationCommand bookReservationDto, CancellationToken ct = default);
    Task<GetBookReservationsResponse> GetBookReservations(int? bookId, DateTime? date, int? pageNumber, int? pageSize,
        CancellationToken ct = default);
    Task DeleteBookReservation(int id, CancellationToken ct = default);
}
