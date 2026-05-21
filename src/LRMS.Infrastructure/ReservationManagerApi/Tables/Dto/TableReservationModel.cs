namespace LRMS.Infrastructure.ReservationManagerApi.Tables.Dto;

public record struct TableReservationModel(long id, long table_id, string name, string phone, DateTime start_at, DateTime end_at);
