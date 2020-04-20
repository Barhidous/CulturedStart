using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using HarmonyLib;

namespace zCulturedStart.Patches
{
    //[HarmonyPatch(typeof(FirstPhaseCampaignBehavior), "OnSettlementLeft")]
    class CSFreePlayPatch
    {
        private static void Postfix()
        {            
            Vec2 StartPos2 = CulturedStartLocPatch.GetSettlementLoc(CSCharCreationOption.CSOptionSettlement());
            MobileParty.MainParty.Position2D = StartPos2;
            MapState mapstate;
            mapstate = (GameStateManager.Current.ActiveState as MapState);
            mapstate.Handler.TeleportCameraToMainParty();
            CSApplyChoices.ApplyStoryOptions();
        }
    }
}
