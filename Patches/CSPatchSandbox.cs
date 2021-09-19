using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using StoryMode;
using StoryMode.CharacterCreationContent;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using TaleWorlds.CampaignSystem;

using TaleWorlds.Library;


namespace zCulturedStart.Patches

{
    [HarmonyPatch(typeof(SandboxCharacterCreationContent), "OnCharacterCreationFinalized")]
    class CSPatchSandbox
    {
        private static bool Prefix()
        {
            CSApplyChoices.ApplyStoryOptions();
            Vec2 StartPos = CulturedStartLocPatch.GetSettlementLoc(CSCharCreationOption.CSOptionSettlement());           
            MobileParty.MainParty.Position2D = StartPos;
            return true;
        }
    }
}
