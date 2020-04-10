using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Core;
using TaleWorlds.Library;
using System.Reflection;
using StoryMode;
using StoryMode.CharacterCreationSystem;
using HarmonyLib;
using TaleWorlds.Localization;


namespace zCulturedStart
{
    public class CultureStartOptions 
    {
        public static void AddStartOption(CharacterCreation characterCreation)
        {
            //Default option CSDefault
            
            CharacterCreationMenu characterCreationMenu = new CharacterCreationMenu(new TextObject("{=peNBA0WW}Story Background", null), new TextObject("{=jg3T5AyE}Who are you in Caldaria...", null), new CharacterCreationOnInit(BranchsOnInit), CharacterCreationMenu.MenuTypes.MultipleChoice);

            CharacterCreationCategory characterCreationCategory = characterCreationMenu.AddMenuCategory(null);
            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH}A commoner (Default Start)", null), new List<SkillObject> { DefaultSkills.Charm }, 0,0,0,0, null,new CharacterCreationOnSelect(CSDefaultOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv} Setting off with your Father, Mother, Brother and your two younger siblings to a new town you'd heard was safer. But you did not make it", null), null, 0, 0, 0);

            //Merchant Start
            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH}A budding caravanner", null), new List<SkillObject> { DefaultSkills.Trade }, 0, 1, 10, 0, null, new CharacterCreationOnSelect(MerchantOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv}With what savings you could muster you purchased some mules and mercenaries", null), null, 0, 0, 0);

            //Exiled Start
            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH} A noble of " + CharacterCreationContent.Instance.Culture.StringId + " in exile", null), new List<SkillObject> { DefaultSkills.Leadership }, 0, 1, 10, 0, null, new CharacterCreationOnSelect(CSExileOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv}Forced into exile after your parents were executed for suspected treason. With only your family's bodyguard you set off. Should you return you'd be viewed as a criminal.", null), null, 0, 150, 0);

            //Mercanary start            
            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH}In a failing mercenary company", null), new List<SkillObject> { DefaultSkills.Tactics }, 0, 1, 10, 0, null, new CharacterCreationOnSelect(MercenaryOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv}With men deserting over lack of wages, your company leader was found, and you decided to take you chance and lead", null), null, 0, 50, 0);
            
            //Looter start
            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH}A looter lowlife.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 1, 10, 0, null, new CharacterCreationOnSelect(CSLooterOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv}Left impoverished from war, you found a group of like minded ruffians who desperate to get by", null), null, 0, 0, 0);

            characterCreation.AddNewMenu(characterCreationMenu);
        }
        
        private static void CSDefaultOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSSelectOption = 1;
        }        
        private static void MerchantOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSSelectOption = 2;
        }
        private static void CSExileOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSSelectOption = 3;
        }        
        private static void MercenaryOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSSelectOption = 4;
        }
        private static void CSLooterOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSSelectOption = 5;
        }
        
        

        //Not using right now investigate if anything has to happen
        private static void BranchsOnInit(CharacterCreation characterCreation)
        {
            //Init 
            List<FaceGenChar> list = new List<FaceGenChar>();
            BodyProperties bodyProperties = CharacterObject.PlayerCharacter.GetBodyProperties(CharacterObject.PlayerCharacter.Equipment, -1);
            bodyProperties = FaceGen.GetBodyPropertiesWithAge(ref bodyProperties, 23f);
            list.Add(new FaceGenChar(bodyProperties, CharacterObject.PlayerCharacter.Equipment, CharacterObject.PlayerCharacter.IsFemale, "act_childhood_schooled"));
            characterCreation.ChangeFaceGenChars(list);
            //Doing this to add menus late to allow previous selections to work. But have to make sure it's not already added. No good way in menu class so forced to use text
            TextObject backstory;
            TextObject backstory2;           
            backstory = new TextObject("{=jg3T5AyE} Along the way, the inn at which you were staying was attacked by raiders. Your parents were slain and your two youngest siblings seized, but you and your brother survived because...", null);            
            backstory2 = new TextObject("{=jg3T5AyE} During your journies you were ambushed by raiders you survived because...", null);
            
            List<CharacterCreationMenu> CurMenus = (List<CharacterCreationMenu>)AccessTools.Field(typeof(CharacterCreation), "CharacterCreationMenu").GetValue(characterCreation);
            bool loaded = false;
            foreach (CharacterCreationMenu x in CurMenus)
            {
                if (x.Text.ToString() == backstory.ToString() || x.Text.ToString() == backstory2.ToString())
                {
                    loaded = true;
                    break;
                }
            };
            if (!loaded)
            {
                CharacterCreationContent.Instance.GetType().GetMethod("AddEscapeMenu", BindingFlags.NonPublic | BindingFlags.Static).Invoke(CharacterCreationContent.Instance, new object[] { characterCreation });
            }

        }
        public static void AddGameOption(CharacterCreation characterCreation)
        {
            //Adding this menu to choose start option type to leave story options story only
            CharacterCreationMenu characterCreationMenu = new CharacterCreationMenu(new TextObject("{=peNBA0WW}Quest Start Options", null), new TextObject("{=jg3T5AyE}How do you want to handle your Quests", null), new CharacterCreationOnInit(GameOptionOnInit), CharacterCreationMenu.MenuTypes.MultipleChoice);
            //Free Play add this
            CharacterCreationCategory characterCreationCategory = characterCreationMenu.AddMenuCategory(new CharacterCreationOnCondition(FreePlayLoadedOnCondition));
            characterCreationCategory.AddCategoryOption(new TextObject(("{=5vCHolsH} Tutorial Skip (Default Quests)"), null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(FPDefaultOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv} Default start of the game just without Tutorial", null), null, 0, 0, 0);
            characterCreationCategory.AddCategoryOption(new TextObject(("{=5vCHolsH} Neretzes's Folly (Skips First Quest)"), null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(FPSkipOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv} First Non Quest Noble you talk to completes the quest.", null), null, 0, 0, 0);
            characterCreationCategory.AddCategoryOption(new TextObject(("{=5vCHolsH} Sandbox Start *Just Let Me Play*"), null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(FPSandBoxOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv} No Story quests but still able to create New Kingdom", null), null, 0, 0, 0);
            //No Free Play Load this
            CharacterCreationCategory characterCreationCategory2 = characterCreationMenu.AddMenuCategory(new CharacterCreationOnCondition(NotFreePlayLoadedOnCondition));
            characterCreationCategory2.AddCategoryOption(new TextObject(("{=5vCHolsH} Tutorial Skip (Default Quests)"), null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(DefaultOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv} Default start of the game just without Tutorial", null), null, 0, 0, 0);
            characterCreationCategory2.AddCategoryOption(new TextObject(("{=5vCHolsH} Neretzes's Folly Skip (Skips First Quest)"), null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(SkipOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv} First Non Quest Noble you talk to completes the quest.", null), null, 0, 0, 0);
            characterCreationCategory2.AddCategoryOption(new TextObject(("{=5vCHolsH} Sandbox ***NO CUSTOM KINGDOM***"), null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(SandBoxOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv} Download Just Let me Play BEFORE starting your game to Enable Kingdoms. No quests", null), null, 0, 0, 0);
            characterCreation.AddNewMenu(characterCreationMenu);
        }
        private static void GameOptionOnInit(CharacterCreation characterCreation)
        {
            List<CharacterCreationMenu> CurMenus = (List<CharacterCreationMenu>)AccessTools.Field(typeof(CharacterCreation), "CharacterCreationMenu").GetValue(characterCreation);
            bool loaded = false;
            foreach (CharacterCreationMenu x in CurMenus)
            {
                if (x.Text.ToString() == "Who are you in Caldaria...")
                {
                    loaded = true;
                    break;
                }
            };
            if (!loaded)
            {
                AddStartOption(characterCreation);
            }

        }
        private static void FPDefaultOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSGameOption = 0;
        }
        private static void FPSkipOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSGameOption = 1;
        }
        private static void FPSandBoxOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSGameOption = 2;
        }
        private static void DefaultOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSGameOption = 0;
        }
        private static void SkipOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSGameOption = 1;
        }
        private static void SandBoxOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSGameOption = 2;
        }
        public static void AddStartLocation(CharacterCreation characterCreation)
        {
            //Default option CSDefault
            //CSCharCreationOption csOptions;
            CharacterCreationMenu characterCreationMenu = new CharacterCreationMenu(new TextObject("{=peNBA0WW}Starting Location", null), new TextObject("{=jg3T5AyE}Beginning your new adventure", null), new CharacterCreationOnInit(LocationOnInit), CharacterCreationMenu.MenuTypes.MultipleChoice);
            CharacterCreationCategory characterCreationCategory = characterCreationMenu.AddMenuCategory(null);
            characterCreationCategory.AddCategoryOption(new TextObject(("{=5vCHolsH} Near your home in the city where your journey began"), null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(HomeOnConsequnce), new CharacterCreationApplyFinalEffects(HomeOnAction), new TextObject("{=CvfRsafv} Back to where you started", null), null, 0, 0, 0);
            
            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH} In a strange new city (Random).", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(RandStartOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv} Travelling wide and far you arrive at an unknown city", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH} In a caravan to the Aserai city of Qasira.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(QasariOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv} You leave the caravan right at the gates", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH} In a caravan to the Battania city of Dunglanys.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(DunglanysOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv} You leave the caravan right at the gates", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH} On a ship to the Empire city of Zeonica.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(ZeonicaOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv} You leave the ship and arrive right at the games", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH} In a caravan to the Sturgia city of Balgard.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(BalgardOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv}  You leave the caravan right at the gates", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH} In a caravan to the Khuzait city of Ortongard.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(OrtongardOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv}  You leave the caravan right at the gates", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH} In a river boat to the Vlandia city of Pravend.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(PravendOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv} You arrive right at the gates", null), null, 0, 0, 0);
            characterCreation.AddNewMenu(characterCreationMenu);
        }
        private static void CSDoNothingOnApply(CharacterCreation characterCreation)
        {
        }
        private static void HomeOnConsequnce(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSLocationOption = 0; //Home town
        }
        private static void HomeOnAction(CharacterCreation characterCreation)        {
            
        }
        private static void RandStartOnConsequnce(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSLocationOption = 1; //Random
        }
        private static void QasariOnConsequnce(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSLocationOption = 2; //Aserai 
        }
        private static void DunglanysOnConsequnce(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSLocationOption = 3; //Battania 
        }
        private static void ZeonicaOnConsequnce(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSLocationOption = 4; //Imperial 
        }
        private static void BalgardOnConsequnce(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSLocationOption = 5; //Sturgia 
        }
        private static void OrtongardOnConsequnce(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSLocationOption = 6; //Khuzait  
        }
        private static void PravendOnConsequnce(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSLocationOption = 7; //Vlandia   
        }
        
        private static void LocationOnInit(CharacterCreation characterCreation)
        {
            


        }
        
        public static bool FreePlayLoadedOnCondition()
        {
            //Loop to see if FreePlay is loaded
            Type FpCheck = AccessTools.TypeByName("FreePlay.FreePlaySubModule");            
            if (FpCheck != null)
            {
                return true;
            }
            return false;
        }
        public static bool NotFreePlayLoadedOnCondition()
        {
            //Loop to see if FreePlay is loaded
            Type FpCheck = AccessTools.TypeByName("FreePlay.FreePlaySubModule");
            if (FpCheck == null)
            {
                return true;
            }
            return false;
        }
    }

}
