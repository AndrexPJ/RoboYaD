using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using MongoDB.Driver;
using System;
using System.Threading;
using RoboYaDBL.BusinessLogic;
using RoboYaDBL.Documents;
using RoboYaDBL.Mongo;
using CommandDocument = RoboYaDBL.Documents.CommandDocument;

namespace RoboYaDBL
{
    class Program
    {
        static void Main(string[] args)
        {
            IMongoClient client = new MongoClient("mongodb://ilya:ilya@ds049641.mongolab.com:49641/yandex_bot");
            var db = client.GetDatabase("yandex_bot");

            var collectionContainer = new MongoCollectionContainer(db,
                new[]
                {
                    new KeyValuePair<Type, string>(typeof (PhrasePriceDocument), "phrases"),
                    new KeyValuePair<Type, string>(typeof (CommandDocument), "commands"),
                    new KeyValuePair<Type, string>(typeof (CampaignPhrasesDocument), "campaignsPhrases"),
                    new KeyValuePair<Type, string>(typeof (CampaignSettingsDocument), "testCampaignSettings") 
                });

            var documentRepository = new MongoDocumentRepository(collectionContainer);
            var commandBuilder = new CommandBuilder();
            var mainApplicationProccessor = new MainApplicationProccessor(documentRepository, commandBuilder);

            /*var testSettings = new CampaignSettingsDocument
            {
                CampaignID = 92339,
                IsActive = true,
                MaxPrice = 10.0f,
                ShowInBottom = true,
                ShowInTop = true
            };

            documentRepository.Write(testSettings);*/

            var procThread = new Thread(() =>
            {
                while (true)
                {
                    Console.WriteLine("");
                    Console.WriteLine("WAKE UP");
                    mainApplicationProccessor.Proccess();
                    Console.WriteLine("SLEEP");
                    Thread.Sleep(2*1000*60);
                }
            });

            procThread.Start();
            procThread.Join();
        }
    }

}
