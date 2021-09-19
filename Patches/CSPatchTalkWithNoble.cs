using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using StoryMode.Behaviors.Quests.FirstPhase;

using TaleWorlds.Localization;
using HarmonyLib;

namespace zCulturedStart
{
    
    //HarmonyPatch(typeof(BannerInvestigationQuestBehavior).assembly.GetType("BannerInvestigationQuest"), "UpdateAllNoblesDead")]
    class CSTalkWithNoblePatch
    {
        
        
        private static void NoblePatch(BannerInvestigationQuestBehavior __instance)
        {
                     
            
            if (CSCharCreationOption.CSGameOption == 1)
            {
                __instance.GetType().GetField("_allNoblesDead", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(__instance, true);
                
            }
            if (CSCharCreationOption.CSGameOption == 2)
            {
                //CampaignEvents.RemoveListeners(__instance);
            }
        }
   }
}
