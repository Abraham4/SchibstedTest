using DomainsServices;
using DomainsServices.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Data.Entity;
using System.Security.Principal;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Mvc5;
using WebApplicationTest.Controllers;
using WebApplicationTest.Models;

namespace WebApplicationTest
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IUserDomainService, UserDomainService>();

            container.RegisterType<AccountController>(new InjectionConstructor(
                container.Resolve<IUserDomainService>()
                ));


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}