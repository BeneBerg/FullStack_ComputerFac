using API.Areas.HelpPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы Web API

            // Маршруты Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.SetDocumentationProvider(new XmlDocumentationProvider(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin\\API.xml")));
        }
    }
}
