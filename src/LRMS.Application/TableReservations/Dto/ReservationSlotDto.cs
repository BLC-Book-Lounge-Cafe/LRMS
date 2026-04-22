namespace LRMS.Application.TableReservations.Dto;

/// <summary>
///     Данные слота для резервирования.
/// </summary>
public class ReservationSlotDto
{
    /// <summary>
    ///     Время начала.
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    ///     Время конца.
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    ///     true - слот зарезервирован, иначе - false.
    /// </summary>
    public bool IsReserved { get; set; }
}