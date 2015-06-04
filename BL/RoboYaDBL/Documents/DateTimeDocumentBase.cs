using System;
using MongoDB.Bson;

namespace RoboYaDBL.Documents
{
    class DateTimeDocumentBase : IMongoDocument, IHasDateTime
    {
        public ObjectId _id { get; set; }
        public DateTime datetime { get; set; }
    }
}