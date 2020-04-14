using System;
using System.Reflection;
using StoryMode;
using StoryMode.Behaviors.Quests.FirstPhase;
using StoryMode.StoryModePhases;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;
using FreePlay;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using HarmonyLib;


namespace zCulturedStart
{
    class CSBehaviour : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            StoryModeEvents.OnCharacterCreationIsOverEvent.AddNonSerializedListener(this, new Action(this.OnCharacterCreationIsOver));
            CampaignEvents.OnQuestCompletedEvent.AddNonSerializedListener(this, new Action<QuestBase, QuestBase.QuestCompleteDetails>(this.OnQuestCompleted));
            CampaignEvents.OnQuestStartedEvent.AddNonSerializedListener(this, new Action<QuestBase>(this.OnQuestStarted));
        }
        public override void SyncData(IDataStore dataStore)
        {
        }
        private readonly Type FpType = AccessTools.TypeByName("FreePlay.FreePlayCreateKingdomBehaviour");
        public void OnCharacterCreationIsOver()
        {
            if(CultureStartOptions.FreePlayLoadedOnCondition() && CSCharCreationOption.CSGameOption != 2){ 
            CampaignBehaviorBase FPBeh = (CampaignBehaviorBase)AccessTools.Method(typeof(StoryMode.CampaignStoryMode), nameof(CampaignStoryMode.GetCampaignBehavior)).MakeGenericMethod(this.FpType).Invoke(Campaign.Current, null);            
            CampaignEvents.RemoveListeners(FPBeh);
            }
        }
        private void OnQuestStarted(QuestBase quest)
        {
            if (quest.StringId == "investigate_neretzes_banner_quest" && CSCharCreationOption.CSGameOption == 1)
            { 
                AccessTools.Method(typeof(QuestBase), "CompleteQuestWithSuccess").Invoke(quest, null);
            }
        }
        private void OnQuestCompleted(QuestBase quest, QuestBase.QuestCompleteDetails detail)
        {
            if (quest.StringId == "rebuild_player_clan_storymode_quest")
            {
                if (CultureStartOptions.FreePlayLoadedOnCondition() && Campaign.Current.QuestManager.Quests.Count < 2)
                {
                    Type dynamicFP = AccessTools.TypeByName("FreePlay.FreePlayCreateKingdomBehaviour").GetNestedType("CreateKingdomFreePlayQuest");
                   
                    QuestBase FreePlayKingdom = (QuestBase)Activator.CreateInstance(dynamicFP, new object[] { Hero.MainHero });
                    FreePlayKingdom.StartQuest();

                }
                else
                {
                    
                    foreach (QuestBase quest1 in Campaign.Current.QuestManager.Quests)
                    {
                        if (quest1.StringId == "free_play_create_kingdom_quest")
                        { 
                        quest1.CompleteQuestWithCancel();
                            break;
                        }
                    }
                }
               
            }
        }
        
    }
        
}

