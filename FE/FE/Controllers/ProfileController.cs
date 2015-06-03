using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FE.Models;
using FE.Utils;
using Microsoft.AspNet.Identity;
using MongoDB.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

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
        public  ActionResult Statistic(string company)
        {
            if (company == null)
                throw new HttpException(404, "lol");

            var ClientLogin = MongoDBConection.Connection<ApplicationUser>("AspNetUsers")
              .Find(Query.Matches("UserName", User.Identity.Name))
               .FirstOrDefault()
               .ClientLogin;

            var lastElem = MongoDBConection.Connection<Copanies>("companies")
             .Find(Query.Matches("Name", company))
             .SetSortOrder(SortBy.Descending("datetime"))
             .SetLimit(1);



            var elemnts = MongoDBConection.Connection<Copanies>("companies")
           .Find(Query.Matches("Name", company))
           .SetSortOrder(SortBy.Ascending("datetime"))
           .SetLimit(12);


            ViewBag.labels = "\"" + String.Join("\", \"", elemnts.Select(x => x.datetime)) + "\"";
            ViewBag.dateShows = "\"" + String.Join("\", \"", elemnts.Select(x => x.Shows)) + "\""; 
            ViewBag.dateClicks = "\"" + String.Join("\", \"", elemnts.Select(x => x.Clicks)) + "\"";



            ViewBag.ctrStat = "\"" + String.Join("\", \"", elemnts.Select<Copanies, string>(x => ((double)x.Clicks * 100.0 / (double)x.Shows).ToString(CultureInfo.GetCultureInfo("en-GB")))) + "\""; 
            ViewBag.midllePrice = "\"" + String.Join("\", \"", elemnts.Select<Copanies, string>(x => ((double)x.Sum / (double)x.Clicks).ToString(CultureInfo.GetCultureInfo("en-GB")))) + "\""; 


            ViewBag.lastElem = lastElem.First();

            ViewBag.company = company;
            return View();
        }
    }
}