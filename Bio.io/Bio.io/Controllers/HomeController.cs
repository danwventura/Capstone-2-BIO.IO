using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bio.io.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Active()
        {
            ViewBag.Message = "Your active devices page.";

            return View();
        }

        public ActionResult AllRoutes() //If I have a seperate View for this should I just include this in the .cs controller?
        {
            ViewBag.Message = "Your all routes page.";

            return View();
        }

        public ActionResult RouteDetails()
        {
            ViewBag.Message = "Your route details page.";

            return View();
        }

        public ActionResult Images()
        {
            ViewBag.Message = "Your images page.";

            return View();
        }

        public ActionResult Account()
        {
            ViewBag.Message = "Your account page.";

            return View();
        }
    }
}