using LRMS.Application.Dto;

namespace LRMS.Application.Repositories;

public interface ISpaceStateRepository
{
    Task<SpaceStateDto> GetSpaceStateAsync(CancellationToken ct = default);
}
