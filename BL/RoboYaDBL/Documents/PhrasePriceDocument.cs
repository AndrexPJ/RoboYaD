namespace RoboYaDBL.Documents
{
    class PhrasePriceDocument : DateTimeDocumentBase
    {
        public int PhraseID { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public double PremiumMax { get; set; }
        public double PremiumMin { get; set; }
    }
}