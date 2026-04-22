using LRMS.Application.Exceptions;
using LRMS.Application.SpaceState.Commands;
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
        if (command.NoiseLevel < 0 || command.NoiseLevel > 100)
            throw new DomainException("Уровень шума должен быть в диапазоне от 0 до 100.");

        if (string.IsNullOrEmpty(command.Description))
            throw new DomainException("Описание не может быть пустым.");

        await _repository.UpdateSpaceStateAsync(command.NoiseLevel, command.Description, ct);
    }
}
