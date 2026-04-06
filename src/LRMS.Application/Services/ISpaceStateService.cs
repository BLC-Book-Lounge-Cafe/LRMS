using LRMS.Application.Requests;

namespace LRMS.Application.Services;

public interface ISpaceStateService
{
    Task<GetSpaceStateResponse> GetSpaceStateAsync(CancellationToken ct = default);
}
