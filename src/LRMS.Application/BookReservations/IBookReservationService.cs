using LRMS.Application.BookReservations.Dto;

namespace LRMS.Application.BookReservations;

public interface IBookReservationService
{
    Task CreateBookReservation(BookReservationDto bookReservationDto, CancellationToken ct = default);
}
