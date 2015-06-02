using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace RoboYaDBL
{
    class DocumentRepository<TDocument> : IDocumentRepository<TDocument>
    {
        private readonly IMongoCollection<TDocument> collection;
        public DocumentRepository(IMongoDatabase db, string collectionName)
        {
            collection = db.GetCollection<TDocument>(collectionName);
        }
        public TDocument ReadSync(Expression<Func<TDocument, bool>> filter)
        {
            var task = collection.FindOneAndDeleteAsync<TDocument>(new ExpressionFilterDefinition<TDocument>(filter));
            return task.Result;
        }

        public List<TDocument> ReadManySync(Expression<Func<TDocument, bool>> filter)
        {
            var task = collection.FindAsync(filter);
            return task.Result.ToListAsync().Result;
        }

        public void WriteSync(TDocument document)
        {
            var task = collection.InsertOneAsync(document);
            task.Wait();
        }
    }

}
