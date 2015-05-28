using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FE.Models
{
    public class yandex_bot
    {
        [ScaffoldColumn(false)]
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("lol")]
        public string Name{get;set;}

   
    }
}