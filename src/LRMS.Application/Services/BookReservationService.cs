using LRMS.Application.Dto;
using LRMS.Application.Repositories;

namespace LRMS.Application.Services;

public class BookReservationService(IBookReservationRepository repository) : IBookReservationService
{
    private readonly IBookReservationRepository _repository = repository;

    public async Task CreateBookReservation(BookReservationDto bookReservationDto, CancellationToken ct = default)
    {
        await _repository.CreateBookReservation(bookReservationDto, ct);
    }
}
