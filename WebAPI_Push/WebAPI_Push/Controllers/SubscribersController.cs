using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using WebAPI_Push.Models;
using WebPush;

namespace WebAPI_Push.Controllers
{
    public class SubscribersController : Controller
    {
        private string endPoint { get; set; }
        private string privateKey { get; set; }
        private string publicKey { get; set; }


        
        Subscriber[] subscribers;
      
        public ActionResult Index()
        {
          
                //vapid key one time generation on startup of server?
                SubscriberModel context = new SubscriberModel();
                subscribers = context.Subscribers.ToArray();

                 return View(subscribers);
     
          

        }

        [System.Web.Http.HttpPost]
        private HttpResponseMessage sendPushNotificationClient(string endPoint, string publicKey, string privateKey, string jsonsubdata, string url)
        {
            //await when fetching from DB
            var pushSubscription = new PushSubscription(endPoint, publicKey, privateKey);

            var webPushClient = new WebPushClient();
            //vapid keys generated once on startup of server first time
            webPushClient.SetVapidDetails(url, "BEwlWteE_ICdDxIdssM_u6mdP9Z6zNUGFx8yMabjYZVykoE3ZfMQ08M3vYjlRcDrzgBWTNAnjCnzycj3JGC_7Rk", "_-qIEuo3cRUBw48j7nslSu1ia8Rx8hxh2bkzQhC3Wyo");

            //db query external msg

            Subscriber sub = GetSubscriberData(publicKey);
            string title = sub.Header.Trim();
            string body = sub.Message.Trim();

            JObject payloadjs = new JObject(
                new JProperty("title", title),
                new JProperty("body", body)
                );

            webPushClient.SendNotification(pushSubscription, payloadjs.ToString());//,"test",vapidDetails);

            return new HttpResponseMessage()
            {
                Content = new StringContent("POST: Test message: " + "passed")
            };

        }

        public Subscriber GetSubscriberData(string publicKey)
        {
            SubscriberModel db = new SubscriberModel();

            var subdata = (from subscribers in db.Subscribers
                           where subscribers.PublicKey.Equals(publicKey)
                           select subscribers).SingleOrDefault();
            return subdata;
        }


    }
}
