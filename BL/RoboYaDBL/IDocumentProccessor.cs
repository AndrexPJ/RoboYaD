using System.Collections.Generic;

namespace RoboYaDBL
{
    interface IDocumentProccessor<TIn, out TOut>
    {
        TOut Proccess(List<TIn> documents);
    }
}