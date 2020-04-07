using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.Library;
using TaleWorlds.SaveSystem;
using StoryMode;

namespace zCulturedStart
{
    class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();
            
            Harmony harmony = new Harmony("mod.bannerlord.mipen");
            harmony.PatchAll();

        }
       
    }
}

