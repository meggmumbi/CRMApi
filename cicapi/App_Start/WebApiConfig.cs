using System.Runtime.Serialization;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace cicapi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            // config.Filters.Add(new AuthorizeAttribute());
            config.EnableCors();
            config.Filters.Add(new BasicAuthenticationAttribute());
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }
    }
}
