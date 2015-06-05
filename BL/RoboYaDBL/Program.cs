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
            const int timeIntervalMs = 2*1000*60;
            const int dataTimeIntervalMs = timeIntervalMs;

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
            var mainApplicationProccessor = new MainApplicationProccessor(documentRepository, commandBuilder, dataTimeIntervalMs);

            var procThread = new Thread(() =>
            {
                while (true)
                {
                    ConsoleTimeLogger.Log("WAKE UP");
                    try
                    {
                        mainApplicationProccessor.Proccess();
                    }
                    catch (Exception ex)
                    {
                        ConsoleTimeLogger.Log(string.Format("ERROR {0}", ex.Message));
                    }
                    ConsoleTimeLogger.Log("SLEEP");
                    Thread.Sleep(timeIntervalMs);
                }
            });

            procThread.Start();
            procThread.Join();
        }
    }

}
