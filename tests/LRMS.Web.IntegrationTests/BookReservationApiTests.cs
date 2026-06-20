using LRMS.Web.IntegrationTests.ApiClient.Models;
using LRMS.Web.IntegrationTests.Fixtures;
using Microsoft.Kiota.Abstractions.Serialization;

namespace LRMS.Web.IntegrationTests;

public class BookReservationApiTests(BackendFixture backendFixture) : IClassFixture<BackendFixture>
{
    private readonly BackendFixture _backendFixture = backendFixture;

    [Fact]
    public async Task CreateBookReservationSuccessful()
    {
        await _backendFixture.Client.BookReservations.PostAsync(new()
        {
            BookId = new UntypedInteger(1),
            CustomerName = "test",
            CustomerPhone = "+79001234567",
            Date = DateTime.Now
        }, cancellationToken: TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task CreateBookReservationWithInvalidPhone()
    {
        await Assert.ThrowsAsync<ErrorResponse>(async () =>
            await _backendFixture.Client.BookReservations.PostAsync(new()
            {
                BookId = new UntypedInteger(1),
                CustomerName = "test",
                CustomerPhone = "89001234567",
                Date = DateTime.Now
            }, cancellationToken: TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task CreateBookReservationWithInvalidBookId()
    {
        await Assert.ThrowsAsync<ErrorResponse>(async () =>
            await _backendFixture.Client.BookReservations.PostAsync(new()
            {
                BookId = new UntypedInteger(-1),
                CustomerName = "test",
                CustomerPhone = "+79001234567",
                Date = DateTime.Now
            }, cancellationToken: TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task CreateBookReservationWithInvalidDate()
    {
        await Assert.ThrowsAsync<ErrorResponse>(async () =>
            await _backendFixture.Client.BookReservations.PostAsync(new()
            {
                BookId = new UntypedInteger(1),
                CustomerName = "test",
                CustomerPhone = "+79001234567",
                Date = DateTime.Now.AddDays(-1)
            }, cancellationToken: TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task GetBookReservationsSuccessful()
    {
        var result = await _backendFixture.ClientWithAuthorization.BookReservations.GetAsync(
            cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(result?.BookReservations);
        Assert.NotEmpty(result.BookReservations);
    }

    [Fact]
    public async Task DeleteBookReservationSuccessful()
    {
        var result = await _backendFixture.ClientWithAuthorization.BookReservations.GetAsync(
            cancellationToken: TestContext.Current.CancellationToken);

        var id = ((UntypedInteger)result.BookReservations.First().Id).GetValue();

        await _backendFixture.ClientWithAuthorization.BookReservations[id].DeleteAsync(
            cancellationToken: TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task DeleteBookReservationFailure()
    {
        await Assert.ThrowsAsync<ErrorResponse>(async () =>
            await _backendFixture.ClientWithAuthorization.BookReservations[-1].DeleteAsync(
                cancellationToken: TestContext.Current.CancellationToken));
    }
}
