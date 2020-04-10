﻿using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using System.Reflection;
using StoryMode;
using StoryMode.Behaviors;
using StoryMode.Behaviors.Quests.FirstPhase;
using StoryMode.StoryModePhases;
using HarmonyLib;


namespace zCulturedStart
{   [HarmonyPatch(typeof(MainStoryLine), "CompleteTutorialPhase")]
    class CSPatchDisableTutorial
    {
        private static bool Prefix(MainStoryLine __instance, bool isSkipped)
        {
            
            __instance.TutorialPhase.GetType().GetMethod("CompleteTutorial", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(__instance.TutorialPhase, new object[] { isSkipped });
            CSOnStoryModeEnded();
            __instance.GetType().GetProperty("FirstPhase").SetValue(__instance, new FirstPhase());
            


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
                    //Type t = AccessTools.TypeByName("Storymode.BannerInvestigationQuestBehavior+BannerInvestigationQuest");
                    //Type t = (Type)Traverse.CreateWithType("StoryMode.Behaviors.Quests.FirstPhase.RebuildPlayerClanQuestBehavior+RebuildPlayerClanQuest").Type()
                    //CampaignEvents.
                    //CampaignEvents.RemoveListeners(t);

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
