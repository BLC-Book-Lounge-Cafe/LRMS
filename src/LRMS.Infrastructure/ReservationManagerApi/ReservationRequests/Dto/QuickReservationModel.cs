namespace LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Dto;

public record struct QuickReservationModel(int id, string status, string name, string phone);
