namespace LRMS.Web.Contracts;

/// <summary>
///     Описывает ошибку, возникшую в процессе обработки команды.
/// </summary>
/// <param name="Message">Текстовое сообщение ошибки.</param>
public record ErrorResponse(string Message);
