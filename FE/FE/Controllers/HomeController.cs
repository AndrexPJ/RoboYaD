using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FE.Models;
using FE.Utils;
using MongoDB.Driver.Builders;

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

            var ClientLogin = MongoDBConection.Connection<ApplicationUser>("AspNetUsers")
              .Find(Query.Matches("UserName", User.Identity.Name))
               .FirstOrDefault()
               .ClientLogin;


            var list = MongoDBConection.Connection<Copanies>("companies").Distinct("Name", Query.Matches("ClientLogin", ClientLogin)).ToList();

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