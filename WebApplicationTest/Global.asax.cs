using DomainsServices.Entities;
using Newtonsoft.Json;
using System;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebApplicationTest.App_Start;

namespace WebApplicationTest
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
            {
                // Get the forms authentication ticket.
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                UserDto user = JsonConvert.DeserializeObject<UserDto>(authTicket.UserData);

                HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(user.Name,user.Id.ToString()), user.Roles.Split(':'));
            }
           
        }
    }
}
