using LRMS.Application.Exceptions;
using LRMS.Application.SpaceState.Commands;
using LRMS.Application.SpaceState.Dto;
using LRMS.Application.SpaceState.Requests;

namespace LRMS.Application.SpaceState;

public class SpaceStateService(ISpaceStateRepository repository) : ISpaceStateService
{
    private readonly ISpaceStateRepository _repository = repository;

    public async Task<GetSpaceStateResponse> GetSpaceStateAsync(CancellationToken ct = default)
    {
        return new(await _repository.GetSpaceStateAsync(ct));
    }

    public async Task UpdateSpaceStateAsync(UpdateSpaceStateCommand command, CancellationToken ct = default)
    {
        if (!Enum.IsDefined(typeof(NoiseLevelType), command.NoiseLevel))
            throw new DataValidationException("Уровень шума должен быть в диапазоне от 0 до 5.");

        await _repository.UpdateSpaceStateAsync(command.NoiseLevel, command.Description, ct);
    }
}
