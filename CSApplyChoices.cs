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
            if (CSCharCreationOption.SkipFolly)
            {
                Hero brother = StoryMode.StoryMode.Current.MainStoryLine.Brother;
                KillCharacterAction.ApplyByMurder(brother,null,false);
                //brother.ChangeState(Hero.CharacterStates.Dead);
                //string test2 = typeof(CSCharCreationOption).FullName;
                //string test = typeof(BannerInvestigationQuestBehavior).FullName;
                //Type testtype = typeof(CSApplyChoices).Assembly.GetType("CSCharCreationOption");
                //Type BannerInvestigationQuest = typeof(BannerInvestigationQuestBehavior).Assembly.GetType("StoryMode.Behaviors.Quests.FirstPhase.BannerInvestigationQuestBehavior+BannerInvestigationQuest");
                //BannerInvestigationQuest.GetField("_allNoblesDead", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(BannerInvestigationQuest, true);
                //StoryMode.StoryMode.Current.MainStoryLine.CompleteFirstPhase();               
            }
        }
    }
}
