using LRMS.Application.SpaceState.Requests;

namespace LRMS.Application.SpaceState;

public class SpaceStateService(ISpaceStateRepository repository) : ISpaceStateService
{
    private readonly ISpaceStateRepository _repository = repository;

    public async Task<GetSpaceStateResponse> GetSpaceStateAsync(CancellationToken ct = default)
    {
        return new(await _repository.GetSpaceStateAsync(ct));
    }
}
