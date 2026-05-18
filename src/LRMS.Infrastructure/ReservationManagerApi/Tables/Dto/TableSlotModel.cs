namespace LRMS.Infrastructure.ReservationManagerApi.Tables.Dto;

public record struct TableSlotModel(DateTime start_at, DateTime end_at, bool is_reserved);
