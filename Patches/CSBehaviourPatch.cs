using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using StoryMode;



namespace zCulturedStart
{
    [HarmonyPatch(typeof(StoryModeSubModule), "AddBehaviors")]
    class CSBehaviourPatch
    {
        private static void Postfix(CampaignGameStarter campaignGameStarter)
        {
            campaignGameStarter.AddBehavior(new CSBehaviour());
            if (CultureStartOptions.FreePlayLoadedOnCondition())
            {
                Type fpType = AccessTools.TypeByName("FreePlayCreateKingdomBehaviour");
                CampaignBehaviorBase fpBehavior = (CampaignBehaviorBase)Activator.CreateInstance(fpType);                
                
                campaignGameStarter.AddBehavior(fpBehavior);
            }
        }
    }
}
