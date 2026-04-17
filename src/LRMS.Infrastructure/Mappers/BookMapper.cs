using LRMS.Application.Dto;
using LRMS.Infrastructure.Persistence.Models;
using Riok.Mapperly.Abstractions;

namespace LRMS.Infrastructure.Mappers;

[Mapper]
public partial class BookMapper
{
    [MapperIgnoreTarget(nameof(BookDto.IsReserved))]
    [MapProperty(nameof(BookEntity.ImagePath), nameof(BookDto.ImageUrl))]
    public static partial BookDto ToDto(BookEntity entity);
}
