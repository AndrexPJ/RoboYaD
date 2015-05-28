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
        public static void Connection()
        {
            string connectionString = "mongodb://ilya:ilya@ds049641.mongolab.com:49641/yandex_bot";
            
            var client = new MongoClient(connectionString);
          //  var database = client.GetDatabase("foo");
            //var collection = database.GetCollection<BsonDocument>("bar");

             //collection.InsertOneAsync(new BsonDocument("Name", "Jack1")).Wait();


            MongoServer server = client.GetServer();
           

           
                MongoDatabase db = server.GetDatabase("yandex_bot");
                Console.WriteLine("В базе данных {0} имеются следующие коллекции:", db.Name);
                IEnumerable<string> collections = db.GetCollectionNames();
                collections.ToList().ForEach(c => Console.WriteLine(c));
                Console.WriteLine();
            

            /*

            /*var list = await collection.Find(new BsonDocument("Name", "Jack"))
                .ToListAsync();

            foreach (var document in list)
            {
                Console.WriteLine(document["Name"]);
            }*./



            //var colelctions = client.GetDatabase("yandex_bot");
            */

            

        
        }
    }
}