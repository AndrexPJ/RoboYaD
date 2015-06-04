using MongoDB.Bson;

namespace RoboYaDBL.Documents
{
    interface IMongoDocument
    {
        ObjectId _id { get; set; }
    }
}