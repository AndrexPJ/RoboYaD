using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FE.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult Statistic(string company)
        {
            ViewBag.company = company;
            return View();
        }
    }
}