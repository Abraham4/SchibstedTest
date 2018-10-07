using ApiUserManagement.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using WebApplicationTest.Filters;

namespace WebApplicationTest.Controllers
{

    public class HomeController : Controller
    {
        [AuthoritzationFilter(UserRole = "PAGE_1,PAGE_2,PAGE_3,Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [AuthoritzationFilter(UserRole ="PAGE_1")]
        public ActionResult PAGE1()
        {
            return View();
        }

        [AuthoritzationFilter(UserRole ="PAGE_2")]
        public ActionResult PAGE2()
        {
            return View();
        }

        [AuthoritzationFilter(UserRole ="PAGE_3")]
        public ActionResult PAGE3()
        {
            return View();
        }
        
    }
}