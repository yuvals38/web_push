
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI_Push.Models;
using WebPush;

namespace WebAPI_Push.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductsController : ApiController
    {
        private string endPoint { get; set; }
        private string privateKey { get; set; }
        private string publicKey { get; set; }
        //one time setup -- should be in config file after creation. used in main.js
        private const string vapidPublicKEY = "BEwlWteE_ICdDxIdssM_u6mdP9Z6zNUGFx8yMabjYZVykoE3ZfMQ08M3vYjlRcDrzgBWTNAnjCnzycj3JGC_7Rk";
        private const string vapidPrivateKey = "_-qIEuo3cRUBw48j7nslSu1ia8Rx8hxh2bkzQhC3Wyo";

        public Product[] products = new Product[] { new Product { name = "newname" } };

        public Subscriber[] subscribers = new Subscriber[] { new Subscriber { Name = "xxxxxx" } };

        public IEnumerable<Product> GetAllSubscribers()
        {
           
            //vapid key one time generation on startup of server?
            return products;

        }
        //public IEnumerable<Subscriber> GetAll()
        //{
        //    SubscriberModel context = new SubscriberModel();
        //    subscribers = context.Subscribers.ToArray();
        //    return subscribers;
        //}


        public IHttpActionResult GetProduct(string id)
        {
            //test
            //sendPushNotificationClient(endPoint, publicKey, privateKey, "");

            //sendPushNotificationClient();
            //var product = products.FirstOrDefault((p) => p.Id == id);
            if (id == null)
            {
                return NotFound();
            }

            return Ok(id);
        }

        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage AddSubscriber([FromBody]string subscriptiondata)
        {
            if (subscriptiondata != null)
            {
                string clientDomain = string.Empty;
                var referrer = Request.Headers.Referrer;
                if (referrer != null)
                {
                    clientDomain = referrer.GetLeftPart(UriPartial.Authority);
                }

                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Subscription));
                MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(subscriptiondata));
                stream.Position = 0;
                Subscription dataContractDetail = (Subscription)jsonSerializer.ReadObject(stream);

                endPoint = dataContractDetail.endpoint;
                publicKey = dataContractDetail.keys.p256dh;
                privateKey = dataContractDetail.keys.auth;

                string clientIPAddress = IPController.GetClientIpAddress(Request);

                if (dataContractDetail.actions != null)
                {
                    if (dataContractDetail.actions == "subscribe")
                    {
                        //subsrcibe user -insert/update DB
                        SaveData("new name", privateKey, publicKey, "notification from server", "message here", clientDomain, endPoint);
                        sendPushNotificationClient(endPoint, publicKey, privateKey, subscriptiondata, clientDomain);
                    }
                    else if (dataContractDetail.actions == "unsubscribe")
                    {
                        //delete sub from db
                        DeleteSubscriber(publicKey);
                    }
                }

            }


            return new HttpResponseMessage()
            {
                Content = new StringContent("POST: Test message: " + subscriptiondata)
            };
        }


        private async void sendPushNotificationClient(string endPoint, string publicKey, string privateKey, string jsonsubdata, string url)
        {
            //await when fetching from DB
            var pushSubscription = new PushSubscription(endPoint.Trim(), publicKey.Trim(), privateKey.Trim());

            var webPushClient = new WebPushClient();
            //vapid keys generated once on startup of server first time
            webPushClient.SetVapidDetails(url.Trim(), vapidPublicKEY,vapidPrivateKey);

            //db query external msg

            Subscriber sub = GetSubscriberData(publicKey);
            string title = sub.Header.Trim();
            string body = sub.Message.Trim();

            JObject payloadjs = new JObject(
                new JProperty("title", title),
                new JProperty("body", body)
                );

            webPushClient.SendNotification(pushSubscription, payloadjs.ToString());//,"test",vapidDetails);

        }

        public Subscriber GetSubscriberData(string publicKey)
        {
            SubscriberModel db = new SubscriberModel();

            var subdata = (from subscribers in db.Subscribers
                           where subscribers.PublicKey.Equals(publicKey)
                           select subscribers).SingleOrDefault();
            return subdata;
        }

        void SaveData(string newname, string privateKey, string publicKey, string header, string message, string clientDomain, string endPoint)
        {
            try
            {
                using (var db = new SubscriberModel())
                {
                    var query = from st in db.Subscribers
                                where st.PublicKey == publicKey
                                select st;

                    var sub = query.FirstOrDefault<Subscriber>();

                    if (sub == null)
                    {
                        var subData = new Subscriber
                        {
                            Name = newname.Trim(),
                            PrivateKey = privateKey.Trim(),
                            PublicKey = publicKey.Trim(),
                            Header = header.Trim(),
                            Message = message.Trim(),
                            URL = clientDomain.Trim(),
                            Endpoint = endPoint.Trim()
                        };
                        db.Subscribers.Add(subData);
                        db.SaveChanges();

                        // Display all 

                    }

                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var errors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in errors.ValidationErrors)
                    {
                        // get the error message 
                        string errorMessage = validationError.ErrorMessage;
                    }
                }
            }
        }

        void DeleteSubscriber(string publicKey)
        {
            try
            {
                using (var db = new SubscriberModel())
                {
                    var subtoremove = db.Subscribers.SingleOrDefault(x => x.PublicKey == publicKey); //returns a single item.

                    if (subtoremove != null)
                    {
                        db.Subscribers.Remove(subtoremove);
                        db.SaveChanges();
                    }

                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var errors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in errors.ValidationErrors)
                    {
                        // get the error message 
                        string errorMessage = validationError.ErrorMessage;
                    }
                }
            }
        }

        [HttpPut]
        public void SendNotificationManager([FromBody]int subID)
        {
            using (var db = new SubscriberModel())
            {
                var query = from st in db.Subscribers
                            where st.ID == subID
                            select st;

                var sub = query.FirstOrDefault<Subscriber>();

                if (sub != null)
                {
                    sendPushNotificationClient(sub.Endpoint, sub.PublicKey, sub.PrivateKey,"", sub.URL);

                }
            }
        }
    }
}
