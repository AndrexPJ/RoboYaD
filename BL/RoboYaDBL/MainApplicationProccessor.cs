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
        private readonly double timeIntervalMs;

        public MainApplicationProccessor(IMongoDocumentRepository documentRepository, ICommandBuilder commandBuilder, double timeIntervalMs)
        {
            this.documentRepository = documentRepository;
            this.commandBuilder = commandBuilder;
            this.timeIntervalMs = timeIntervalMs;
        }

        public void Proccess()
        {
            ConsoleTimeLogger.Log("START reading documents");

            var campaingSettings = GetCampaignSettings();
            var campaingPhrases =
                documentRepository.Read<CampaignPhrasesDocument>(f => true)
                    .Where(p => campaingSettings.ContainsKey(p.CampaignID))// how to filter by mongo?
                    .ToDictionary(p => p.CampaignID, p => p.PhraseIDs);


            var bottomDateTime = DateTime.Now.AddMilliseconds(-timeIntervalMs);

            ConsoleTimeLogger.Log(string.Format("READ prices after {0}", bottomDateTime));
            var phrasePrices = GetPhrasePrices(campaingPhrases, bottomDateTime);

            if (!phrasePrices.Any())
            {
                ConsoleTimeLogger.Log("EMPTY prices");
                return;
            }

            ConsoleTimeLogger.Log("FINISH reading documents");

            ConsoleTimeLogger.Log("START document proccessing");
            foreach (var campaingId in campaingSettings.Keys)
            {
                var setting = campaingSettings[campaingId];
                var phrases = campaingPhrases[campaingId];
                var commands = commandBuilder.Build(phrasePrices, phrases, setting);
                documentRepository.Write<CommandDocument>(commands);
            }
            ConsoleTimeLogger.Log("FINISH document proccessing");
        }

        private Dictionary<int, CampaignSettingsDocument> GetCampaignSettings()
        {
            var campaingSettings =
                documentRepository.Read<CampaignSettingsDocument>(s => s.IsActive);
            var campaingSettingsDict = campaingSettings.ToDictionary(s => s.CampaignID);
            return campaingSettingsDict;
        }

        private Dictionary<int, PhrasePriceDocument> GetPhrasePrices(Dictionary<int, int[]> activeCampaingPhraseDict, DateTime dateTime)
        {
            if (!activeCampaingPhraseDict.Any())
                return new Dictionary<int, PhrasePriceDocument>();

            var phraseIDs =
                activeCampaingPhraseDict.Values.Aggregate((f, s) => f.Concat(s).ToArray())
                    .Distinct();
            var phraseIdSet = new HashSet<int>(phraseIDs);
            var phrasePrices = documentRepository.Read<PhrasePriceDocument>(p => p.datetime >= dateTime)
                                                 .Where(p => phraseIdSet.Contains(p.PhraseID))
                                                 .ToOldestValueDictionary(p => p.PhraseID);
            return phrasePrices;
        }
    }
}