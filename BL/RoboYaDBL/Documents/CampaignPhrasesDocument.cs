namespace RoboYaDBL.Documents
{
    class CampaignPhrasesDocument : MongoDocumentBase
    {
        public int CampaignID { get; set; }

        public int[] PhraseIDs { get; set; }
    }
}