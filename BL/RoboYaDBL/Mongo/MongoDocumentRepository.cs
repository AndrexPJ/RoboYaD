using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;

namespace RoboYaDBL.Mongo
{
    class MongoDocumentRepository : IMongoDocumentRepository
    {
        private readonly IMongoCollectionContainer collectionContainer;

        public MongoDocumentRepository(IMongoCollectionContainer collectionContainer)
        {
            this.collectionContainer = collectionContainer;
        }

        public TDocument ReadAndDelete<TDocument>(Expression<Func<TDocument, bool>> filter)
        {
            var collection = collectionContainer.Get<TDocument>();
            var task = collection.FindOneAndDeleteAsync<TDocument>(filter);
            return task.Result;
        }

        public List<TDocument> Read<TDocument>(Expression<Func<TDocument, bool>> filter)
        {
            var collection = collectionContainer.Get<TDocument>();
            var task = collection.FindAsync<TDocument>(filter);
            return task.Result.ToListAsync().Result;
        }

        public void Write<TDocument>(TDocument document)
        {
            var collection = collectionContainer.Get<TDocument>();
            var task = collection.InsertOneAsync(document);
            task.Wait();
        }

        public void Write<TDocument>(IEnumerable<TDocument> documents)
        {
            var collection = collectionContainer.Get<TDocument>();
            var task = collection.InsertManyAsync(documents);
            task.Wait();
        }
    }
}