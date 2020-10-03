using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Runtime.Serialization;
using System.Web;

namespace WebAPI_Push.Models
{
    public class SubscriberData : DbContext
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string PrivateKey { get; set; }

        [StringLength(100)]
        public string PublicKey { get; set; }

        [StringLength(100)]
        public string Header { get; set; }

        [StringLength(100)]
        public string Message { get; set; }

        [StringLength(100)]
        public string URL { get; set; }

        [StringLength(250)]
        public string Endpoint { get; set; }
    }
}