using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Linq.Expressions;

namespace RoboYaDBL
{
    class Program
    {

        interface IDocumentProccessor<TIn, TOut>
        {
            TOut Proccess(TIn document);
        }

        class MainDocumentProccessor : IDocumentProccessor<HistoryDocument, CommandDocument>
        {
            public CommandDocument Proccess(HistoryDocument document)
            {
                return new CommandDocument();
            }
        }

        class ApplicationProccessor<TIn, TOut>
        {
            IDocumentRepository<TIn> readDocumentRepository;
            IDocumentRepository<TOut> writeDocumentRepository;
            IDocumentProccessor<TIn, TOut> documentProccessor;
            Expression<Func<TIn, bool>> readFilter;
            public ApplicationProccessor(IDocumentRepository<TIn> readDocumentRepository, IDocumentRepository<TOut> writeDocumentRepository, 
                                  IDocumentProccessor<TIn, TOut> documentProccessor, Expression<Func<TIn, bool>> readFilter)
            {
                this.readDocumentRepository = readDocumentRepository;
                this.writeDocumentRepository = writeDocumentRepository;
                this.documentProccessor = documentProccessor;
                this.readFilter = readFilter;
            }
            public void Proccess()
            {
                Console.WriteLine("Start document proccessing");
                var inDocument = readDocumentRepository.ReadSync(readFilter);
                var outDocument = documentProccessor.Proccess(inDocument);
                writeDocumentRepository.WriteSync(outDocument);
                Console.WriteLine("Document proccessed");
            }
        }

        static void Main(string[] args)
        {
            IMongoClient client = new MongoClient("mongodb://ilya:ilya@ds049641.mongolab.com:49641/yandex_bot");
            var db = client.GetDatabase("yandex_bot");

            var historyRepository = new DocumentRepository<HistoryDocument>(db, "HistoryCollection");
            var commandRepository = new DocumentRepository<CommandDocument>(db, "CommandCollection");
            var documentProccessor = new MainDocumentProccessor();

            var applicationProccess = new ApplicationProccessor<HistoryDocument, CommandDocument>(historyRepository, commandRepository, documentProccessor, d => true);

            var procThread = new Thread(new ThreadStart(() => {
                while (true)
                {
                    applicationProccess.Proccess();
                    Console.WriteLine("Sleep");
                    Thread.Sleep(2 * 1000 * 60);
                }
            }));

            procThread.Start();
            procThread.Join();
        }
    }
}
