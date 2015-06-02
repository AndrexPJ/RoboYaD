using System.Collections.Generic;

namespace RoboYaDBL
{
    class MainDocumentProccessor : IDocumentProccessor<PriceDocument, CommandDocument>
    {
        public CommandDocument Proccess(List<PriceDocument> documents)
        {
            return new CommandDocument();
        }
    }
}