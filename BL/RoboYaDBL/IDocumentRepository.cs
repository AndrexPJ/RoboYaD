using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace RoboYaDBL
{
    interface IDocumentRepository<TDocument>
    {
        TDocument ReadSync(Expression<Func<TDocument, bool>> filter);
        void WriteSync(TDocument document);
        List<TDocument> ReadManySync(Expression<Func<TDocument, bool>> filter);
    }
}
