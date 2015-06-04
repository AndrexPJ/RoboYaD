using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver.Linq;
using RoboYaDBL.BusinessLogic;
using RoboYaDBL.Documents;
using RoboYaDBL.Mongo;

namespace RoboYaDBL
{
    class MainApplicationProccessor
    {
        private readonly IMongoDocumentRepository documentRepository;
        private readonly ICommandBuilder commandBuilder;

        public MainApplicationProccessor(IMongoDocumentRepository documentRepository, ICommandBuilder commandBuilder)
        {
            this.documentRepository = documentRepository;
            this.commandBuilder = commandBuilder;
        }

        public void Proccess()
        {
            Console.WriteLine("START reading documents");

            var campaingSettings = GetCampaignSettings();
            var campaingPhrases =
                documentRepository.Read<CampaignPhrasesDocument>(f => true)
                    .Where(p => campaingSettings.ContainsKey(p.CampaignID))// how to filter by mongo?
                    .ToDictionary(p => p.CampaignID, p => p.PhraseIDs);

            var phrasePrices = GetPhrasePrices(campaingPhrases);

            Console.WriteLine("FINISH reading documents");
            
            Console.WriteLine("START document proccessing");
            foreach (var campaingId in campaingSettings.Keys)
            {
                var setting = campaingSettings[campaingId];
                var phrases = campaingPhrases[campaingId];
                var commands = commandBuilder.Build(phrasePrices, phrases, setting);
                documentRepository.Write<CommandDocument>(commands);
            }
            Console.WriteLine("FINISH document proccessing");
        }

        private Dictionary<int, CampaignSettingsDocument> GetCampaignSettings()
        {
            var campaingSettings =
                documentRepository.Read<CampaignSettingsDocument>(s => s.IsActive);
            var campaingSettingsDict = campaingSettings.ToDictionary(s => s.CampaignID);
            return campaingSettingsDict;
        }

        private Dictionary<int, PhrasePriceDocument> GetPhrasePrices(Dictionary<int, int[]> activeCampaingPhraseDict)
        {
            if (!activeCampaingPhraseDict.Any())
                return new Dictionary<int, PhrasePriceDocument>();

            var phraseIDs =
                activeCampaingPhraseDict.Values.Aggregate((f, s) => f.Concat(s).ToArray())
                    .Distinct();
            var phraseIdSet = new HashSet<int>(phraseIDs);

            var phrasePrices = documentRepository.Read<PhrasePriceDocument>(p => true)
                                                 .Where(p => phraseIdSet.Contains(p.PhraseID))
                                                 .ToOldestValueDictionary(p => p.PhraseID);
            return phrasePrices;
        }
    }
}