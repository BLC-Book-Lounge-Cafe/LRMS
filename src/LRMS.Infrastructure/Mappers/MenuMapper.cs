using LRMS.Application.Menu.Commands;
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

    [MapperIgnoreTarget(nameof(MenuItemEntity.CategoryId))]
    [MapperIgnoreTarget(nameof(MenuItemEntity.Id))]
    public static partial MenuItemEntity ToEntity(MenuItemForCreateDto dto);

    [MapperIgnoreTarget(nameof(MenuItemDto.Id))]
    public static partial MenuItemDto ToDto(MenuItemForCreateDto dto);

    [MapperIgnoreTarget(nameof(MenuCategoryDto.Id))]
    public static partial MenuCategoryDto ToDto(CreateMenuCategoryCommand command);

    [MapperIgnoreTarget(nameof(MenuCategoryDto.Id))]
    public static partial MenuCategoryDto ToDto(UpdateMenuCategoryCommand command);
}
