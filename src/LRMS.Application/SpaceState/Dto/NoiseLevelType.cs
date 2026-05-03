namespace LRMS.Application.SpaceState.Dto;

/// <summary>
///     Тип уровня шума.
/// </summary>
public enum NoiseLevelType : byte
{
    /// <summary>
    ///     Очень тихо.
    /// </summary>
    VeryQuiet,

    /// <summary>
    ///     Спокойная обстановка.
    /// </summary>
    CalmEnvironment,

    /// <summary>
    ///     Умеренный шум.
    /// </summary>
    ModerateNoise,

    /// <summary>
    ///     Оживленно.
    /// </summary>
    Lively,

    /// <summary>
    ///     Заметно оживленно.
    /// </summary>
    NoticeablyLively,

    /// <summary>
    ///     Концерт с живой музыкой
    /// </summary>
    LiveMusicConcert
}
