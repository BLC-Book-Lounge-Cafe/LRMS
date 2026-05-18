using LRMS.Infrastructure.ReservationManagerApi.Tables.Commands;
using LRMS.Infrastructure.ReservationManagerApi.Tables.Dto;
using LRMS.Infrastructure.ReservationManagerApi.Tables.Requests;
using Riok.Mapperly.Abstractions;

namespace LRMS.Infrastructure.Mappers;

[Mapper]
internal partial class TableReservationMapper
{
    [MapProperty(nameof(TableSlotModel.start_at), nameof(ReservationSlotDto.StartTime))]
    [MapProperty(nameof(TableSlotModel.end_at), nameof(ReservationSlotDto.EndTime))]
    [MapProperty(nameof(TableSlotModel.is_reserved), nameof(ReservationSlotDto.IsReserved))]
    public static partial ReservationSlotDto ToDto(TableSlotModel model);

    [MapProperty(nameof(CreateTableReservationCommand.TableId), nameof(CreateTableReservationDto.table_id))]
    [MapProperty(nameof(CreateTableReservationCommand.CustomerName), nameof(CreateTableReservationDto.name))]
    [MapProperty(nameof(CreateTableReservationCommand.CustomerPhone), nameof(CreateTableReservationDto.phone))]
    [MapProperty(nameof(CreateTableReservationCommand.StartTime), nameof(CreateTableReservationDto.start_at), Use = nameof(ToISOFormat))]
    [MapProperty(nameof(CreateTableReservationCommand.EndTime), nameof(CreateTableReservationDto.end_at), Use = nameof(ToISOFormat))]
    public static partial CreateTableReservationDto ToDto(CreateTableReservationCommand model);

    [MapProperty(nameof(TableReservationsResponse.reservations), nameof(GetTableReservationsResponse.TableReservations))]
    [MapProperty(nameof(TableReservationsResponse.page_number), nameof(GetTableReservationsResponse.PageNumber))]
    [MapProperty(nameof(TableReservationsResponse.page_size), nameof(GetTableReservationsResponse.PageSize))]
    [MapProperty(nameof(TableReservationsResponse.total_entries), nameof(GetTableReservationsResponse.TotalEntries))]
    [MapProperty(nameof(TableReservationsResponse.total_pages), nameof(GetTableReservationsResponse.TotalPages))]
    public static partial GetTableReservationsResponse ToResponse(TableReservationsResponse response);

    [MapProperty(nameof(TableReservationModel.id), nameof(TableReservationDto.Id))]
    [MapProperty(nameof(TableReservationModel.table_id), nameof(TableReservationDto.TableId))]
    [MapProperty(nameof(TableReservationModel.name), nameof(TableReservationDto.CustomerName))]
    [MapProperty(nameof(TableReservationModel.phone), nameof(TableReservationDto.CustomerPhone))]
    [MapProperty(nameof(TableReservationModel.start_at), nameof(TableReservationDto.StartTime))]
    [MapProperty(nameof(TableReservationModel.end_at), nameof(TableReservationDto.EndTime))]
    public static partial TableReservationDto ToDto(TableReservationModel model);

    private static string ToISOFormat(DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
    }
}
