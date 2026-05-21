using LRMS.Infrastructure.ReservationManagerApi.Books.Dto;
using Refit;

namespace LRMS.Infrastructure.ReservationManagerApi.Books;

public interface IBookApi
{
    [Post("/api/v1/books")]
    Task<HttpResponseMessage> CreateBookReservation([Body] CreateBookReservationDto dto, CancellationToken ct = default);

    [Get("/api/v1/books")]
    Task<HttpResponseMessage> GetBookReservations([Refit.Query] long? book_id, [Refit.Query] string? reserved_at,
        [Refit.Query] int? page_number, [Refit.Query] int? page_size, CancellationToken ct = default);

    [Delete("/api/v1/books/{id}")]
    Task<HttpResponseMessage> DeleteBookReservation(long id, CancellationToken ct = default);
}
