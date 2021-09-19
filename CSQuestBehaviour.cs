using System;
using System.Collections.Generic;
using System.Reflection;
using StoryMode;
using StoryMode.Behaviors.Quests.FirstPhase;
using StoryMode.StoryModePhases;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.SandBox;
using TaleWorlds.Core;
using TaleWorlds.Library;
using Helpers;
using TaleWorlds.Localization;
using HarmonyLib;


namespace zCulturedStart
{
    class CSBehaviour : CampaignBehaviorBase
    {
        public override void RegisterEvents()
        {
            CampaignEvents.OnCharacterCreationIsOverEvent.AddNonSerializedListener(this, new Action(this.OnCharacterCreationIsOver));
            CampaignEvents.OnQuestCompletedEvent.AddNonSerializedListener(this, new Action<QuestBase, QuestBase.QuestCompleteDetails>(this.OnQuestCompleted));
            CampaignEvents.OnQuestStartedEvent.AddNonSerializedListener(this, new Action<QuestBase>(this.OnQuestStarted));
            
        }
        public override void SyncData(IDataStore dataStore)
        {
        }
        private readonly Type FpType = AccessTools.TypeByName("FreePlay.FreePlayCreateKingdomBehavior");
        private readonly Type FpStartType = AccessTools.TypeByName("FreePlay.FreePlayGameStartBehavior");
        
        public void OnCharacterCreationIsOver()
        {
                   

        }
        public static Vec2 GetSettlementLoc(Settlement settlement) //This is to get the vector of a HC settlement...Possible Todo Home town
        {
            return settlement.GatePosition;
        }
        private static void SelectClanName()
        {
            
            InformationManager.ShowTextInquiry(new TextInquiryData(new TextObject("{=JJiKk4ow}Select your family name: ", null).ToString(), string.Empty, true, false, GameTexts.FindText("str_done", null).ToString(), null, new Action<string>(OnChangeClanNameDone), null, false, new Func<string, Tuple<bool, string>>(FactionHelper.IsClanNameApplicable), "", ""), false);

        }
        private static bool IsNewClanNameApplicable(string input)
        {
            return input.Length <= 50 && input.Length >= 1;
        }
        private static void OnChangeClanNameDone(string newClanName)
        {
            TextObject textObject = new TextObject(newClanName ?? "", null);
            Clan.PlayerClan.InitializeClan(textObject, textObject, Clan.PlayerClan.Culture, Clan.PlayerClan.Banner);
            OpenBannerSelectionScreen();
        }

        private static void OpenBannerSelectionScreen()
        {
            Game.Current.GameStateManager.PushState(Game.Current.GameStateManager.CreateState<BannerEditorState>(), 0);
        }

        private void OnQuestStarted(QuestBase quest)
        {
            if (quest.StringId == "investigate_neretzes_banner_quest" && CSCharCreationOption.CSGameOption == 1)
            { 
                AccessTools.Method(typeof(QuestBase), "CompleteQuestWithSuccess").Invoke(quest, null);
            } 
            if (quest.StringId == "main_storyline_create_kingdom_quest_1"|| quest.StringId == "main_storyline_create_kingdom_quest_0")
            {
                if (Clan.PlayerClan.Kingdom != null) { 
                    if (Clan.PlayerClan.Kingdom.RulingClan == Clan.PlayerClan){
                        Type type = AccessTools.TypeByName("StoryMode.Behaviors.Quests.FirstPhase.CreateKingdomQuestBehavior+CreateKingdomQuest");
                        JournalLog log = (JournalLog)AccessTools.Field(type, "_clanIndependenceRequirementLog").GetValue(quest);
                        AccessTools.Field(type, "_hasPlayerCreatedKingdom").SetValue(quest, true);
                        Object[] parameters = new object[] { log, 1 };
                        AccessTools.Method(typeof(QuestBase), "UpdateQuestTaskStage").Invoke(quest, parameters);
                    }
                }
            }
        }
        private void OnQuestCompleted(QuestBase quest, QuestBase.QuestCompleteDetails detail)
        {
           
              
        }       

    }
        
}

