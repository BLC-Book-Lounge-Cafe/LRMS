namespace LRMS.Infrastructure.ReservationManagerApi.Tables.Dto;

public record struct TableSlotsResponse(IReadOnlyCollection<TableSlotModel> slots);
