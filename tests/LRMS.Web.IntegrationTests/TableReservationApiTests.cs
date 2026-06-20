using LRMS.Web.IntegrationTests.ApiClient.Models;
using LRMS.Web.IntegrationTests.Fixtures;
using Microsoft.Kiota.Abstractions.Serialization;

namespace LRMS.Web.IntegrationTests;

public class TableReservationApiTests(BackendFixture backendFixture) : IClassFixture<BackendFixture>
{
    private readonly BackendFixture _backendFixture = backendFixture;

    [Fact]
    public async Task GetSlotsSuccessful()
    {
        var slots = await _backendFixture.Client.TableReservations.Slots.PostAsync(new()
        {
            Date = DateTime.Now,
            TableId = new UntypedInteger(5)
        }, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(slots?.ReservationSlots);
        Assert.NotEmpty(slots.ReservationSlots);
    }

    [Fact]
    public async Task GetSlotsWithInvalidTableId()
    {
        await Assert.ThrowsAsync<ErrorResponse>(async () =>
            await _backendFixture.Client.TableReservations.Slots.PostAsync(new()
            {
                Date = DateTime.Now,
                TableId = new UntypedInteger(89)
            }, cancellationToken: TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task CreateTableReservationSuccessful()
    {
        await _backendFixture.Client.TableReservations.PostAsync(new()
        {
            CustomerName = "test",
            CustomerPhone = "+79001234567",
            TableId = new UntypedInteger(5),
            StartTime = DateTime.Now,
            EndTime = DateTime.Now.AddHours(2),
        }, cancellationToken: TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task CreateTableReservationWithInvalidPhone()
    {
        await Assert.ThrowsAsync<ErrorResponse>(async () =>
            await _backendFixture.Client.TableReservations.PostAsync(new()
            {
                CustomerName = "test",
                CustomerPhone = "89001234567",
                TableId = new UntypedInteger(1),
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2),
            }, cancellationToken: TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task CreateTableReservationWithInvalidTableId()
    {
        await Assert.ThrowsAsync<ErrorResponse>(async () =>
            await _backendFixture.Client.TableReservations.PostAsync(new()
            {
                CustomerName = "test",
                CustomerPhone = "+79001234567",
                TableId = new UntypedInteger(89),
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(2),
            }, cancellationToken: TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task CreateTableReservationWithInvalidTime()
    {
        await Assert.ThrowsAsync<ErrorResponse>(async () =>
            await _backendFixture.Client.TableReservations.PostAsync(new()
            {
                CustomerName = "test",
                CustomerPhone = "+79001234567",
                TableId = new UntypedInteger(89),
                StartTime = DateTime.Now.AddHours(2),
                EndTime = DateTime.Now,
            }, cancellationToken: TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task GetTableReservationSuccessful()
    {
        var result = await _backendFixture.ClientWithAuthorization.TableReservations.GetAsync(
            cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(result?.TableReservations);
        Assert.NotEmpty(result.TableReservations);
    }

    [Fact]
    public async Task DeleteTableReservationSuccessful()
    {
        var result = await _backendFixture.ClientWithAuthorization.TableReservations.GetAsync(
            cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(result?.TableReservations);
        Assert.NotEmpty(result.TableReservations);

        var id = ((UntypedInteger)result.TableReservations.First().Id).GetValue();

        await _backendFixture.ClientWithAuthorization.TableReservations[id].DeleteAsync(
            cancellationToken: TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task DeleteTableReservationWithInvalidTableReservations()
    {
        await Assert.ThrowsAsync<ErrorResponse>(async () =>
            await _backendFixture.ClientWithAuthorization.TableReservations[-1].DeleteAsync(
                cancellationToken: TestContext.Current.CancellationToken));
    }
}
