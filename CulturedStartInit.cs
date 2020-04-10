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
            

            Harmony harmony = new Harmony("mod.bannerlord.CS");
            harmony.Patch(BannerInvestigationQuest, new HarmonyMethod(postfix));
            harmony.PatchAll();
            if (CultureStartOptions.FreePlayLoadedOnCondition()) { 
                List<InitialStateOption> list = (List <InitialStateOption>)AccessTools.Field(TaleWorlds.MountAndBlade.Module.CurrentModule.GetType(), "_initialStateOptions").GetValue(TaleWorlds.MountAndBlade.Module.CurrentModule);
                InitialStateOption fp = list.Find(x => x.Id == "FreePlayStartGame");
                list.Remove(fp);
            }

        }
        
    }
}

