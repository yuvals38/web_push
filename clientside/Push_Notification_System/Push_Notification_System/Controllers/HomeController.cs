using Push_Notification_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Push_Notification_System.Controllers
{
    //[RoutePrefix("Home")]
    public class HomeController : Controller
    {
        Subscription model = new Subscription();
        public ActionResult Index()
        {
           
            return View(model);
        }

        public HomeController()
        {
        }


       // [Route("create", Name = "Create"),  HttpPost]
        [HttpPost]
        public ActionResult Create(string SubscriptionData)
        {
            try
            {
                //save to db
                
            }
            catch (Exception e )
            {

            }
            return RedirectToAction("Index",model);
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