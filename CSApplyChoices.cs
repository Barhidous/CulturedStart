using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Core;
using StoryMode;
using StoryMode.CharacterCreationSystem;
using TaleWorlds.Localization;

namespace zCulturedStart
{
    class CSApplyChoices
    {
        public static void UpdateNezzyFolly()
        {
            if (CSCharCreationOption.SkipFolly)
            {
                Hero brother = StoryMode.StoryMode.Current.MainStoryLine.Brother;
                brother.ChangeState(Hero.CharacterStates.Dead);
                StoryMode.StoryMode.Current.MainStoryLine.CompleteFirstPhase();
            }
        }
    }
}
