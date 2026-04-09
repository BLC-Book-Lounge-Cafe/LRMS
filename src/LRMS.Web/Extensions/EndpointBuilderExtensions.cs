using LRMS.Web.Api;
using LRMS.Web.Contracts;
using LRMS.Web.OpenApi;

namespace LRMS.Web.Extensions;

public static class EndpointBuilderExtensions
{
    extension(RouteHandlerBuilder builder)
    {
        public RouteHandlerBuilder ProducesCommonErrors(
            string? conflictDescription = null,
            string? notFoundDescription = null,
            string? internalErrorDescription = null)
        {
            builder.ProducesWithDescription(StatusCodes.Status400BadRequest,
                "В случае некорректно составленного запроса.");
            builder.ProducesWithDescription<ErrorResponse>(StatusCodes.Status404NotFound,
                notFoundDescription ?? "В случае, если запрашиваемая сущность не найдена.");
            builder.ProducesWithDescription<ErrorResponse>(StatusCodes.Status409Conflict,
                conflictDescription ?? "В случае нарушения доменных правил.");
            builder.ProducesWithDescription<ErrorResponse>(StatusCodes.Status500InternalServerError,
                internalErrorDescription ?? "В случае внутренней ошибки сервера.");
            return builder;
        }
    }

    extension(IEndpointRouteBuilder endpointRouteBuilder)
    {
        public IEndpointRouteBuilder MapApi()
        {
            endpointRouteBuilder.MapReservationRequestsApi();
            endpointRouteBuilder.MapSpaceStateApi();
            endpointRouteBuilder.MapTableReservationsApi();
            return endpointRouteBuilder;
        }
    }

}
