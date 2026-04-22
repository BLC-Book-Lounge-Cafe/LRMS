using LRMS.Application.BookReservations.Dto;

namespace LRMS.Application.BookReservations;

public interface IBookReservationRepository
{
    Task CreateBookReservation(BookReservationDto bookReservationDto, CancellationToken ct = default);
}
