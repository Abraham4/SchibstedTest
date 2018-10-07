using DomainsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ApiUserManagement.Filters
{
    public class AuthoritzationFilterAttribute : AuthorizeAttribute
    {
        public string UserRole { get; set; }


        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new RedirectResult("/Account/Login");
            }
            else
            {

                var id = filterContext.HttpContext.User.Identity.AuthenticationType;
                UserDomainService userService = new UserDomainService();
                var user = Task.Run(async () => await userService.GetUserRolesById(new Guid(id))).Result;

                bool existsRole = false;
                foreach (var role in user)
                {
                    if (UserRole.Contains(role)) existsRole = true;
                }
                if (!existsRole)
                {
                    filterContext.HttpContext.Response.Clear();
                    filterContext.HttpContext.Response.StatusDescription = "Access Denied";
                    filterContext.HttpContext.Response.Write("Access Denied, You don't have enough permissions to see this page");
                    filterContext.HttpContext.Response.StatusCode = 403;
                    filterContext.Result = new EmptyResult();
                    filterContext.HttpContext.Response.End();
                }

            }
        }
    }
}