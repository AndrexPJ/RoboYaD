using MongoDB.Bson;

namespace RoboYaDBL
{
    class Document : IDocument
    {
        public ObjectId _id { get; set; }
        public string datetime { get; set; }
    }
}