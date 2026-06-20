using LRMS.Application.Menu;
using LRMS.Application.Menu.Commands;
using LRMS.Application.Menu.Dto;
using LRMS.Infrastructure.Exceptions;
using LRMS.Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace LRMS.Infrastructure.Persistence.Menu;

public class MenuRepository(LrmsDbContext dbContext) : IMenuRepository
{
    private readonly LrmsDbContext _dbContext = dbContext;

    public async Task<MenuCategoryDto> CreateMenuCategory(CreateMenuCategoryCommand category, CancellationToken ct = default)
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
            itemEntities.Add(itemEntity);
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
                ? [.. items.Select(MenuMapper.ToMenuItemDto).OrderBy(i => i.Name)]
                : [];
            result.Add(categoryDto);
        }

        return [.. result.OrderBy(c => c.Name)];
    }

    public async Task<MenuCategoryDto> UpdateMenuCategory(long id, UpdateMenuCategoryCommand command, CancellationToken ct = default)
    {
        var categoryEntity = await _dbContext.MenuCategories.FindAsync([id], ct)
            ?? throw new EntityNotFoundException("Не удалось найти категорию меню.");

        categoryEntity.Name = command.Name;
        _dbContext.MenuCategories.Update(categoryEntity);

        var allItemEntities = await _dbContext.MenuItems
            .Where(i => i.CategoryId == id)
            .ToDictionaryAsync(i => i.Id, ct);

        List<MenuItemEntity> itemsToCreate = [];
        List<MenuItemEntity> itemsToUpdate = [];
        foreach (var item in command.MenuItems)
        {
            if (!allItemEntities.TryGetValue(item.Id, out var itemEntity))
            {
                itemEntity = new()
                {
                    Name = item.Name,
                    Price = item.Price,
                    CategoryId = id
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

        return new()
        {
            Id = categoryEntity.Id,
            Name = categoryEntity.Name,
            MenuItems = [.. (await _dbContext.MenuItems.Where(i => i.CategoryId == categoryEntity.Id).ToListAsync(ct))
                .Select(MenuMapper.ToMenuItemDto)]
        };
    }
}
