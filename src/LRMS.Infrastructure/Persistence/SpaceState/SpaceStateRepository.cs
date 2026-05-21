using LRMS.Application.SpaceState;
using LRMS.Application.SpaceState.Dto;
using LRMS.Infrastructure.ReservationManagerApi.ApiWrapper;
using LRMS.Infrastructure.ReservationManagerApi.Tables;
using LRMS.Infrastructure.ReservationManagerApi.Tables.Dto;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace LRMS.Infrastructure.Persistence.SpaceState;

public class SpaceStateRepository(LrmsDbContext dbContext, ITableApi tableApi) : ISpaceStateRepository
{
    private readonly LrmsDbContext _dbContext = dbContext;
    private readonly ITableApi _tableApi = tableApi;

    public async Task<SpaceStateDto> GetSpaceStateAsync(CancellationToken ct = default)
    {
        var spaceState = await _dbContext.SpaceStates.FirstAsync(ct);
        var spaceStateDto = SpaceStateMapper.ToDto(spaceState);
        spaceStateDto.WorkloadLevel = await GetWorkloadLevel(ct);
        spaceStateDto.CurrentTrack = JsonSerializer.Deserialize<CurrentTrackDto>(spaceState.CurrentTrack);
        return spaceStateDto;
    }

    public async Task UpdateSpaceStateAsync(byte noiseLevel, string? description, CancellationToken ct = default)
    {
        var spaceState = await _dbContext.SpaceStates.FirstAsync(ct);
        spaceState.NoiseLevel = noiseLevel;
        if (!string.IsNullOrEmpty(description))
            spaceState.Description = description;
        spaceState.UpdatedAt = DateTime.UtcNow;
        _dbContext.SpaceStates.Update(spaceState);
        await _dbContext.SaveChangesAsync(ct);
    }

    private async Task<byte> GetWorkloadLevel(CancellationToken ct)
    {
        var allTablesCount = await _dbContext.Tables.CountAsync(ct);
        var now = DateTime.UtcNow;

        var tableReservations = await RestApiWrapper.CallApi<TableReservationsResponse>(
            _tableApi.GetTableReservations(null, DateTime.Now.ToString(), null, null, ct), ct);

        var count = tableReservations.reservations
            .GroupBy(t => t.table_id)
            .Select(t => t.Key)
            .Count();

        return (byte)(count * 100 / allTablesCount);
    }
}
