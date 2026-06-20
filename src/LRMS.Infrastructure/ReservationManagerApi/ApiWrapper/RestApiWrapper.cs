using LRMS.Infrastructure.Exceptions;
using LRMS.Infrastructure.ReservationManagerApi.ApiWrapper.Dto;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;

namespace LRMS.Infrastructure.ReservationManagerApi.ApiWrapper;

internal static class RestApiWrapper
{
    public async static Task<T> CallApi<T>(Task<HttpResponseMessage> func, CancellationToken ct = default)
    {
        var response = await func;

        if (!response.IsSuccessStatusCode)
            await ThrowException(response, ct);

        var content = await response.Content.ReadFromJsonAsync<T>(ct);
        return content is null ? throw new Exception("Content is null.") : content;
    }

    public async static Task CallApi(Task<HttpResponseMessage> func, CancellationToken ct = default)
    {
        var response = await func;

        if (!response.IsSuccessStatusCode)
            await ThrowException(response, ct);
    }

    private static async Task ThrowException(HttpResponseMessage response, CancellationToken ct = default)
    {
        var error = await response.Content.ReadFromJsonAsync<ErrorDto>(ct);
        var message = error.error.message;
        throw response.StatusCode switch
        {
            System.Net.HttpStatusCode.BadRequest => new BadRequestException(message),
            System.Net.HttpStatusCode.Conflict => new DomainException(message),
            System.Net.HttpStatusCode.NotFound => new EntityNotFoundException(message),
            _ => new Exception(message)
        };
    }
}
