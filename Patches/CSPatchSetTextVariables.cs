using System;
using System.Linq;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Localization;
using TaleWorlds.Core;
using System.Collections.Generic;


namespace zCulturedStart.Patches
{
    //Mostly pointless patch but doing to get rid of needless text cause devs didnt bother to wrap skills in a not null
    //Did
    //[HarmonyPatch(typeof(CharacterCreationOption), "SetTextVariables")]
    //class CSPatchSetTextVariables
   // {
       // private static void Postfix(CharacterCreationOption __instance, TextObject __result)
        //{
          //  TextObject textObject = new TextObject("FooBar");
          //  __result = textObject;            
       // }
   // }
}
