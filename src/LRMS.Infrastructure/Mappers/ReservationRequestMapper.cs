using LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Commands;
using LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Dto;
using LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Requests;
using LRMS.Infrastructure.ReservationManagerApi.Tables.Dto;
using LRMS.Infrastructure.ReservationManagerApi.Tables.Requests;
using Riok.Mapperly.Abstractions;

namespace LRMS.Infrastructure.Mappers;

[Mapper]
public partial class ReservationRequestMapper
{

    [MapProperty(nameof(CreateReservationRequestCommand.CustomerName), nameof(CreateQuickReservationDto.name))]
    [MapProperty(nameof(CreateReservationRequestCommand.CustomerPhone), nameof(CreateQuickReservationDto.phone))]
    public static partial CreateQuickReservationDto ToQuickDto(CreateReservationRequestCommand command);

    [MapProperty(nameof(QuickReservationResponse.reservations), nameof(GetReservationRequestsResponse.ReservationRequests))]
    [MapProperty(nameof(QuickReservationResponse.page_number), nameof(GetReservationRequestsResponse.PageNumber))]
    [MapProperty(nameof(QuickReservationResponse.page_size), nameof(GetReservationRequestsResponse.PageSize))]
    [MapProperty(nameof(QuickReservationResponse.total_entries), nameof(GetReservationRequestsResponse.TotalEntries))]
    [MapProperty(nameof(QuickReservationResponse.total_pages), nameof(GetReservationRequestsResponse.TotalPages))]
    public static partial GetReservationRequestsResponse ToResponse(QuickReservationResponse quick);

    [MapProperty(nameof(QuickReservationModel.id), nameof(ReservationRequestDto.Id))]
    [MapProperty(nameof(QuickReservationModel.status), nameof(ReservationRequestDto.Status))]
    [MapProperty(nameof(QuickReservationModel.name), nameof(ReservationRequestDto.CustomerName))]
    [MapProperty(nameof(QuickReservationModel.phone), nameof(ReservationRequestDto.CustomerPhone))]
    public static partial ReservationRequestDto ToDto(QuickReservationModel quickReservationModel);

    [MapProperty(nameof(UpdateReservationRequestCommand.Status), nameof(UpdateQuickReservationDto.status))]
    public static partial UpdateQuickReservationDto ToQuickDto(UpdateReservationRequestCommand command);
}
