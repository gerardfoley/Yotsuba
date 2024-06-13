namespace Yotsuba;

internal static class Extensions
{
    public static bool IsEnPage(this HttpContext context)
    {
        var icic = StringComparison.InvariantCultureIgnoreCase;
        return context.Request.Path.ToString().StartsWith("/en", icic);
    }

    public static bool IsJaPage(this HttpContext context)
    {
        var icic = StringComparison.InvariantCultureIgnoreCase;
        return context.Request.Path.ToString().StartsWith("/ja", icic);
    }
}