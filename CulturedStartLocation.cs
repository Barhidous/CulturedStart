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
using StoryMode.Behaviors;


namespace zCulturedStart
{
    [HarmonyPatch(typeof(TrainingFieldCampaignBehavior), "OnCharacterCreationIsOver")]
    public class CulturedStartLocPatch
              {
        public static Vec2 GetSettlementLoc() //This is to get the vector of a HC settlement...Possible Todo Home town
        {
            Vec2 sresult;
            Settlement settlement;
            string sCulture = Hero.MainHero.MapFaction.Culture.StringId;
            switch (sCulture)
            {
                case "sturgia":
                    settlement = Settlement.Find("town_S2");
                    sresult = settlement.GatePosition;
                    break;
                case "aserai":
                    settlement = Settlement.Find("town_A8");
                    sresult = settlement.GatePosition;
                    break;
                case "vlandia":
                    settlement = Settlement.Find("town_V3");
                    sresult = settlement.GatePosition;
                    break;
                case "battania":
                    settlement = Settlement.Find("town_B2");
                    sresult = settlement.GatePosition;
                    break;
                case "khuzait":
                    settlement = Settlement.Find("town_K4");
                    sresult = settlement.GatePosition;
                    break;
                default:                    
                    settlement = Settlement.Find("tutorial_training_field");
                    sresult = settlement.GatePosition;
                    break;
                    
            }
            return sresult;
        }
        private static bool Prefix(CampaignBehaviorBase __instance)
        {
            // Overwriting entire base options this is default patch. Todo make generic based on conditional patch
            Hero brother = StoryMode.StoryMode.Current.MainStoryLine.Brother;
            PartyBase.MainParty.MemberRoster.RemoveTroop(brother.CharacterObject, 1, default(UniqueTroopDescriptor), 0);
            StoryMode.StoryMode.Current.MainStoryLine.CompleteTutorialPhase(true);
            Vec2 StartPos = GetSettlementLoc();
            MobileParty.MainParty.Position2D = StartPos;
            MapState mapstate;
            mapstate = (GameStateManager.Current.ActiveState as MapState);
            mapstate.Handler.TeleportCameraToMainParty();
            SelectClanName();
            CSApplyChoices.UpdateNezzyFolly();
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
