using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FE.Models;
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

           //  HttpContext.User.

            string connectionString = "mongodb://ilya:ilya@ds049641.mongolab.com:49641/yandex_bot";

            var client = new MongoClient(connectionString);
            

            MongoServer server = client.GetServer();

            MongoDatabase db = server.GetDatabase("yandex_bot");
           

            var collection = db.GetCollection<ApplicationUser>("AspNetUsers").Find(Query.Matches("UserName", User.Identity.Name)).FirstOrDefault();// "\"UserName\"=" + User.Identity.Name);
            var tmp = collection.ClientLogin;



            ViewBag.company = company;
            return View();
        }
    }
}