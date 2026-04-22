using LRMS.Application.Tables;
using LRMS.Application.Tables.Requests;
using LRMS.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LRMS.Web.Api;

public static class TableRouteGroup
{
    extension(IEndpointRouteBuilder endpointRouteBuilder)
    {
        public IEndpointRouteBuilder MapTablesApi()
        {
            var group = endpointRouteBuilder.MapGroup("/tables");

            group.MapGet("/", GetTables)
                .WithName("GetTables")
                .WithDescription("Возвращает информацию о столах.")
                .Produces<GetTablesResponse>()
                .ProducesCommonErrors();

            return endpointRouteBuilder;
        }

        private static async Task<IResult> GetTables(
            [FromServices] ITableService service,
            CancellationToken ct = default)
        {
            return TypedResults.Ok(await service.GetTables(ct));
        }
    }
}
