namespace HtmlToPdfApi.Controllers
{
    public static class ApiVersions
    {
        public const string V1 = "1";
    }

    public static class ApiRouteTemplate
    {
        public const string ROUTE_ENTITY = "api/v{version:apiVersion}/[controller]";
    }
}
