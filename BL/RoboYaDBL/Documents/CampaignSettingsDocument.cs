namespace RoboYaDBL.Documents
{
    class CampaignSettingsDocument : MongoDocumentBase
    {
        public int CampaignID { get; set; }

        public bool ShowInTop { get; set; }

        public bool ShowInBottom { get; set; }

        public bool IsActive { get; set; }

        public float MaxPrice { get; set; }
    }
}