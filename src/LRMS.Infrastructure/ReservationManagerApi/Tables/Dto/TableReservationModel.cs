namespace LRMS.Infrastructure.ReservationManagerApi.Tables.Dto;

public record struct TableReservationModel(int id, int table_id, string name, string phone, DateTime start_at, DateTime end_at);
