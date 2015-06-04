using System.Collections.Generic;
using RoboYaDBL.Documents;

namespace RoboYaDBL.BusinessLogic
{
    class CommandBuilder : ICommandBuilder
    {
        public List<CommandDocument> Build(Dictionary<int, PhrasePriceDocument> phrasePrices, int[] campaingPhraseIDs,
            CampaignSettingsDocument campaignSettings)
        {
            var resultCommands = new List<CommandDocument>();

            foreach (var campaingPhraseId in campaingPhraseIDs)
            {
                var price = phrasePrices[campaingPhraseId];
                var newPrice = Calculate(price, campaignSettings);

                resultCommands.Add(new CommandDocument
                {
                    CampaignID = campaignSettings.CampaignID,
                    PhraseID = campaingPhraseId,
                    Price = newPrice
                });
            }

            return resultCommands;
        }

        private float Calculate(PhrasePriceDocument prices, CampaignSettingsDocument campaignSettings)
        {
            var result = (float)prices.PremiumMin;
            return result ;
        }
    }
}