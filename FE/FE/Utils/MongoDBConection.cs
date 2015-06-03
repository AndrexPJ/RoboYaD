using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FE.Models;
using MongoDB.Bson;
using MongoDB.Driver;



namespace FE.Utils
{
    public class MongoDBConection
    {
        private static MongoDatabase _db = null;

        
        public static MongoCollection<T> Connection<T>(string collectionName)
        {
            if (_db == null)
            {
                string connectionString = "mongodb://ilya:ilya@ds049641.mongolab.com:49641/yandex_bot";
                var client = new MongoClient(connectionString);
                MongoServer server = client.GetServer();
                _db = server.GetDatabase("yandex_bot");
            }

            return _db.GetCollection<T>(collectionName);

        }
    }
}