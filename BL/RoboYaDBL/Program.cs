using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RoboYaDBL
{
    class Program
    {
        static void Main(string[] args)
        {
            IMongoClient client = new MongoClient("mongodb://ilya:ilya@ds049641.mongolab.com:49641/yandex_bot");
            var db = client.GetDatabase("yandex_bot");

            var historyRepository = new DocumentRepository<PriceDocument>(db, "prices");
            var commandRepository = new DocumentRepository<CommandDocument>(db, "Commands");
            var documentProccessor = new MainDocumentProccessor();

            var mainApplicationProccessor = new MainApplicationProccessor<PriceDocument, CommandDocument>(historyRepository, commandRepository, documentProccessor, d => true);

            var procThread = new Thread(() =>
            {
                while (true)
                {
                    mainApplicationProccessor.Proccess();
                    Console.WriteLine("Sleep");
                    Thread.Sleep(2*1000*60);
                }
            });

            procThread.Start();
            procThread.Join();
        }
    }
}
