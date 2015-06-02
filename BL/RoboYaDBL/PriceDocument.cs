using System;
using MongoDB.Bson;

namespace RoboYaDBL
{
    class PriceDocument : Document
    {
        public string Phrase { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public double PremiumMax { get; set; }
        public double PremiumMin { get; set; }
    }
}