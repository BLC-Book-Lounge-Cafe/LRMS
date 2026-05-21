using LRMS.Application.Exceptions;
using LRMS.Infrastructure.Mappers;
using LRMS.Infrastructure.Persistence;
using LRMS.Infrastructure.ReservationManagerApi.ApiWrapper;
using LRMS.Infrastructure.ReservationManagerApi.Books.Dto;
using LRMS.Infrastructure.ReservationManagerApi.Tables.Dto;
using Microsoft.EntityFrameworkCore;

namespace LRMS.Infrastructure.ReservationManagerApi.Books;

public class BookReservationRepository(LrmsDbContext dbContext, IBookApi bookApi) : IBookReservationRepository
{
    private readonly LrmsDbContext _dbContext = dbContext;
    private readonly IBookApi _bookApi = bookApi;

    public async Task CreateBookReservation(CreateBookReservationCommand bookReservationDto, CancellationToken ct = default)
    {
        if (!await _dbContext.Books.AnyAsync(t => t.Id == bookReservationDto.BookId, ct))
            throw new EntityNotFoundException("Не найдена книга.");

        var date = bookReservationDto.Date.ToString("yyyy-MM-dd");

        _ = await RestApiWrapper.CallApi<TableReservationsResponse>(
           _bookApi.CreateBookReservation(BookReservationMapper.ToDto(bookReservationDto), ct), ct);
    }

    public async Task DeleteBookReservation(long id, CancellationToken ct = default)
    {
        await RestApiWrapper.CallApi(_bookApi.DeleteBookReservation(id, ct), ct);
    }

    public async Task<GetBookReservationsResponse> GetBookReservations(
        long? bookId,
        DateTime? date,
        int? pageNumber,
        int? pageSize,
        CancellationToken ct = default)
    {
        if (bookId is not null && !await _dbContext.Books.AnyAsync(t => t.Id == bookId, ct))
            throw new EntityNotFoundException("Не найдена книга.");

        var response = await RestApiWrapper.CallApi<BookReservationsResponse>(
            _bookApi.GetBookReservations(bookId, date?.Date.ToString(), pageNumber, pageSize, ct), ct);
        return BookReservationMapper.ToResponse(response);
    }
}
