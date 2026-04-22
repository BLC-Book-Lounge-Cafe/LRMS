using LRMS.Application.SpaceState;
using LRMS.Application.SpaceState.Commands;
using LRMS.Application.SpaceState.Requests;
using LRMS.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LRMS.Web.Api;

public static class SpaceStateRouteGroup
{
    extension(IEndpointRouteBuilder endpointRouteBuilder)
    {
        public IEndpointRouteBuilder MapSpaceStateApi()
        {
            var group = endpointRouteBuilder.MapGroup("/space-state");

            group.MapGet("/", GetSpaceState)
                .WithName("GetSpaceState")
                .WithDescription("Возвращает текущее состояние пространства.")
                .Produces<GetSpaceStateResponse>()
                .ProducesCommonErrors();

            group.MapPatch("/", UpdateSpaceState)
                .WithName("UpdateSpaceState")
                .WithDescription("Обновляет уровень шума и описание текущего состояния пространства.")
                .Produces(StatusCodes.Status200OK)
                .ProducesCommonErrors(conflictDescription: "В случае, если уровень шума находится вне диапазона от 0 до 100, " +
                    "либо описание пустое.");

            return endpointRouteBuilder;
        }

        private static async Task<IResult> GetSpaceState(
            [FromServices] ISpaceStateService service,
            CancellationToken ct = default)
        {
            return TypedResults.Ok(await service.GetSpaceStateAsync(ct));
        }

        private static async Task<IResult> UpdateSpaceState(
            UpdateSpaceStateCommand command,
            [FromServices] ISpaceStateService service,
            CancellationToken ct = default)
        {
            await service.UpdateSpaceStateAsync(command, ct);
            return TypedResults.Ok();
        }
    }
}
