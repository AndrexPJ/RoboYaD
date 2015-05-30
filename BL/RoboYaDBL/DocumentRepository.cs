using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace RoboYaDBL
{

    class HistoryDocument
    {

    }

    class CommandDocument
    {

    }

    class DocumentRepository<TDocument> : IDocumentRepository<TDocument>
    {
        private IMongoCollection<TDocument> collection;
        public DocumentRepository(IMongoDatabase db, string collectionName)
        {
            this.collection = db.GetCollection<TDocument>(collectionName);
        }
        public TDocument ReadSync(Expression<Func<TDocument, bool>> filter)
        {
            var task = this.collection.FindOneAndDeleteAsync<TDocument>(new ExpressionFilterDefinition<TDocument>(filter));
            return task.Result;
        }
        public void WriteSync(TDocument document)
        {
            var task = this.collection.InsertOneAsync(document);
            task.Wait();
        }
    }

}
