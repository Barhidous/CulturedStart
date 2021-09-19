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
using TaleWorlds.ObjectSystem;

namespace zCulturedStart.Patches
{
    [HarmonyPatch(typeof(SandboxCharacterCreationContent), "StartingAgeOnInit")]
    class CSPatchSandboxAddMenus
    {
        //Duplicating for sandbox start
        private static bool Prefix(CharacterCreation characterCreation)
        //private static void Test(CharacterCreation characterCreation)
        {
            //Change here to make addtl cultures first one to load
            int maincultures = 0;
            CSCharCreationOption.CSSandboxToggle = 1;
            foreach (CultureObject cultureObject in MBObjectManager.Instance.GetObjectTypeList<CultureObject>())
            {
                if (cultureObject.IsMainCulture)
                {
                    maincultures++;
                }
            }
            if (maincultures == 6)
            {
                CultureStartOptions.AddStartOption(characterCreation);
                CultureStartOptions.AddStartLocation(characterCreation);
            }
            else
            {
                CultureStartOptions.AddtlCultures(characterCreation);
                CultureStartOptions.AddStartOption(characterCreation);
                CultureStartOptions.AddStartLocation(characterCreation);
            }

            //CultureStartOptions.AddStartOption(characterCreation, CharacterCreationContent.Instance);

            //CultureStartOptions.AddStartLocation(characterCreation);
            return true;

        }
    }
}
