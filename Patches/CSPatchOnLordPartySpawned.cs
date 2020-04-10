using System;
using System.Collections.Generic;
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

namespace zCulturedStart.Patches
{
    class CSPatchOnLordPartySpawned
    {

        // didnt end up needing this leaving in case I do
        public static bool Prefix(MobileParty spawnedParty, BannerInvestigationQuestBehavior __instance)
        {
            Type t = AccessTools.TypeByName("Storymode.BannerInvestigationQuestBehavior+BannerInvestigationQuest");
            Dictionary<Hero, bool> _noblesToTalk = (Dictionary<Hero, bool>)AccessTools.Field(t, "_noblesToTalk").GetValue(__instance);
            if (spawnedParty.LeaderHero != null && _noblesToTalk.ContainsKey(spawnedParty.LeaderHero) && !_noblesToTalk[spawnedParty.LeaderHero])
            {
                
            }
            return false;            
        }
    }
}
