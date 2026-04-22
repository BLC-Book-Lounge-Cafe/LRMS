using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace LRMS.Web.OpenApi;

internal class ReturnCodeOpenApiOperationTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        var description = context.Description;
        List<IProducesResponseTypeMetadata> producesMetadata = [.. description.ActionDescriptor.EndpointMetadata.OfType<IProducesResponseTypeMetadata>()];
        if (producesMetadata.Count == 0 || operation.Responses is null)
            return Task.CompletedTask;

        foreach (var produces in producesMetadata)
        {
            if (string.IsNullOrEmpty(produces.Description))
                continue;

            string responseCode = produces.StatusCode.ToString();
            if (!operation.Responses.TryGetValue(responseCode, out var response))
            {
                response = new OpenApiResponse();
                operation.Responses.Add(responseCode, response);
            }

            response.Description = produces.Description;
        }

        return Task.CompletedTask;
    }
}
