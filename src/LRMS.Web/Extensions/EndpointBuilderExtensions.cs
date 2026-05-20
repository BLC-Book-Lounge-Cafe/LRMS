using LRMS.Web.Api;
using LRMS.Web.Contracts;
using LRMS.Web.OpenApi;

namespace LRMS.Web.Extensions;

public static class EndpointBuilderExtensions
{
    extension(RouteHandlerBuilder builder)
    {
        public RouteHandlerBuilder ProducesCommonErrors(
            string? badRequest = null,
            string? conflictDescription = null,
            string? notFoundDescription = null,
            string? internalErrorDescription = null,
            string? unprocessableErrorDescription = null)
        {
            builder.ProducesWithDescription<ErrorResponse>(StatusCodes.Status400BadRequest,
                badRequest ?? "В случае некорректно составленного запроса.");

            if (notFoundDescription is not null)
            builder.ProducesWithDescription<ErrorResponse>(StatusCodes.Status404NotFound,notFoundDescription);

            if (conflictDescription is not null)
                builder.ProducesWithDescription<ErrorResponse>(StatusCodes.Status409Conflict,conflictDescription);

            if (unprocessableErrorDescription is not null)
                builder.ProducesWithDescription<ErrorResponse>(StatusCodes.Status422UnprocessableEntity, unprocessableErrorDescription);

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
            endpointRouteBuilder.MapTablesApi();
            endpointRouteBuilder.MapBookReservationsApi();
            endpointRouteBuilder.MapMenuApi();
            endpointRouteBuilder.MapBooksApi();
            endpointRouteBuilder.MapAdminLoginApi();
            return endpointRouteBuilder;
        }
    }

}
