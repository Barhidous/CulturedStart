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

namespace zCulturedStart
{
    
    [HarmonyPatch(typeof(CharacterCreationContent), "AddMenus")]
    class CSCharCreationPatch
    {
        private static bool Prefix(CharacterCreation characterCreation, CharacterCreationContent __instance)
        //private static void Test(CharacterCreation characterCreation)
        {
            
            CharacterCreationContent.AddParentsMenu(characterCreation);
            CharacterCreationContent.AddChildhoodMenu(characterCreation);
            CharacterCreationContent.AddEducationMenu(characterCreation);
            CharacterCreationContent.AddYouthMenu(characterCreation);
            CharacterCreationContent.AddAdulthoodMenu(characterCreation);
            CultureStartOptions.AddStartOption(characterCreation);
            CharacterCreationContent.Instance.GetType().GetMethod("AddEscapeMenu", BindingFlags.NonPublic | BindingFlags.Static).Invoke(CharacterCreationContent.Instance, new object[] {characterCreation});
            //CultureStartOptions.AddStartOption2(characterCreation);
            return false;

        }
    }
}
