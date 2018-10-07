using ApiUserManagement.Filters;
using DomainsServices;
using DomainsServices.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using WebApplicationTest;
using WebApplicationTest.Controllers;
using WebApplicationTest.Filters;

namespace WebApplicationTest.App_Start
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
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new BasicAuthenticationAttribute());

            string path = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName;
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }
    }
}