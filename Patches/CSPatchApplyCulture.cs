using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using StoryMode;
using Helpers;
using StoryMode.CharacterCreationSystem;
namespace zCulturedStart
{
    [HarmonyPatch(typeof(CharacterCreationContent), "ApplyCulture")]
    class CSPatchApplyCulture
    {
        private static bool Prefix(CharacterCreation characterCreation)
        {
            CharacterObject.PlayerCharacter.Culture = CharacterCreationContent.Instance.Culture;
            Clan.PlayerClan.Culture = CharacterCreationContent.Instance.Culture;
            Settlement settlement = CSCharCreationOption.cultureSettlement(Hero.MainHero);
            Clan.PlayerClan.InitializeHomeSettlement(settlement);
            Hero.MainHero.BornSettlement = Clan.PlayerClan.HomeSettlement;
            
            return false;
        }
    }
}
