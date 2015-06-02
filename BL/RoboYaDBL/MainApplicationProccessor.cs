using System;
using System.Linq.Expressions;

namespace RoboYaDBL
{
    class MainApplicationProccessor<TIn, TOut>
    {
        readonly IDocumentRepository<TIn> readDocumentRepository;
        readonly IDocumentRepository<TOut> writeDocumentRepository;
        readonly IDocumentProccessor<TIn, TOut> documentProccessor;
        readonly Expression<Func<TIn, bool>> readFilter;
        public MainApplicationProccessor(IDocumentRepository<TIn> readDocumentRepository, IDocumentRepository<TOut> writeDocumentRepository, 
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
            var inDocuments = readDocumentRepository.ReadManySync(readFilter);
            var outDocument = documentProccessor.Proccess(inDocuments);
            writeDocumentRepository.WriteSync(outDocument);
            Console.WriteLine("Document proccessed");
        }
    }
}