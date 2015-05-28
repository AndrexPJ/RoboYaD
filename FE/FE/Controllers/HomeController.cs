using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FE.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        [Authorize]
        public ActionResult Profile()
        {


            IList<string> list = new List<string>();
            list.Add("12312412412");
            list.Add("56443535434");
            list.Add("57656565445");
            ViewBag.ListCompany = list;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}