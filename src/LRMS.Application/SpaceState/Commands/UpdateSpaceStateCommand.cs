namespace LRMS.Application.SpaceState.Commands;

/// <summary>
///     Команда на обновление уровня шума и описания пространства.
/// </summary>
/// <param name="NoiseLevel">Уровень шума в процентах.</param>
/// <param name="Description">Описание.</param>
public record struct UpdateSpaceStateCommand(byte NoiseLevel, string Description);
