using LRMS.Application.BookReservations.Dto;
using LRMS.Infrastructure.Persistence.BookReservations;
using Riok.Mapperly.Abstractions;

namespace LRMS.Infrastructure.Mappers;

[Mapper]
public static partial class BookReservationMapper
{
    [MapperIgnoreTarget(nameof(BookReservationEntity.CreatedAt))]
    [MapperIgnoreTarget(nameof(BookReservationEntity.Id))]
    [MapProperty(nameof(BookReservationDto.Date), nameof(BookReservationEntity.ReservationDate))]
    public static partial BookReservationEntity ToEntity(BookReservationDto dto);
}
