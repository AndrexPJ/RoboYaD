using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FE.Models
{
    public class StatusCompany
    {
        [BsonId]
        public ObjectId Id { get; set; }


        public bool ShowInTop{ get; set; }
        public bool IsActive{ get; set; }
        public bool ShowInBottom{ get; set; }
        public int CampaignID{ get; set; }

        public float MaxPrice{ get; set; }

    }
}