using LRMS.Infrastructure.ReservationManagerApi.Books.Dto;
using Riok.Mapperly.Abstractions;

namespace LRMS.Infrastructure.Mappers;

[Mapper]
public static partial class BookReservationMapper
{
    [MapProperty(nameof(CreateBookReservationCommand.BookId), nameof(CreateBookReservationDto.book_id))]
    [MapProperty(nameof(CreateBookReservationCommand.CustomerName), nameof(CreateBookReservationDto.name))]
    [MapProperty(nameof(CreateBookReservationCommand.CustomerPhone), nameof(CreateBookReservationDto.phone))]
    [MapProperty(nameof(CreateBookReservationCommand.Date), nameof(CreateBookReservationDto.reserved_at), Use = nameof(ToISOFormat))]
    public static partial CreateBookReservationDto ToDto(CreateBookReservationCommand command);

    [MapProperty(nameof(BookReservationModel.id), nameof(BookReservationDto.Id))]
    [MapProperty(nameof(BookReservationModel.book_id), nameof(BookReservationDto.BookId))]
    [MapProperty(nameof(BookReservationModel.name), nameof(BookReservationDto.CustomerName))]
    [MapProperty(nameof(BookReservationModel.phone), nameof(BookReservationDto.CustomerPhone))]
    [MapProperty(nameof(BookReservationModel.reserved_at), nameof(BookReservationDto.Date))]
    public static partial BookReservationDto ToDto(BookReservationModel model);

    [MapProperty(nameof(BookReservationsResponse.reservations), nameof(GetBookReservationsResponse.BookReservations))]
    [MapProperty(nameof(BookReservationsResponse.page_number), nameof(GetBookReservationsResponse.PageNumber))]
    [MapProperty(nameof(BookReservationsResponse.page_size), nameof(GetBookReservationsResponse.PageSize))]
    [MapProperty(nameof(BookReservationsResponse.total_pages), nameof(GetBookReservationsResponse.TotalPages))]
    [MapProperty(nameof(BookReservationsResponse.total_entries), nameof(GetBookReservationsResponse.TotalEntries))]
    public static partial GetBookReservationsResponse ToResponse(BookReservationsResponse response);

    private static string ToISOFormat(DateTime dateTime)
    {
        return dateTime.ToString("yyyy-MM-dd");
    }
}
