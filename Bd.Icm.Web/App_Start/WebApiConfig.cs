using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json.Serialization;

namespace Bd.Icm.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.MapHttpAttributeRoutes();
        }
    }
}
