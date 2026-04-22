using LRMS.Application.Books.Dto;
using LRMS.Infrastructure.Persistence.Books;
using Riok.Mapperly.Abstractions;

namespace LRMS.Infrastructure.Mappers;

[Mapper]
public partial class BookMapper
{
    [MapperIgnoreTarget(nameof(BookDto.IsReserved))]
    [MapProperty(nameof(BookEntity.ImagePath), nameof(BookDto.ImageUrl))]
    public static partial BookDto ToDto(BookEntity entity);
}
