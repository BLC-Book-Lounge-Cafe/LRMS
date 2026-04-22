using LRMS.Application.SpaceState.Requests;

namespace LRMS.Application.SpaceState;

public interface ISpaceStateService
{
    Task<GetSpaceStateResponse> GetSpaceStateAsync(CancellationToken ct = default);
}
