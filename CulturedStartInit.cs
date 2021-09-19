using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using HarmonyLib;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Localization;
using StoryMode.Behaviors.Quests.FirstPhase;
using System.Reflection;
using zCulturedStart.Patches;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Library;
using StoryMode;
using StoryMode.Behaviors;
using StoryMode.StoryModePhases;
using StoryMode.StoryModeObjects;

namespace zCulturedStart
{
    class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

           
            var BannerInvestigationQuest = typeof(BannerInvestigationQuestBehavior).Assembly.GetType("StoryMode.Behaviors.Quests.FirstPhase.BannerInvestigationQuestBehavior+BannerInvestigationQuest").GetMethod("InitializeNotablesToTalkList", BindingFlags.NonPublic | BindingFlags.Instance);            
            var postfix = typeof(CSTalkWithNoblePatch).GetMethod("NoblePatch", BindingFlags.NonPublic | BindingFlags.Static);
            Harmony harmony = new Harmony("mod.bannerlord.CS");
            if (CultureStartOptions.FreePlayLoadedOnCondition()) {                
                var FPStart = AccessTools.Method(AccessTools.TypeByName("FreePlay.FreePlayGameStartBehavior"), "OnCharacterCreationIsOver");
                var FPPostfix = typeof(CSFreePlayPatch).GetMethod("Postfix", BindingFlags.NonPublic | BindingFlags.Static);
                harmony.Patch(FPStart, null, new HarmonyMethod(FPPostfix));
            }
            
            harmony.Patch(BannerInvestigationQuest, new HarmonyMethod(postfix));                    

            harmony.PatchAll();
           

        }
        
    }
}

