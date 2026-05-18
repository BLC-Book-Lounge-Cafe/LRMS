using LRMS.Infrastructure.Mappers;
using LRMS.Infrastructure.ReservationManagerApi.ApiWrapper;
using LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Commands;
using LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Dto;
using LRMS.Infrastructure.ReservationManagerApi.ReservationRequests.Requests;

namespace LRMS.Infrastructure.ReservationManagerApi.ReservationRequests;

public class ReservationRequestRepository(
    IReservationRequestApi reservationRequestApi) : IReservationRequestRepository
{
    private readonly IReservationRequestApi _reservationRequestApi = reservationRequestApi;

    public async Task CreateReservationRequest(CreateReservationRequestCommand command, CancellationToken ct = default)
    {
        _ = await RestApiWrapper.CallApi<QuickReservationModel>(
            _reservationRequestApi.CreateReservationRequest(ReservationRequestMapper.ToQuickDto(command), ct), ct);
    }

    public async Task<GetReservationRequestsResponse> GetReservationRequests(GetReservationRequest request, CancellationToken ct = default)
    {
        var response = await RestApiWrapper.CallApi<QuickReservationResponse>(
            _reservationRequestApi.GetReservationRequests(request.Status, request.CreatedDate, request.PageNumber, request.PageSize, ct), ct);

        return ReservationRequestMapper.ToResponse(response);
    }

    public async Task<ReservationRequestDto> UpdateReservationRequest(int id, UpdateReservationRequestCommand command, CancellationToken ct = default)
    {
        var response = await RestApiWrapper.CallApi<QuickReservationModel>(
            _reservationRequestApi.UpdateReservationRequest(id, ReservationRequestMapper.ToQuickDto(command), ct), ct);

        return ReservationRequestMapper.ToDto(response);
    }
}
