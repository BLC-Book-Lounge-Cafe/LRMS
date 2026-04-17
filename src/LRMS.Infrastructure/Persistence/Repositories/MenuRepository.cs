using LRMS.Application.Dto;
using LRMS.Application.Repositories;
using LRMS.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LRMS.Infrastructure.Persistence.Repositories;

public class MenuRepository(LrmsDbContext dbContext) : IMenuRepository
{
    private readonly LrmsDbContext _dbContext = dbContext;

    public async Task<IReadOnlyCollection<MenuCategoryDto>> GetMenuItems(CancellationToken ct = default)
    {
        var categories = await _dbContext.MenuCategories.ToListAsync(ct);
        var itemsDictionary = await _dbContext.MenuItems.GroupBy(i => i.CategoryId).ToDictionaryAsync(i => i.Key, ct);

        List<MenuCategoryDto> result = [];
        foreach (var category in categories)
        {
            var categoryDto = MenuMapper.ToMenuCategoryDto(category);
            categoryDto.MenuItems = itemsDictionary.TryGetValue(category.Id, out var items)
                ? [.. items.Select(MenuMapper.ToMenuItemDto)]
                : [];
            result.Add(categoryDto);
        }

        return result;
    }
}
