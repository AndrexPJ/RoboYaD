using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace RoboYaDBL.Mongo
{
    class MongoCollectionContainer : IMongoCollectionContainer
    {
        private readonly IMongoDatabase database;
        private readonly Dictionary<Type, string> typeCollectionNames;

        public MongoCollectionContainer(IMongoDatabase database, IEnumerable<KeyValuePair<Type, string>> typeCollectionNamePairs)
        {
            this.database = database;
            typeCollectionNames = typeCollectionNamePairs.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public IMongoCollection<TDocument> Get<TDocument>()
        {
            var collectionName = typeCollectionNames[typeof(TDocument)];
            return database.GetCollection<TDocument>(collectionName);
        } 
    }
}