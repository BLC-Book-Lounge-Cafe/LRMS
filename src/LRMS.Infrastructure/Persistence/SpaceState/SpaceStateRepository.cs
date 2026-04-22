using LRMS.Application.SpaceState;
using LRMS.Application.SpaceState.Dto;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace LRMS.Infrastructure.Persistence.SpaceState;

public class SpaceStateRepository(LrmsDbContext dbContext) : ISpaceStateRepository
{
    private readonly LrmsDbContext _dbContext = dbContext;

    public async Task<SpaceStateDto> GetSpaceStateAsync(CancellationToken ct = default)
    {
        var spaceState = await _dbContext.SpaceStates.FirstAsync(ct);
        var spaceStateDto = SpaceStateMapper.ToDto(spaceState);
        spaceStateDto.WorkloadLevel = await GetWorkloadLevel(ct);
        spaceStateDto.CurrentTrack = JsonSerializer.Deserialize<CurrentTrackDto>(spaceState.CurrentTrack);
        return spaceStateDto;
    }

    public async Task UpdateSpaceStateAsync(byte noiseLevel, string description, CancellationToken ct = default)
    {
        var spaceState = await _dbContext.SpaceStates.FirstAsync(ct);
        spaceState.NoiseLevel = noiseLevel;
        spaceState.Description = description;
        spaceState.UpdatedAt = DateTime.UtcNow;
        _dbContext.SpaceStates.Update(spaceState);
        await _dbContext.SaveChangesAsync(ct);
    }

    private async Task<byte> GetWorkloadLevel(CancellationToken ct)
    {
        var allTablesCount = await _dbContext.Tables.CountAsync(ct);
        var now = DateTime.UtcNow;
        var tableReservationsCount = await _dbContext.TableReservations
            .Where(t => t.ReservationStartAt <= now && t.ReservationEndAt >= now)
            .GroupBy(t => t.TableId)
            .Select(t => t.Key)
            .CountAsync(ct);

        return (byte)(tableReservationsCount * 100 / allTablesCount);
    }
}
