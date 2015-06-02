using MongoDB.Bson;

namespace RoboYaDBL
{
    interface IDocument
    {
        ObjectId _id { get; set; }
        string datetime { get; set; }
    }
}