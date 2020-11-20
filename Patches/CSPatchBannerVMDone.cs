using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using HarmonyLib;
using TaleWorlds.CampaignSystem;


namespace zCulturedStart.Patches
{
    //This patch is to make kingdom be created after banner is done.
    [HarmonyPatch(typeof(BannerEditorVM), "ExecuteDone")]
    class CSPatchBannerVMDone
    {
        private static bool Prefix(BannerEditorVM __instance)
        {
            Action<bool> OnExit = (Action<bool>)AccessTools.Field(typeof(BannerEditorVM), "OnExit").GetValue(__instance);
            OnExit(false);
            if ((CSCharCreationOption.CSSelectOption==7 || CSCharCreationOption.CSSelectOption == 8) && Clan.PlayerClan.Kingdom == null) { 
                CSApplyChoices.CSCreateKingdom();
            }
            return false;

        }
    }
}
