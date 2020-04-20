using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Library;
using HarmonyLib;
using StoryMode;
using StoryMode.Behaviors;
using StoryMode.StoryModePhases;


namespace zCulturedStart
{
    [HarmonyPatch(typeof(TrainingFieldCampaignBehavior), "OnCharacterCreationIsOver")]
    public class CulturedStartLocPatch
              {
        public static Vec2 GetSettlementLoc(Settlement settlement) //This is to get the vector of a HC settlement...Possible Todo Home town
        {
            return settlement.GatePosition;
        }
        
        private static bool Prefix(TrainingFieldCampaignBehavior __instance)
        {
            if (!CultureStartOptions.FreePlayLoadedOnCondition()) { 
                Hero brother = StoryMode.StoryMode.Current.MainStoryLine.Brother;
                PartyBase.MainParty.MemberRoster.RemoveTroop(brother.CharacterObject, 1, default(UniqueTroopDescriptor), 0);
                //Setting Various extra values to try and match usual complete tutorial phase to make sure events fire.
                AccessTools.Field(typeof(TrainingFieldCampaignBehavior), "_talkedWithBrotherForTheFirstTime").SetValue(__instance, true);
                TutorialPhase.Instance.PlayerTalkedWithBrotherForTheFirstTime();
                brother.ChangeState(Hero.CharacterStates.Disabled);
                //Believe this is line missed that is causing all the brother issues
                StoryMode.StoryMode.Current.MainStoryLine.Brother.Clan = CampaignData.NeutralFaction;


                StoryMode.StoryMode.Current.MainStoryLine.CompleteTutorialPhase(true);
                Vec2 StartPos = GetSettlementLoc(CSCharCreationOption.CSOptionSettlement());
                MobileParty.MainParty.Position2D = StartPos;
                MapState mapstate;
                mapstate = (GameStateManager.Current.ActiveState as MapState);
                mapstate.Handler.TeleportCameraToMainParty();
                SelectClanName();
                CSApplyChoices.ApplyStoryOptions();
            }
            return false;
        }
        
        private static void SelectClanName()
        {
            InformationManager.ShowTextInquiry(new TextInquiryData(new TextObject("{=JJiKk4ow}Select your family name: ", null).ToString(), string.Empty, true, false, GameTexts.FindText("str_done", null).ToString(), null, new Action<string>(OnChangeClanNameDone), null, false, new Func<string, bool>(IsNewClanNameApplicable), ""), false);
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
    }
    
   
}
