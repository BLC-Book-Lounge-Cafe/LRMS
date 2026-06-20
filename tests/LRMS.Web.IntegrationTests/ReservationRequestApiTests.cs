using LRMS.Web.IntegrationTests.ApiClient.Models;
using LRMS.Web.IntegrationTests.Fixtures;
using Microsoft.Kiota.Abstractions.Serialization;

namespace LRMS.Web.IntegrationTests;

public class ReservationRequestApiTests(BackendFixture backendFixture) : IClassFixture<BackendFixture>
{
    private readonly BackendFixture _backendFixture = backendFixture;

    [Fact]
    public async Task CreateReservationRequestSuccessful()
    {
        await _backendFixture.Client.ReservationRequests.PostAsync(new()
        {
            CustomerName = "test",
            CustomerPhone = "+79001234567"
        }, cancellationToken: TestContext.Current.CancellationToken);
    }

    [Fact]
    public async Task CreateReservationRequestWithEmptyName()
    {
        await Assert.ThrowsAsync<ErrorResponse>(async () =>
            await _backendFixture.Client.ReservationRequests.PostAsync(new()
            {
                CustomerName = "",
                CustomerPhone = "+79001234567"
            }, cancellationToken: TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task CreateReservationRequestWithInvalidPhone()
    {
        await Assert.ThrowsAsync<ErrorResponse>(async () =>
            await _backendFixture.Client.ReservationRequests.PostAsync(new()
            {
                CustomerName = "test",
                CustomerPhone = "89001234567"
            }, cancellationToken: TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task GetReservationRequestsSuccessful()
    {
        var result = await _backendFixture.ClientWithAuthorization.ReservationRequests.GetAsync(
            cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(result?.ReservationRequests);
        Assert.NotEmpty(result.ReservationRequests);
    }

    [Fact]
    public async Task UpdateReservationRequestsSuccessful()
    {
        var response = await _backendFixture.ClientWithAuthorization.ReservationRequests.GetAsync(
            cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(response?.ReservationRequests);
        var reservationRequest = response.ReservationRequests.First(r => r.Status == "pending");

        var id = ((UntypedInteger)reservationRequest.Id).GetValue();

        var result = await _backendFixture.ClientWithAuthorization.ReservationRequests[id]
            .PutAsync(new()
        {
            Status = "confirmed"
        }, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(result);
        Assert.Equal("confirmed", result.Status);
    }

    [Fact]
    public async Task UpdateReservationRequestsFailure()
    {
        var response = await _backendFixture.ClientWithAuthorization.ReservationRequests.GetAsync(
            cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(response?.ReservationRequests);
        var reservationRequest = response.ReservationRequests.First(r => r.Status == "confirmed");

        var id = ((UntypedInteger)reservationRequest.Id).GetValue();

        await Assert.ThrowsAsync<ErrorResponse>(async () =>
            await _backendFixture.ClientWithAuthorization.ReservationRequests[id]
                .PutAsync(new()
                {
                    Status = "confirmed"
                }, cancellationToken: TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task UpdateReservationRequestsWithInvalidStatus()
    {
        var response = await _backendFixture.ClientWithAuthorization.ReservationRequests.GetAsync(
            cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(response?.ReservationRequests);
        var reservationRequest = response.ReservationRequests.First(r => r.Status == "pending");

        var id = ((UntypedInteger)reservationRequest.Id).GetValue();

        await Assert.ThrowsAsync<ErrorResponse>(async () =>
            await _backendFixture.ClientWithAuthorization.ReservationRequests[id]
                .PutAsync(new()
                {
                    Status = "status"
                }, cancellationToken: TestContext.Current.CancellationToken));
    }
}
