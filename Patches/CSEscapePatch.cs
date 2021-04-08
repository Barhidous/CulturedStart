using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;
using StoryMode;
using StoryMode.CharacterCreationContent;
using TaleWorlds.CampaignSystem.CharacterCreationContent;
using StoryMode.StoryModeObjects;
using TaleWorlds.Localization;
using HarmonyLib;

namespace zCulturedStart
{
    [HarmonyPatch(typeof(StoryModeCharacterCreationContent), "EscapeOnInit")]
    class CSEscapePatch
    {
        private static bool Prefix(CharacterCreation characterCreation)
        {
            //Major change due to change in escape, i'm just patching on this function not modifying the menu anymore. Will show brother/siblings and shit but \o/
            List<CharacterCreationMenu> CurMenus = (List<CharacterCreationMenu>)AccessTools.Field(typeof(CharacterCreation), "CharacterCreationMenu").GetValue(characterCreation);
            bool loaded = false;
            foreach (CharacterCreationMenu x in CurMenus)
            {
                if (x.Text.ToString() == "Beginning your new adventure")
                {
                    loaded = true;
                    break;
                }
            };
            if (!loaded)
            {
                CultureStartOptions.AddStartLocation(characterCreation);
            }

            return true;
        }
    }
      
}
