﻿using Newtonsoft.Json.Serialization;

using System.Web.Http;

using System.Web.Hosting;
using System.IO;

namespace SchoolAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
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
        }


        private static void CreateAppDataFolder()
        {
            string path = HostingEnvironment.MapPath("~/App_Data");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
