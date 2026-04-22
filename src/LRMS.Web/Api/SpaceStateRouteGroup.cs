using LRMS.Application.SpaceState;
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

            return endpointRouteBuilder;
        }

        private static async Task<IResult> GetSpaceState(
            [FromServices] ISpaceStateService service,
            CancellationToken ct = default)
        {
            return TypedResults.Ok(await service.GetSpaceStateAsync(ct));
        }
    }
}
