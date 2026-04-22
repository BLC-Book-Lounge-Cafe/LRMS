using LRMS.Application.Exceptions;
using LRMS.Application.TableReservations;
using LRMS.Application.TableReservations.Dto;
using Microsoft.EntityFrameworkCore;

namespace LRMS.Infrastructure.Persistence.TableReservations;

public class TableReservationRepository(LrmsDbContext dbContext) : ITableReservationRepository
{
    private readonly LrmsDbContext _dbContext = dbContext;

    public async Task<IReadOnlyCollection<ReservationSlotDto>> GetSlots(int tableId, DateTime date, CancellationToken ct = default)
    {
        var spaceSettings = await _dbContext.SpaceSettings.FirstAsync(ct);

        if (!await _dbContext.Tables.AnyAsync(t => t.Id == tableId, ct))
            throw new EntityNotFoundException("Не найден стол.");

        if (date.Kind != DateTimeKind.Utc)
        {
            date = DateTime.SpecifyKind(date, DateTimeKind.Utc);
        }

        var existingReservations = await _dbContext.TableReservations
            .Where(t => t.TableId == tableId && t.ReservationStartAt.Date == date)
            .OrderBy(t => t.ReservationStartAt)
            .ToListAsync(ct);

        var allSlots = GenerateTimeSlots(spaceSettings.StartTime, spaceSettings.EndTime, spaceSettings.MinTableReservationTime);

        foreach (var slot in allSlots)
        {
            if (HasConflict(slot, existingReservations))
                slot.IsReserved = true;

            slot.StartTime = DateTime.SpecifyKind(slot.StartTime, DateTimeKind.Local);
            slot.EndTime = DateTime.SpecifyKind(slot.EndTime, DateTimeKind.Local);
        }

        return allSlots;
    }

    private static List<ReservationSlotDto> GenerateTimeSlots(DateTime dayStart, DateTime dayEnd, byte durationMinutes)
    {
        var slots = new List<ReservationSlotDto>();
        var current = dayStart;

        while (current.AddMinutes(durationMinutes) <= dayEnd)
        {
            var endTime = current.AddMinutes(durationMinutes);
            slots.Add(new ReservationSlotDto
            {
                StartTime = current,
                EndTime = endTime,
                IsReserved = false,
            });
            current = endTime;
        }

        return slots;
    }

    private static bool HasConflict(ReservationSlotDto slot, IEnumerable<TableReservationEntity> existingBookings)
    {
        foreach (var booking in existingBookings)
        {
            if (slot.StartTime < booking.ReservationEndAt && slot.EndTime > booking.ReservationStartAt)
                return true;
        }

        return false;
    }

    public async Task CreateTableReservation(TableReservationDto tableReservationDto, CancellationToken ct = default)
    {
        if (!await _dbContext.Tables.AnyAsync(t => t.Id == tableReservationDto.TableId, ct))
            throw new EntityNotFoundException("Не найден стол.");

        if (tableReservationDto.StartTime.Kind != DateTimeKind.Utc)
        {
            tableReservationDto.StartTime = DateTime.SpecifyKind(tableReservationDto.StartTime, DateTimeKind.Utc);
        }

        if (tableReservationDto.EndTime.Kind != DateTimeKind.Utc)
        {
            tableReservationDto.EndTime = DateTime.SpecifyKind(tableReservationDto.EndTime, DateTimeKind.Utc);
        }

        var entity = new TableReservationEntity
        {
            TableId = tableReservationDto.TableId,
            CustomerName = tableReservationDto.CustomerName,
            CustomerPhone = tableReservationDto.CustomerPhone,
            ReservationStartAt = tableReservationDto.StartTime,
            ReservationEndAt = tableReservationDto.EndTime,
            CreatedAt = DateTime.UtcNow
        };

        using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);

        var conflictingReservations = await _dbContext.TableReservations
            .FromSqlRaw("SELECT * FROM \"table_reservations\" WHERE \"table_id\" = {0} AND \"reservation_start_at\" < {1} AND \"reservation_end_at\" > {2} FOR UPDATE",
                tableReservationDto.TableId, tableReservationDto.EndTime, tableReservationDto.StartTime)
            .ToListAsync(ct);

        if (conflictingReservations.Count != 0)
        {
            await transaction.RollbackAsync(ct);
            throw new DomainException("Не удалось забронировать стол. Данное время уже занято.");
        }

        var reservations = await _dbContext.Tables
            .FromSqlRaw("SELECT * FROM \"tables\" WHERE \"id\" = {0} FOR UPDATE", tableReservationDto.TableId)
            .FirstOrDefaultAsync(ct);

        await _dbContext.TableReservations.AddAsync(entity, ct);
        await _dbContext.SaveChangesAsync(ct);

        await transaction.CommitAsync(ct);
    }
}
