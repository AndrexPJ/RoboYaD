using System.Collections.Generic;
using RoboYaDBL.Documents;

namespace RoboYaDBL.BusinessLogic
{
    internal interface ICommandBuilder
    {
        List<CommandDocument> Build(Dictionary<int, PhrasePriceDocument> phrasePrices, int[] campaingPhraseIDs,
            CampaignSettingsDocument campaignSettings);
    }
}