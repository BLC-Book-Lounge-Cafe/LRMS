namespace LRMS.Application.SpaceState.Dto;

/// <summary>
///     Данные текущего трека.
/// </summary>
/// <param name="Name">Название.</param>
/// <param name="Authors">Исполнители.</param>
/// <param name="ImageUrl">Url картинки.</param>
public record struct CurrentTrackDto(string Name, IReadOnlyCollection<string> Authors, string ImageUrl);
