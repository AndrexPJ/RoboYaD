using MongoDB.Driver;

namespace RoboYaDBL.Mongo
{
    interface IMongoCollectionContainer
    {
        IMongoCollection<TDocument> Get<TDocument>();
    }
}