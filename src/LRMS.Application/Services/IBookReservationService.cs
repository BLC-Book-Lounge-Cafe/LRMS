using LRMS.Application.Dto;

namespace LRMS.Application.Services;

public interface IBookReservationService
{
    Task CreateBookReservation(BookReservationDto bookReservationDto, CancellationToken ct = default);
}
