using LRMS.Application.ReservationRequests.Dto;
using LRMS.Infrastructure.Persistence.ReservationRequests;
using Riok.Mapperly.Abstractions;

namespace LRMS.Infrastructure.Mappers;

[Mapper]
public partial class ReservationRequestMapper
{
    [MapperIgnoreSource(nameof(ReservationRequestEntity.CreatedAt))]
    public static partial ReservationRequestDto ToDto(ReservationRequestEntity entity);
}
