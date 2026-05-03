using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
namespace LRMS.Web.OpenApi.Providers;

internal partial class XmlCommentProvider
{
    private readonly IDictionary? _commentCache;

    public XmlCommentProvider(bool ignoreExceptions)
    {
        try
        {
            var generatedType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.FullName != null && XmlCommentCacheClassRegex().IsMatch(t.FullName))
                ?? throw new XmlProviderException(
                    "The class 'XmlCommentCache' or the generated OpenApi file wih xml comments is not found");

            var cacheProperty = generatedType.GetProperty("Cache")
                ?? throw new XmlProviderException("The property 'Cache' is not found in class 'XmlCommentCache'");

            _commentCache = cacheProperty.GetValue(null) as IDictionary
                ?? throw new XmlProviderException("Could not convert cache to type 'IDictionary'");
        }
        catch
        {
            if (!ignoreExceptions)
                throw;
        }
    }

    public string? GetComment(string memberFullName, XmlCommentType xmlCommentType, XmlCommentMemberType memberType)
    {
        if (_commentCache is null)
            return null;

        var memberKey = $"{GetMemberKeyPrefix(memberType)}:{memberFullName}";
        var comment = _commentCache[memberKey];
        return comment?.GetType().GetProperty(xmlCommentType.ToString())?.GetValue(comment) as string;
    }

    private static string GetMemberKeyPrefix(XmlCommentMemberType memberType) => memberType switch
    {
        XmlCommentMemberType.Type => "T",
        XmlCommentMemberType.Property => "P",
        XmlCommentMemberType.Method => "M",
        XmlCommentMemberType.Field => "F",
        _ => throw new XmlProviderException("Invalid xml comment member type")
    };

    [GeneratedRegex(@"Microsoft\.AspNetCore\.OpenApi\.Generated\.<OpenApiXmlCommentSupport_generated>[0-9A-F]*__XmlCommentCache")]
    private static partial Regex XmlCommentCacheClassRegex();
}
