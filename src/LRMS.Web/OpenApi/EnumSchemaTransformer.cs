using LRMS.Web.OpenApi.Providers;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace LRMS.Web.OpenApi;

internal class EnumSchemaTransformer(
    XmlCommentProvider xmlCommentProvider,
    bool ignoreExceptions) : IOpenApiSchemaTransformer
{
    private readonly XmlCommentProvider _xmlCommentProvider = xmlCommentProvider;
    private readonly bool _ignoreExceptions = ignoreExceptions;

    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        try
        {
            if (context.JsonPropertyInfo is not null && context.JsonPropertyInfo.PropertyType.IsEnum)
            {
                var enumType = context.JsonPropertyInfo.PropertyType;
                var enumElementNames = Enum.GetNames(enumType);
                var enumUnderlyingType = Enum.GetUnderlyingType(enumType);
                IList<IOpenApiSchema> schemas = [];
                foreach (var enumElementName in enumElementNames)
                {
                    var description = _xmlCommentProvider.GetComment(GetElementFullName(enumType, enumElementName),
                        XmlCommentType.Summary, XmlCommentMemberType.Field);
                    schemas.Add(new OpenApiSchema()
                    {
                        Title = enumElementName,
                        Const = Convert.ChangeType(Enum.Parse(enumType, enumElementName), enumUnderlyingType).ToString(),
                        Description = description
                    });
                }
                schema.OneOf = schemas;
            }
        }
        catch
        {
            if (!_ignoreExceptions)
                throw;
        }
        return Task.CompletedTask;
    }

    private static string GetElementFullName(Type enumType, string enumElementName) =>
        $"{enumType.FullName}.{enumElementName}";

}
