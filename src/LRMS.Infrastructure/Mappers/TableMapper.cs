using LRMS.Application.Tables.Dto;
using LRMS.Infrastructure.Persistence.Tables;
using Riok.Mapperly.Abstractions;

namespace LRMS.Infrastructure.Mappers;

[Mapper]
internal partial class TableMapper
{
    [MapperIgnoreSource(nameof(TableEntity.CreatedAt))]
    public static partial TableDto ToDto(TableEntity entity);
}
