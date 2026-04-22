using LRMS.Application.Menu.Dto;
using LRMS.Infrastructure.Persistence.Menu;
using Riok.Mapperly.Abstractions;

namespace LRMS.Infrastructure.Mappers;

[Mapper]
public static partial class MenuMapper
{
    [MapperIgnoreTarget(nameof(MenuCategoryDto.MenuItems))]
    public static partial MenuCategoryDto ToMenuCategoryDto(MenuCategoryEntity entity);

    [MapperIgnoreSource(nameof(MenuItemEntity.CategoryId))]
    public static partial MenuItemDto ToMenuItemDto(MenuItemEntity entity);
}
