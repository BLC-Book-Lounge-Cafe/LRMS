using LRMS.Application.Menu;
using LRMS.Infrastructure.Exceptions;
using LRMS.Infrastructure.IntegrationTests.Fixtures;
using Microsoft.Extensions.DependencyInjection;
using Snapshooter.Xunit3;

namespace LRMS.Infrastructure.IntegrationTests;

[Trait("Category", "Integration")]
[Collection("Database collection")]
public class MenuRepositoryTests(ServiceFixture serviceFixture) : IClassFixture<ServiceFixture>
{
    private readonly ServiceFixture _serviceFixture = serviceFixture;

    [Fact]
    public async Task CreateMenuCategorySuccessful()
    {
        var menuRepository = _serviceFixture.GetRequiredService<IMenuRepository>();

        var result = await menuRepository.CreateMenuCategory(new("Category", [
            new("Item1", 50),
            new("Item2", 60),
            new("Item3", 70),
            new("Item4", 50),
            new("Item5", 150),
            ]), TestContext.Current.CancellationToken);

        result.MatchSnapshot();
    }

    [Fact]
    public async Task GetMenuSuccessful()
    {
        var menuRepository = _serviceFixture.GetRequiredService<IMenuRepository>();

        var result = await menuRepository.GetMenuItems(TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task UpdateMenuCategorySuccessful()
    {
        var menuRepository = _serviceFixture.GetRequiredService<IMenuRepository>();

        var result = await menuRepository.UpdateMenuCategory(2, new("Category", [
            new(0, "Item1", 50),
            new(1, "Item2", 60),
            new(2, "Item3", 70),
            new(0, "Item4", 50),
            new(3, "Item5", 150),
            ]), TestContext.Current.CancellationToken);

        result.MatchSnapshot();
    }

    [Fact]
    public async Task UpdateMenuCategoryFailure()
    {
        var menuRepository = _serviceFixture.GetRequiredService<IMenuRepository>();

        await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            await menuRepository.UpdateMenuCategory(10000, new("Category", [
                new(0, "Item1", 50),
                new(1, "Item2", 60),
                new(2, "Item3", 70),
                new(0, "Item4", 50),
                new(3, "Item5", 150),
                ]), TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task DeleteMenuCategorySuccessful()
    {
        var menuRepository = _serviceFixture.GetRequiredService<IMenuRepository>();

        await menuRepository.DeleteMenuCategory(1, TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task DeleteMenuCategoryFailure()
    {
        var menuRepository = _serviceFixture.GetRequiredService<IMenuRepository>();

        await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
            await menuRepository.DeleteMenuCategory(10000, TestContext.Current.CancellationToken));
    }
}
