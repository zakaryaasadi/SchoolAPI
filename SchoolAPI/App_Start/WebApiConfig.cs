using Newtonsoft.Json.Serialization;

using System.Web.Http;

using System.Web.Hosting;
using System.IO;
using System.Web.Http.Cors;

namespace SchoolAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes

            var cors = new EnableCorsAttribute("*", "*", "*");

            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();


            CreateAppDataFolder();
            CreateImagesAndVideosFolder();
        }


        private static void CreateAppDataFolder()
        {
            string path = HostingEnvironment.MapPath("~/App_Data");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }


        private static void CreateImagesAndVideosFolder()
        {
            string path = HostingEnvironment.MapPath("~/images");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            File.WriteAllText(path + @"/web.config", "<?xml version=\"1.0\" encoding=\"UTF-8\"?><configuration><system.webServer><directoryBrowse enabled = \"true\" /></system.webServer></configuration>");


             path = HostingEnvironment.MapPath("~/videos");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            File.WriteAllText(path + @"/web.config", "<?xml version=\"1.0\" encoding=\"UTF-8\"?><configuration><system.webServer><directoryBrowse enabled = \"true\" /></system.webServer></configuration>");
        }
    }
}
