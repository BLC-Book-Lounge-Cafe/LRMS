using LRMS.Application.Exceptions;
using LRMS.Application.Menu;
using LRMS.Application.Menu.Dto;
using LRMS.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LRMS.Infrastructure.Persistence.Menu;

public class MenuRepository(LrmsDbContext dbContext) : IMenuRepository
{
    private readonly LrmsDbContext _dbContext = dbContext;

    public async Task<MenuCategoryDto> CreateMenuCategory(MenuCategoryForCreateDto category, CancellationToken ct = default)
    {
        var categoryEntity = new MenuCategoryEntity
        {
            Name = category.Name
        };

        await _dbContext.MenuCategories.AddAsync(categoryEntity, ct);
        await _dbContext.SaveChangesAsync(ct);

        List<MenuItemEntity> itemEntities = [];
        foreach (var item in category.MenuItems)
        {
            var itemEntity = MenuMapper.ToEntity(item);
            itemEntity.CategoryId = categoryEntity.Id;
        }
        await _dbContext.MenuItems.AddRangeAsync(itemEntities, ct);
        await _dbContext.SaveChangesAsync(ct);

        return new()
        {
            Id = categoryEntity.Id,
            Name = categoryEntity.Name,
            MenuItems = [.. itemEntities.Select(MenuMapper.ToMenuItemDto)]
        };
    }

    public async Task DeleteMenuCategory(long id, CancellationToken ct = default)
    {
        var category = await _dbContext.MenuCategories.FindAsync([id], ct)
            ?? throw new EntityNotFoundException("Категория меню не найдена.");

        _dbContext.MenuCategories.Remove(category);
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

    public async Task UpdateMenuCategory(MenuCategoryDto category, CancellationToken ct = default)
    {
        var categoryEntity = await _dbContext.MenuCategories.FindAsync([category.Id], ct)
            ?? throw new EntityNotFoundException("Не удалось найти категорию меню.");

        categoryEntity.Name = category.Name;
        _dbContext.MenuCategories.Update(categoryEntity);

        var allItemEntities = await _dbContext.MenuItems
            .Where(i => i.CategoryId == category.Id)
            .ToDictionaryAsync(i => i.Id, ct);

        List<MenuItemEntity> itemsToCreate = [];
        List<MenuItemEntity> itemsToUpdate = [];
        foreach (var item in category.MenuItems)
        {
            if (!allItemEntities.TryGetValue(item.Id, out var itemEntity))
            {
                itemEntity = new()
                {
                    Name = item.Name,
                    Price = item.Price,
                    CategoryId = category.Id
                };
                itemsToCreate.Add(itemEntity);
                continue;
            }

            itemEntity.Name = item.Name;
            itemEntity.Price = item.Price;
            itemsToUpdate.Add(itemEntity);
            allItemEntities.Remove(item.Id);
        }

        await _dbContext.MenuItems.AddRangeAsync(itemsToCreate, ct);
        _dbContext.MenuItems.UpdateRange(itemsToUpdate);
        _dbContext.MenuItems.RemoveRange(allItemEntities.Values);
        await _dbContext.SaveChangesAsync(ct);
    }
}
