using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using Newtonsoft.Json.Converters;
using System.Data.SQLite;
using System.Web.Hosting;

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

            CreateTableForAttachBuffer();
        }

        private static void CreateTableForAttachBuffer()
        {
            string pathDb = HostingEnvironment.MapPath("~/App_Data/BufferDb.db");
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + pathDb + ";Version=3;"))
            {
                connection.Open();
                string sql = "CREATE TABLE IF NOT EXISTS 'BUFFER' ('ATTACH_ID' INT, 'FRAME' INT, 'DATA' BLOB);";
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
