using LRMS.Application.Dto;
using LRMS.Application.Exceptions;
using LRMS.Application.Repositories;
using LRMS.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LRMS.Infrastructure.Persistence.Repositories;

public class BookReservationRepository(LrmsDbContext dbContext) : IBookReservationRepository
{
    private readonly LrmsDbContext _dbContext = dbContext;

    public async Task CreateBookReservation(BookReservationDto bookReservationDto, CancellationToken ct = default)
    {
        if (!await _dbContext.Books.AnyAsync(t => t.Id == bookReservationDto.BookId, ct))
            throw new EntityNotFoundException("Не найдена книга.");

        if (bookReservationDto.Date.Kind != DateTimeKind.Utc)
        {
            bookReservationDto.Date = DateTime.SpecifyKind(bookReservationDto.Date, DateTimeKind.Utc);
        }

        var bookReservationEntity = BookReservationMapper.ToEntity(bookReservationDto);
        bookReservationEntity.CreatedAt = DateTime.UtcNow;

        using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);

        var conflictingReservations = await _dbContext.BookReservations
            .FromSqlRaw("SELECT * FROM \"book_reservations\" WHERE \"book_id\" = {0} AND \"reservation_date\" = {1} FOR UPDATE",
                bookReservationDto.BookId, bookReservationDto.Date)
            .ToListAsync(ct);

        if (conflictingReservations.Count != 0)
        {
            await transaction.RollbackAsync(ct);
            throw new DomainException("Не удалось забронировать книгу. Данная дата уже занята.");
        }

        var reservations = await _dbContext.Books
            .FromSqlRaw("SELECT * FROM \"books\" WHERE \"id\" = {0} FOR UPDATE", bookReservationDto.BookId)
            .FirstOrDefaultAsync(ct);

        await _dbContext.BookReservations.AddAsync(bookReservationEntity, ct);
        await _dbContext.SaveChangesAsync(ct);

        await transaction.CommitAsync(ct);
    }
}
