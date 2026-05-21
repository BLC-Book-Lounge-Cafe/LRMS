using LRMS.Application.Exceptions;
using LRMS.Application.Menu.Commands;
using LRMS.Application.Menu.Dto;
using LRMS.Application.Menu.Requests;

namespace LRMS.Application.Menu;

public class MenuService(IMenuRepository repository) : IMenuService
{
    private readonly IMenuRepository _repository = repository;

    public async Task<MenuCategoryDto> CreateMenuCategory(CreateMenuCategoryCommand category, CancellationToken ct = default)
    {
        ValidateName(category.Name);
        foreach (var item in category.MenuItems)
            ValidateMenuItem(item.Name, item.Price);
        return await _repository.CreateMenuCategory(category, ct);
    }

    public async Task DeleteMenuCategory(long id, CancellationToken ct = default)
    {
        await _repository.DeleteMenuCategory(id, ct);
    }

    public async Task<GetMenuResponse> GetMenu(CancellationToken ct = default)
    {
        return new(await _repository.GetMenuItems(ct));
    }

    public async Task<MenuCategoryDto> UpdateMenuCategory(long id, UpdateMenuCategoryCommand command, CancellationToken ct = default)
    {
        ValidateName(command.Name);
        foreach (var item in command.MenuItems)
            ValidateMenuItem(item.Name, item.Price);
        return await _repository.UpdateMenuCategory(id, command, ct);
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new DataValidationException("Название категории меню не может быть пустым.");

        if (name.Length > 255)
            throw new DataValidationException("Название категории меню не может превышать 255 символов.");
    }

    private static void ValidateMenuItem(string name, double price)
    {
        if (string.IsNullOrEmpty(name))
            throw new DataValidationException("Название элемента меню не может быть пустым.");

        if (name.Length > 255)
            throw new DataValidationException("Название элемента меню не может превышать 255 символов.");

        if (price < 0)
            throw new DataValidationException("Цена элемента меню не может быть меньше 0.");
    }
}
