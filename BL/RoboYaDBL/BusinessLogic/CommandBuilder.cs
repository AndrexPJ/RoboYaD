using System.Collections.Generic;
using System.Linq;
using RoboYaDBL.Documents;

namespace RoboYaDBL.BusinessLogic
{
    class CommandBuilder : ICommandBuilder
    {
        public List<CommandDocument> Build(Dictionary<int, PhrasePriceDocument> phrasePrices, int[] campaingPhraseIDs,
            CampaignSettingsDocument campaignSettings)
        {
            return campaingPhraseIDs.Where(phrasePrices.ContainsKey)
                .Select(phraseId => new CommandDocument
                {
                    CampaignID = campaignSettings.CampaignID,
                    PhraseID = phraseId,
                    Price = Calculate(phrasePrices[phraseId], campaignSettings)
                })
                .ToList();
        }

        private static double Calculate(PhrasePriceDocument prices, CampaignSettingsDocument campaignSettings)
        {
            var result = prices.PremiumMin;
            return result ;
        }
    }
}