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
using StoryMode.CharacterCreationContent;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
namespace zCulturedStart
{
    //[HarmonyPatch(typeof(CharacterCreationContentBase), "ApplyCulture")]
   // {
    //Is Fucked, dont think i actually need this anymore. 
       // private static bool Prefix(CharacterCreation characterCreation)
      //  {
         //   CharacterObject.PlayerCharacter.Culture = CharacterCreationContentBase.Instance.GetSelectedCulture();
          //  Clan.PlayerClan.Culture = CharacterCreationContentBase.Instance.GetSelectedCulture();
          //  Settlement settlement = CSCharCreationOption.cultureSettlement(Hero.MainHero);
          //  Clan.PlayerClan.UpdateHomeSettlement(settlement);
          //  Hero.MainHero.BornSettlement = Clan.PlayerClan.HomeSettlement;
          //  CharacterCreationContentBase.Instance.
          //  return false;
        //}
  //  }
}
