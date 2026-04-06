using LRMS.Application.Repositories;
using LRMS.Application.Requests;

namespace LRMS.Application.Services;

public class SpaceStateService(ISpaceStateRepository repository) : ISpaceStateService
{
    private readonly ISpaceStateRepository _repository = repository;

    public async Task<GetSpaceStateResponse> GetSpaceStateAsync(CancellationToken ct = default)
    {
        return new(await _repository.GetSpaceStateAsync(ct));
    }
}
