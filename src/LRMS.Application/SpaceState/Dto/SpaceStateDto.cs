namespace LRMS.Application.SpaceState.Dto;

/// <summary>
///     Данные о состоянии пространства.
/// </summary>
public class SpaceStateDto
{
    /// <summary>
    ///     Уровень шума в процентах.
    /// </summary>
    public byte NoiseLevel { get; set; }

    /// <summary>
    ///     Уровень загруженности в процентах.
    /// </summary>
    public byte WorkloadLevel { get; set; }

    /// <summary>
    ///     Описание от администратора.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    ///     Текущий трек.
    /// </summary>
    public CurrentTrackDto CurrentTrack { get; set; }
}
