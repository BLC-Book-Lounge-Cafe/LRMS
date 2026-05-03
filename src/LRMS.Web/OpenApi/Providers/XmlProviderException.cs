namespace LRMS.Web.OpenApi.Providers;

internal class XmlProviderException : Exception
{
    public XmlProviderException()
    {
    }

    public XmlProviderException(string? message) : base(message)
    {
    }

    public XmlProviderException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
