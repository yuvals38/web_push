using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebAPI_Push.Models
{
    public class Product
    {
      
        public string name { get; set; }
       
    }
    [DataContract]
    public class Subscription
    {
        [DataMember(Name = "endpoint")]
        public string endpoint { get; set; }
        [DataMember(Name = "expirationtime")]
        public string expirationtime { get; set; }
        [DataMember(Name = "keys")]
        public Keys keys { get; set; }
        [DataMember(Name = "actions")]
        public string actions { get; set; }
    }
    [DataContract]
    public class Keys
    {
        [DataMember(Name = "p256dh")]
        public string p256dh { get; set; }
        [DataMember(Name = "auth")]
        public string auth { get; set; }
    }
}