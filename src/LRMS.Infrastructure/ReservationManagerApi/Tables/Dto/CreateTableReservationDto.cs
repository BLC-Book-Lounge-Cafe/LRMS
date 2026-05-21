namespace LRMS.Infrastructure.ReservationManagerApi.Tables.Dto;

public record struct CreateTableReservationDto(long table_id, string name, string phone, string start_at, string end_at);
