using LRMS.Application.Exceptions;
using LRMS.Infrastructure.Mappers;
using LRMS.Infrastructure.Persistence;
using LRMS.Infrastructure.ReservationManagerApi.ApiWrapper;
using LRMS.Infrastructure.ReservationManagerApi.Tables.Commands;
using LRMS.Infrastructure.ReservationManagerApi.Tables.Dto;
using LRMS.Infrastructure.ReservationManagerApi.Tables.Requests;
using Microsoft.EntityFrameworkCore;

namespace LRMS.Infrastructure.ReservationManagerApi.Tables;

public class TableReservationRepository(LrmsDbContext dbContext, ITableApi tableApi) : ITableReservationRepository
{
    private readonly LrmsDbContext _dbContext = dbContext;
    private readonly ITableApi _tableApi = tableApi;

    public async Task<IReadOnlyCollection<ReservationSlotDto>> GetSlots(long tableId, DateTime date, CancellationToken ct = default)
    {
        await CheckTableExists(tableId, ct);

        string formattedDate = date.ToString("yyyy-MM-dd");

        var slots = await RestApiWrapper.CallApi<TableSlotsResponse>(
            _tableApi.GetTableReservationSlots(tableId, formattedDate, ct), ct);

        return [.. slots.slots.Select(TableReservationMapper.ToDto)];
    }

    public async Task CreateTableReservation(CreateTableReservationCommand tableReservationDto, CancellationToken ct = default)
    {
        await CheckTableExists(tableReservationDto.TableId, ct);

        var dto = TableReservationMapper.ToDto(tableReservationDto);
        _ = await RestApiWrapper.CallApi<TableReservationModel>(
            _tableApi.CreateTableReservation(dto, ct), ct);
    }

    private async Task CheckTableExists(long id, CancellationToken ct)
    {
        if (!await _dbContext.Tables.AnyAsync(t => t.Id == id, ct))
            throw new EntityNotFoundException("Не найден стол.");
    }

    public async Task<GetTableReservationsResponse> GetTableReservations(
        long? tableId,
        string? activeAt,
        int? pageNumber,
        int? pageSize,
        CancellationToken ct = default)
    {
        if (tableId is not null)
            await CheckTableExists(tableId.Value, ct);

        var response = await RestApiWrapper.CallApi<TableReservationsResponse>(
            _tableApi.GetTableReservations(tableId, activeAt, pageNumber, pageSize, ct), ct);

        return TableReservationMapper.ToResponse(response);
    }

    public async Task DeleteTableReservation(long id, CancellationToken ct = default)
    {
        await RestApiWrapper.CallApi(_tableApi.DeleteTableReservation(id, ct), ct);
    }
}
