using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RoboYaDBL.Mongo
{
    interface IMongoDocumentRepository
    {
        TDocument ReadAndDelete<TDocument>(Expression<Func<TDocument, bool>> filter);
        List<TDocument> Read<TDocument>(Expression<Func<TDocument, bool>> filter);
        void Write<TDocument>(TDocument document);
        void Write<TDocument>(IEnumerable<TDocument> documents);
    }
}