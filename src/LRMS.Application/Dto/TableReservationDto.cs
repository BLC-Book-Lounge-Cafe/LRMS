namespace LRMS.Application.Dto;

public record struct TableReservationDto(int TableId, string CustomerName, string CustomerPhone, DateTime StartTime, DateTime EndTime);
