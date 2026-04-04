using LRMS.Application.Dto;
using LRMS.Application.Exceptions;
using LRMS.Application.Repositories;
using LRMS.Infrastructure.Mappers;
using LRMS.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace LRMS.Infrastructure.Persistence.Repositories;

public class ReservationRequestRepository(LrmsDbContext dbContext) : IReservationRequestRepository
{
    private readonly LrmsDbContext _dbContext = dbContext;

    public async Task CreateReservationRequest(string customerName, string CustomerPhone, CancellationToken ct = default)
    {
        await _dbContext.AddAsync(new ReservationRequestEntity
        {
            CustomerName = customerName,
            CustomerPhone = CustomerPhone,
            CreatedAt = DateTime.UtcNow,
        }, ct);

        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteReservationRequest(int id, CancellationToken ct = default)
    {
        var reservationRequest = await _dbContext.ReservationRequests.FindAsync([id], ct)
            ?? throw new EntityNotFoundException("Не найден запрос на бронирование стола.");
        _dbContext.ReservationRequests.Remove(reservationRequest);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyCollection<ReservationRequestDto>> GetReservationRequests(CancellationToken ct = default)
    {
        var result = await _dbContext.ReservationRequests.OrderBy(r => r.CreatedAt).ToListAsync(ct);
        return [.. result.Select(ReservationRequestMapper.ToDto)];
    }
}
