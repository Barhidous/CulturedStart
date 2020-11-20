using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using TaleWorlds.Library;
using HarmonyLib;
using StoryMode;
using StoryMode.CharacterCreationSystem;
using TaleWorlds.CampaignSystem;
using TaleWorlds.ObjectSystem;

namespace zCulturedStart
{
    
    [HarmonyPatch(typeof(CharacterCreationContent), "AddMenus")]
    class CSCharCreationPatch
    {
        private static bool Prefix(CharacterCreation characterCreation)
        //private static void Test(CharacterCreation characterCreation)
        {
            //Change here to make addtl cultures first one to load
            int maincultures = 0; 
            foreach (CultureObject cultureObject in MBObjectManager.Instance.GetObjectTypeList<CultureObject>())
            {
                if (cultureObject.IsMainCulture)
                {
                    maincultures++;
                }
            }
            if (maincultures == 6) { 
                CultureStartOptions.AddGameOption(characterCreation);
            }
            else
            {
                CultureStartOptions.AddtlCultures(characterCreation);
                CultureStartOptions.AddGameOption(characterCreation);
            }
            CharacterCreationContent.AddParentsMenu(characterCreation);
            CharacterCreationContent.AddChildhoodMenu(characterCreation);
            CharacterCreationContent.AddEducationMenu(characterCreation);
            CharacterCreationContent.AddYouthMenu(characterCreation);
            CharacterCreationContent.AddAdulthoodMenu(characterCreation);
            //CultureStartOptions.AddStartOption(characterCreation, CharacterCreationContent.Instance);
            
            //CultureStartOptions.AddStartLocation(characterCreation);
            return false;

        }
    }
}
