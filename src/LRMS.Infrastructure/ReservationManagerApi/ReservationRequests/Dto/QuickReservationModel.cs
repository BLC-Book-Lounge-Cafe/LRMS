namespace LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Dto;

public record struct QuickReservationModel(long id, string status, string name, string phone);
