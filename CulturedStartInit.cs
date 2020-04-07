using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem;
using StoryMode.Behaviors.Quests.FirstPhase;
using System.Reflection;
using StoryMode;

namespace zCulturedStart
{
    class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            //Type test = typeof(BannerInvestigationQuestBehavior).Assembly.GetType("BannerInvestigationQuest");
            //Type test2 = typeof(BannerInvestigationQuestBehavior).Assembly.GetType("StoryMode.Behaviors.Quests.FirstPhase.BannerInvestigationQuestBehavior+BannerInvestigationQuest");
            // Type test3 = typeof(BannerInvestigationQuestBehavior).GetType("BannerInvestigationQuest");
            //typeof(BannerInvestigationQuestBehavior).Declar
            //for typeof(BannerInvestigationQuestBehavior).GetType("")
            var BannerInvestigationQuest = typeof(BannerInvestigationQuestBehavior).Assembly.GetType("StoryMode.Behaviors.Quests.FirstPhase.BannerInvestigationQuestBehavior+BannerInvestigationQuest").GetMethod("InitializeNotablesToTalkList", BindingFlags.NonPublic | BindingFlags.Instance);
            var postfix = typeof(CSTalkWithNoblePatch).GetMethod("NoblePatch", BindingFlags.NonPublic | BindingFlags.Static);


            Harmony harmony = new Harmony("mod.bannerlord.mipen");
            harmony.Patch(BannerInvestigationQuest, new HarmonyMethod(postfix));
            harmony.PatchAll();
            
            

        }
       
    }
}

