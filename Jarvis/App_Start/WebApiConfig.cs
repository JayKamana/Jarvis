using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Jarvis
{
    public static class WebApiConfig
    {
        //public static void register(httpconfiguration config)
        //{
        //    var settings = config.formatters.jsonformatter.serializersettings;
        //    settings.contractresolver = new camelcasepropertynamescontractresolver();
        //    settings.formatting = formatting.indented;

        //    config.maphttpattributeroutes();

        //    config.routes.maphttproute(
        //        name: "defaultapi",
        //        routetemplate: "api/{controller}/{id}",
        //        defaults: new { id = routeparameter.optional }
        //    );
        //}

        public static void Register(HttpConfiguration config)
        {
            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Formatting.Indented;

            config.Routes.MapHttpRoute(
                name: "defaultapi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
