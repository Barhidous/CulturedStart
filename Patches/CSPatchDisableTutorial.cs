using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using System.Reflection;
using StoryMode;
using StoryMode.Behaviors;
using StoryMode.Behaviors.Quests.FirstPhase;
using StoryMode.StoryModePhases;
using StoryMode.CharacterCreationSystem;
using TaleWorlds.Localization;
using HarmonyLib;
using FreePlay;

namespace zCulturedStart
{   [HarmonyPatch(typeof(MainStoryLine), "CompleteTutorialPhase")]
    class CSPatchDisableTutorial
    {
        private static bool Prefix(MainStoryLine __instance, bool isSkipped)
        {
            
            __instance.TutorialPhase.GetType().GetMethod("CompleteTutorial", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(__instance.TutorialPhase, new object[] { isSkipped });
            CSOnStoryModeEnded();
            __instance.GetType().GetProperty("FirstPhase").SetValue(__instance, new FirstPhase());
            if (CSCharCreationOption.CSSelectOption == 4)            {
                //Skip  to create new quests            
                
            }
            else
            {
                
            }
            //new BannerInvestigationQuestBehavior.BannerInvestigationQuest(StoryMode.StoryMode.Current.MainStoryLine.Brother).StartQuest();


            return false;
        }
        private readonly Type _rebuildPlayerClanQuest = typeof(RebuildPlayerClanQuestBehavior).GetNestedType("RebuildPlayerClanQuest", BindingFlags.NonPublic);

        private static void CSOnStoryModeEnded()
        {
            if (CSCharCreationOption.CSGameOption == 2)
            {
                //Skip  to create new quests  
               
                Type InitRebuildClan = typeof(RebuildPlayerClanQuestBehavior).Assembly.GetType("StoryMode.Behaviors.Quests.FirstPhase.RebuildPlayerClanQuestBehavior+RebuildPlayerClanQuest");
                    
                if (InitRebuildClan != null)
                {                  

                    QuestBase ActRebuildClan = (QuestBase)Activator.CreateInstance(InitRebuildClan, new object[] { Hero.MainHero });
                    ActRebuildClan.StartQuest();
                    //QuestBase FreePlaytest = (QuestBase)Activator.CreateInstance(typeof(FreePlayCreateKingdomBehaviour.CreateKingdomFreePlayQuest), new object[] { Hero.MainHero });
                    //FreePlaytest.StartQuest();
                    
                }
            }
            else
            {
                MbEvent mbEvent = (MbEvent)Traverse.Create(StoryModeEvents.Instance).Field("_onStoryModeTutorialEndedEvent").GetValue();
                mbEvent.Invoke();
            }
            
            
        }
        

    }
    
}
