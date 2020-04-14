using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using HarmonyLib;
using TaleWorlds.CampaignSystem;

namespace zCulturedStart
{
    [HarmonyPatch(typeof(BannerEditorVM), "SetClanRelatedRules")]
    class CSPatchColorCreator
    {                 
        static bool Prefix(BannerEditorVM __instance, ref bool canChangeBackgroundColor)
        {
            if(Clan.PlayerClan.Kingdom != null && Clan.PlayerClan.Kingdom.Leader == Hero.MainHero) { 
                canChangeBackgroundColor = true;
            }
            return true;
        }
        
    }
}
