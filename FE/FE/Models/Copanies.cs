using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FE.Models
{
    [BsonIgnoreExtraElements]
    public class Copanies
    {

        [BsonId]
        public ObjectId Id { get; set; }

        
        public string Name { get; set; }
        
       // public float Rest { get; set; }

        public string ClientLogin { get; set; }

        public DateTime datetime { get; set; }

        public int CampaignID { get; set;}

        public int Clicks { get; set; }

        public int Shows { get; set; }
        public int Sum { get; set; }

        public string Status { get; set; }

    }
}