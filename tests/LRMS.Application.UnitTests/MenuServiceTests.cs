using LRMS.Application.Exceptions;
using LRMS.Application.Menu;
using LRMS.Application.Menu.Commands;
using LRMS.Infrastructure.Mappers;
using Moq;
using Snapshooter.Xunit3;

namespace LRMS.Application.UnitTests;

public class MenuServiceTests
{
    [Fact]
    public async Task CreateMenuCategorySuccessful()
    {
        var command = GetCreateMenuCategoryCommand();
        var menuService = new MenuService(CreateMenuRepository(command));

        var result = await menuService.CreateMenuCategory(command, TestContext.Current.CancellationToken);

        result.MatchSnapshot();
    }

    [Fact]
    public async Task ThrowDataValidationExceptionOnEmptyCategoryName()
    {
        var command = GetCreateMenuCategoryCommand(categoryName: string.Empty);
        var menuService = new MenuService(CreateMenuRepository(command));

        await Assert.ThrowsAsync<DataValidationException>(async () => await menuService.CreateMenuCategory(command,
            TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task ThrowDataValidationExceptionOnEmptyItemName()
    {
        var command = GetCreateMenuCategoryCommand(itemName: string.Empty);
        var menuService = new MenuService(CreateMenuRepository(command));

        await Assert.ThrowsAsync<DataValidationException>(async () => await menuService.CreateMenuCategory(command,
            TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task ThrowDataValidationExceptionOnLongItemName()
    {
        var command = GetCreateMenuCategoryCommand(itemName: new string('A', 300));
        var menuService = new MenuService(CreateMenuRepository(command));

        await Assert.ThrowsAsync<DataValidationException>(async () => await menuService.CreateMenuCategory(command,
            TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task ThrowDataValidationExceptionOnLongCategoryName()
    {
        var command = GetCreateMenuCategoryCommand(categoryName: new string('A', 300));
        var menuService = new MenuService(CreateMenuRepository(command));

        await Assert.ThrowsAsync<DataValidationException>(async () => await menuService.CreateMenuCategory(command,
            TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task ThrowDataValidationExceptionOnNegativePrice()
    {
        var command = GetCreateMenuCategoryCommand(price: -10);
        var menuService = new MenuService(CreateMenuRepository(command));

        await Assert.ThrowsAsync<DataValidationException>(async () => await menuService.CreateMenuCategory(command,
            TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task DeleteMenuCategorySuccessful()
    {
        var menuService = new MenuService(CreateMenuRepository());

        await menuService.DeleteMenuCategory(1, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task GetMenuSuccessful()
    {
        var menuService = new MenuService(CreateMenuRepository());

        var result = await menuService.GetMenu(TestContext.Current.CancellationToken);

        result.MatchSnapshot();
    }

    [Fact]
    public async Task UpdateMenuCategorySuccessful()
    {
        var command = new UpdateMenuCategoryCommand("Category2", [ new(0, "Item", 10) ]);
        var menuService = new MenuService(CreateMenuRepository(updateCommand: command));

        var result = await menuService.UpdateMenuCategory(1, command, TestContext.Current.CancellationToken);

        result.MatchSnapshot();
    }

    private static IMenuRepository CreateMenuRepository(
        CreateMenuCategoryCommand? createCommand = null,
        UpdateMenuCategoryCommand? updateCommand = null)
    {
        var mock = new Mock<IMenuRepository>();

        if (createCommand is not null)
            mock.Setup(mock => mock.CreateMenuCategory(It.IsAny<CreateMenuCategoryCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(MenuMapper.ToDto(createCommand.Value));

        if (updateCommand is not null)
            mock.Setup(mock => mock.UpdateMenuCategory(It.IsAny<long>(), It.IsAny<UpdateMenuCategoryCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(MenuMapper.ToDto(updateCommand.Value));

        mock.Setup(mock => mock.GetMenuItems()).ReturnsAsync([
            new() {
                Name = "Category1",
                MenuItems = [
                        new(){
                            Name = "Item1",
                            Price = 2
                        },
                        new(){
                            Name = "Item2",
                            Price = 10
                        },
                ]
            },
            new() {
                Name = "Category2",
                MenuItems = [
                        new(){
                            Name = "Item3",
                            Price = 20
                        },
                        new(){
                            Name = "Item4",
                            Price = 107
                        },
                ]
            },
        ]);

        return mock.Object;
    }

    private static CreateMenuCategoryCommand GetCreateMenuCategoryCommand(
        string? categoryName = null,
        string? itemName = null,
        double? price = null)
    {
        return new()
        {
            Name = categoryName ?? "Category",
            MenuItems = [
                    new(){
                        Name = itemName ?? "Item1",
                        Price = price ?? 1
                    },
                    new(){
                        Name = "Item2",
                        Price = 2
                    }
                ]
        };
    }
}
