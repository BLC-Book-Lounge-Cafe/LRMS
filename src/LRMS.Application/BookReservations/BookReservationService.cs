using LRMS.Application.BookReservations.Dto;

namespace LRMS.Application.BookReservations;

public class BookReservationService(IBookReservationRepository repository) : IBookReservationService
{
    private readonly IBookReservationRepository _repository = repository;

    public async Task CreateBookReservation(BookReservationDto bookReservationDto, CancellationToken ct = default)
    {
        await _repository.CreateBookReservation(bookReservationDto, ct);
    }
}
