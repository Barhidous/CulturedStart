using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using System.Reflection;
using StoryMode;
using StoryMode.Behaviors;
using StoryMode.Behaviors.Quests.PlayerClanQuests;
using StoryMode.StoryModePhases;
using HarmonyLib;


namespace zCulturedStart
{   [HarmonyPatch(typeof(MainStoryLine), "CompleteTutorialPhase")]
    class CSPatchDisableTutorial
    {
        private static bool Prefix(MainStoryLine __instance, bool isSkipped)
        {
            TutorialPhase.Instance.CompleteTutorial(true);
            //__instance.TutorialPhase.GetType().GetMethod("CompleteTutorial", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(__instance.TutorialPhase, new object[] { isSkipped });
            CSOnStoryModeEnded();
            __instance.GetType().GetProperty("FirstPhase").SetValue(__instance, new FirstPhase());
            //Combining multiple cleanup/complete logics that do not get called if you dont leave tutorial manually.
            //Object Obj = Campaign.Current.GetCampaignBehavior<TutorialPhaseCampaignBehavior>()
            //CampaignEvents.RemoveListeners(Obj);
            StoryModeEvents.RemoveListeners(Campaign.Current.GetCampaignBehavior<TutorialPhaseCampaignBehavior>());
            //if(StoryMode.StoryModeObjects.StoryModeHeroes. != null ){ 
           //StoryMode.StoryMode.Current.MainStoryLine.Brother.ChangeState(Hero.CharacterStates.Disabled);
            //}
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
                    
                    foreach (MobileParty tracked in MobileParty.All)
                    {
                        Campaign.Current.VisualTrackerManager.RemoveTrackedObject(tracked);
                    }
                   
                }
            }
            else
            {

               // MbEvent mbEvent = (MbEvent)StoryModeEvents.OnStoryModeTutorialEndedEvent;
                //mbEvent.Invoke();
            }
            
            
        }
        

    }
    
}
