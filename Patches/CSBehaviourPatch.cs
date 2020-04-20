using System;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using StoryMode;
using StoryMode.Behaviors;
using StoryMode.Behaviors.Quests.FirstPhase;
using StoryMode.Behaviors.Quests.TutorialPhase;
using StoryMode.Behaviors.Quests.ThirdPhase;
using StoryMode.Behaviors.Quests.SecondPhase;
using StoryMode.Behaviors.Quests;

namespace zCulturedStart
{
    [HarmonyPatch(typeof(StoryModeSubModule), "AddBehaviors")]
    class CSBehaviourPatch
    {
        private static void Postfix(CampaignGameStarter campaignGameStarter)
        {
            campaignGameStarter.AddBehavior(new CSBehaviour());
            //Removing FP behavior modification
            //if (CultureStartOptions.FreePlayLoadedOnCondition())
            //{
                //Type fpType = AccessTools.TypeByName("FreePlayCreateKingdomBehavior");
                //if (fpType != null){ 
                   // CampaignBehaviorBase fpBehavior = (CampaignBehaviorBase)Activator.CreateInstance(fpType);                
                
                    //campaignGameStarter.AddBehavior(fpBehavior);
               // }
                //Testing adding back removed behaviors in postfix
                
           // }
        }
    }
}
