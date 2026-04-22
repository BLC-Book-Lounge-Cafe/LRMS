using LRMS.Application.Exceptions;
using LRMS.Application.Menu;
using LRMS.Application.Menu.Dto;
using LRMS.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LRMS.Infrastructure.Persistence.Menu;

public class MenuRepository(LrmsDbContext dbContext) : IMenuRepository
{
    private readonly LrmsDbContext _dbContext = dbContext;

    public async Task DeleteMenuCategory(int id, CancellationToken ct = default)
    {
        var category = await _dbContext.MenuCategories.FindAsync([id], ct)
            ?? throw new EntityNotFoundException("Категория меню не найдена.");

        _dbContext.MenuCategories.Remove(category);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task DeleteMenuItem(int id, CancellationToken ct = default)
    {
        var item = await _dbContext.MenuItems.FindAsync([id], ct)
            ?? throw new EntityNotFoundException("Элемент меню не найден.");

        _dbContext.MenuItems.Remove(item);
        await _dbContext.SaveChangesAsync(ct);
    }

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
