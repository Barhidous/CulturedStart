using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using StoryMode;
using StoryMode.Behaviors;


namespace zCulturedStart.Patches
{
    [HarmonyPatch(typeof(FirstPhaseCampaignBehavior), "OnSettlementLeft")]
    class CSPatchOnSettleLeft
    {
        private static bool Prefix(FirstPhaseCampaignBehavior __instance)
        {
            //Not letting anything happen here, because I remove listeners in CSPatchDisableTutorial
            return false;            
        }
    }
}
