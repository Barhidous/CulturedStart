using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Core;
using System.Reflection;
using StoryMode;
using StoryMode.Behaviors.Quests.FirstPhase;
using StoryMode.CharacterCreationSystem;
using TaleWorlds.Localization;

namespace zCulturedStart
{
    class CSApplyChoices : CSCharCreationOption
    {
        public static void UpdateNezzyFolly()
        {
            if (CSCharCreationOption.CSSelectOption == 2)
            {
                //Hero brother = StoryMode.StoryMode.Current.MainStoryLine.Brother;
                //KillCharacterAction.ApplyByMurder(brother,null,false);
                //brother.ChangeState(Hero.CharacterStates.Dead);
                            
            }
        }
    }
}
