using System;
using System.Linq.Expressions;
namespace RoboYaDBL
{
    interface IDocumentRepository<TDocument>
    {
        TDocument ReadSync(Expression<Func<TDocument, bool>> filter);
        void WriteSync(TDocument document);
    }
}
