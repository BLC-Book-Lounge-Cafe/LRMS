using LRMS.Application.Dto;
using LRMS.Infrastructure.Persistence.Models;
using Riok.Mapperly.Abstractions;

namespace LRMS.Infrastructure.Mappers;

[Mapper]
public partial class ReservationRequestMapper
{
    [MapperIgnoreSource(nameof(ReservationRequestEntity.CreatedAt))]
    public static partial ReservationRequestDto ToDto(ReservationRequestEntity car);
}
