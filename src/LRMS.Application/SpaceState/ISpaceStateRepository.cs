using LRMS.Application.SpaceState.Dto;

namespace LRMS.Application.SpaceState;

public interface ISpaceStateRepository
{
    Task<SpaceStateDto> GetSpaceStateAsync(CancellationToken ct = default);
    Task UpdateSpaceStateAsync(byte noiseLevel, string description, CancellationToken ct = default);
}
