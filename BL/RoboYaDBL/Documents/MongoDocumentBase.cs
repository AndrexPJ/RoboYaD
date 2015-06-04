using MongoDB.Bson;

namespace RoboYaDBL.Documents
{
    class MongoDocumentBase : IMongoDocument
    {
        public ObjectId _id { get; set; }
    }
}