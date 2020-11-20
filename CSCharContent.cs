using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;
using TaleWorlds.Library;
using System.Reflection;
using StoryMode;
using StoryMode.CharacterCreationSystem;
using HarmonyLib;
using TaleWorlds.Localization;
using TaleWorlds.Engine.Screens;


namespace zCulturedStart
{
    public class CultureStartOptions 
    {
        public static void AddStartOption(CharacterCreation characterCreation)
        {
            //Default option CSDefault
            
            CharacterCreationMenu characterCreationMenu = new CharacterCreationMenu(new TextObject("{=1eNBA0WW}Story Background", null), new TextObject("{=5g3T5AyE}Who are you in Caldaria...", null), new CharacterCreationOnInit(BranchsOnInit), CharacterCreationMenu.MenuTypes.MultipleChoice);

            CharacterCreationCategory characterCreationCategory = characterCreationMenu.AddMenuCategory(null);
            characterCreationCategory.AddCategoryOption(new TextObject("{=12CHolsH}A commoner (Default Start)", null), new List<SkillObject> { DefaultSkills.Charm }, 0,0,0,0, null,new CharacterCreationOnSelect(CSDefaultOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS3g3T5AyE} Setting off with your Father, Mother, Brother and your two younger siblings to a new town you'd heard was safer. But you did not make it", null), null, 0, 0, 0);

            //Merchant Start
            characterCreationCategory.AddCategoryOption(new TextObject("{=13CHolsH}A budding caravanner", null), new List<SkillObject> { DefaultSkills.Trade }, 0, 1, 10, 0, null, new CharacterCreationOnSelect(MerchantOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS4g3T5AyE}With what savings you could muster you purchased some mules and mercenaries", null), null, 0, 0, 0);

            //Exiled Start
            characterCreationCategory.AddCategoryOption(new TextObject("{=14CHolsH} A noble of " + CSCharCreationOption.SelectedCulture.StringId + " in exile", null), new List<SkillObject> { DefaultSkills.Leadership }, 0, 1, 10, 0, null, new CharacterCreationOnSelect(CSExileOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS5g3T5AyE}Forced into exile after your parents were executed for suspected treason. With only your family's bodyguard you set off. Should you return you'd be viewed as a criminal.", null), null, 0, 150, 0);

            //Mercanary start            
            characterCreationCategory.AddCategoryOption(new TextObject("{=15CHolsH}In a failing mercenary company", null), new List<SkillObject> { DefaultSkills.Tactics }, 0, 1, 10, 0, null, new CharacterCreationOnSelect(MercenaryOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS6g3T5AyE}With men deserting over lack of wages, your company leader was found, and you decided to take you chance and lead", null), null, 0, 50, 0);
            
            //Looter start
            characterCreationCategory.AddCategoryOption(new TextObject("{=16CHolsH}A looter lowlife.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 1, 10, 0, null, new CharacterCreationOnSelect(CSLooterOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS7g3T5AyE}Left impoverished from war, you found a group of like minded ruffians who desperate to get by", null), null, 0, 0, 0);

            //Vassal Start
            characterCreationCategory.AddCategoryOption(new TextObject("{=17CHolsH} A new vassal of " + CSCharCreationOption.SelectedCulture.StringId, null), new List<SkillObject> { DefaultSkills.Steward }, 0, 1, 10, 0, null, new CharacterCreationOnSelect(CSVassalOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS8g3T5AyE}A young noble who came into an arrangement with the king for a chance at land", null), null, 0, 150, 0);


            //Kingdom Start
            TextObject shortdesc = new TextObject("", null);
            
            TextObject fulldesc = new TextObject("{=CSd1g3T5AyE}With the support of companions you have gathered an army. With limited funds and food you decided it's time for action.", null);

            switch (CSCharCreationOption.SelectedCulture.StringId)
            {
                case "sturgia":
                    shortdesc = new TextObject("{=f6CSm3sP}Leading a Viking Expedition", null);       
                    
                    break;
                case "aserai":
                    shortdesc = new TextObject("{=f5CSm3sP}Leading a Jihad", null);
                    break;
                case "vlandia":
                    shortdesc = new TextObject("{=f4CSm3sP}Leading a Crusade", null);
                    break;
                case "battania":
                    shortdesc = new TextObject("{=f3CSm3sP}Leading a Raiding Expedition", null);
                    break;
                case "khuzait":
                    shortdesc = new TextObject("{=f1CSm3sP}Becoming a Great Khan", null);
                    break;
                case "empire":
                    shortdesc = new TextObject("{=f2CSm3sP}Becoming a new Empire State", null);
                    break;
                default:
                    shortdesc = new TextObject("{=f2CSm3sP}Leading part of " + CSCharCreationOption.SelectedCulture.StringId, null);
                    break;
            }
            
            characterCreationCategory.AddCategoryOption(shortdesc, new List<SkillObject> { DefaultSkills.Leadership, DefaultSkills.Steward}, CharacterAttributesEnum.Social, 1, 15, 1, null, new CharacterCreationOnSelect(CSKingdomOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), fulldesc, null, 0, 351, 0);

            //Holding Start
            TextObject Holdshortdesc = new TextObject("{=f10CSm3sP}You acquired a castle", null);
            characterCreationCategory.AddCategoryOption(Holdshortdesc, new List<SkillObject> { DefaultSkills.Leadership, DefaultSkills.Steward }, CharacterAttributesEnum.Social, 1, 15, 1, null, new CharacterCreationOnSelect(CSHoldingOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS8g3T5AyE}You acquired a castle through your own means and declared yourself a kingdom for better or worse.", null), null, 0, 351, 0);

            //Vassal castle start
            characterCreationCategory.AddCategoryOption(new TextObject("{=17CHolsH} A landed vassal of " + CSCharCreationOption.SelectedCulture.StringId, null), new List<SkillObject> { DefaultSkills.Steward }, 0, 1, 10, 0, null, new CharacterCreationOnSelect(CSCastleVassalOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS9g3T5AyE}A young noble who came into an arrangement with the king for land", null), null, 0, 150, 0);

            //Escape Prisoner start
            characterCreationCategory.AddCategoryOption(new TextObject("{=18CHolsH} An escaped prsioner of a lord of " + CSCharCreationOption.SelectedCulture.StringId, null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 1, 10, 0, null, new CharacterCreationOnSelect(CSEscapeOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS10g3T5AyE}A poor prisoner of petty crimes who managed to break his shackles with a rock and fled", null), null, 0, 0, 0);

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
        private static void CSVassalOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSSelectOption = 6;
        }
        private static void CSKingdomOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSSelectOption = 7;
        }
        private static void CSHoldingOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSSelectOption = 8;
        }
        private static void CSCastleVassalOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSSelectOption = 9;
        }
        private static void CSEscapeOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSSelectOption = 10;
        }

        
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
            backstory = new TextObject("{=jg3T5AyE}Like many families in Calradia, your life was upended by war. Your home was ravaged by the passage of army after army. Eventually, you sold your property and set off with your father, mother, brother, and your two younger siblings to a new town you'd heard was safer. But you did not make it. Along the way, the inn at which you were staying was attacked by raiders. Your parents were slain and your two youngest siblings seized, but you and your brother survived because...", null);            
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
            CharacterCreationMenu characterCreationMenu = new CharacterCreationMenu(new TextObject("{=peNBA0WW}Quest Start Options", null), new TextObject("{=1g3T5AyE}How do you want to handle your Quests", null), new CharacterCreationOnInit(GameOptionOnInit), CharacterCreationMenu.MenuTypes.MultipleChoice);
            //Free Play add this
            CharacterCreationCategory characterCreationCategory = characterCreationMenu.AddMenuCategory(new CharacterCreationOnCondition(FreePlayLoadedOnCondition));
            //characterCreationCategory.AddCategoryOption(new TextObject(("{=5vCHolsH} Tutorial Skip (Default Quests)"), null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(FPDefaultOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv} Default start of the game just without Tutorial", null), null, 0, 0, 0);
            //characterCreationCategory.AddCategoryOption(new TextObject(("{=5vCHolsH} Neretzes's Folly (Skips First Quest)"), null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(FPSkipOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv} First Non Quest Noble you talk to completes the quest.", null), null, 0, 0, 0);
            characterCreationCategory.AddCategoryOption(new TextObject(("{=1vCHolsH} Sandbox Start *Just Let Me Play*"), null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(FPSandBoxOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS11g3T5AyE} No Story quests but still able to create New Kingdom", null), null, 0, 0, 0);
            //No Free Play Load this
            CharacterCreationCategory characterCreationCategory2 = characterCreationMenu.AddMenuCategory(new CharacterCreationOnCondition(NotFreePlayLoadedOnCondition));
            characterCreationCategory2.AddCategoryOption(new TextObject(("{=5vCHolsH} Tutorial Skip (Default Quests)"), null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(DefaultOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS12g3T5AyE} Default start of the game just without Tutorial", null), null, 0, 0, 0);
            characterCreationCategory2.AddCategoryOption(new TextObject(("{=2vCHolsH} Neretzes's Folly Skip (Skips First Quest)"), null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(SkipOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS13g3T5AyE} First Non Quest Noble you talk to completes the quest.", null), null, 0, 0, 0);
            characterCreationCategory2.AddCategoryOption(new TextObject(("{=3vCHolsH} Sandbox"), null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(SandBoxOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS14g3T5AyE} No Quests, now with kingdoms thanks to ability to create your own from base game", null), null, 0, 0, 0);
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
        public static void AddtlCultures(CharacterCreation characterCreation)
        {
            List<string> gamecultures = new List<string>() { "sturgia", "aserai","vlandia","battania","khuzait","empire"};
            CharacterCreationMenu characterCreationMenu = new CharacterCreationMenu(new TextObject("{=CS1eA0WW}Mod Cultures", null), new TextObject("{=5g3T5AyE}Additional Cultures From Mods", null), new CharacterCreationOnInit(AddtlCulturesOnInit), CharacterCreationMenu.MenuTypes.MultipleChoice);
            CharacterCreationCategory characterCreationCategory = characterCreationMenu.AddMenuCategory(null);
            List<CultureObject> AddtlCulturesList = new List<CultureObject>();
            foreach (CultureObject cultureObject in MBObjectManager.Instance.GetObjectTypeList<CultureObject>())
            {
                if (cultureObject.IsMainCulture)
                {
                    if (!gamecultures.Contains(cultureObject.StringId))
                    {
                        characterCreationCategory.AddCategoryOption(cultureObject.Name, new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(CSAddtCultureOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), cultureObject.EncyclopediaText, null, 0, 0, 0);
                        AddtlCulturesList.Add(cultureObject);
                    }
                }
            }
            characterCreationCategory.AddCategoryOption(new TextObject("{=CS13HolsH}None"), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(CSAddtDonothingOnConsequence), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS14HolsH}Will use original selection"), null, 0, 0, 0);
            characterCreation.AddNewMenu(characterCreationMenu);
            CSCharCreationOption.AddtlCulturesList = AddtlCulturesList;                       
        }

        private static void CSAddtCultureOnConsequence(CharacterCreation characterCreation)
        {
            int test2 = 0;
            List<CharacterCreationMenu> CurMenus = (List<CharacterCreationMenu>)AccessTools.Field(typeof(CharacterCreation), "CharacterCreationMenu").GetValue(characterCreation);
            foreach (CharacterCreationMenu x in CurMenus)
            {
                if (x.Text.ToString() == "Additional Cultures From Mods")
                {
                    test2 = x.SelectedOptions.First();
                    test2 = test2 - 1;//First returns base 1                    
                    break;
                }
            };           
            
            CSCharCreationOption.SelectedCulture = CSCharCreationOption.AddtlCulturesList[(test2)];
        }
        private static void CSAddtDonothingOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.SelectedCulture = CharacterCreationContent.Instance.Culture;
        }
        private static void AddtlCulturesOnInit(CharacterCreation characterCreation)
        {
            //List<CharacterCreationMenu> CurMenus = (List<CharacterCreationMenu>)AccessTools.Field(typeof(CharacterCreation), "CharacterCreationMenu").GetValue(characterCreation);
            //bool loaded = false;
            CSCharCreationOption.SelectedCulture = CharacterCreationContent.Instance.Culture;
            //foreach (CharacterCreationMenu x in CurMenus)
            //{
            //    if (x.Text.ToString() == "How do you want to handle your Quests")
            //    {
            //        loaded = true;
            //        break;
            //    }
            //};
            //if (!loaded)
            //{
            //    AddStartOption(characterCreation);
            //}
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
            CharacterCreationMenu characterCreationMenu = new CharacterCreationMenu(new TextObject("{=2eNBA0WW}Starting Location", null), new TextObject("{=2g3T5AyE}Beginning your new adventure", null), new CharacterCreationOnInit(LocationOnInit), CharacterCreationMenu.MenuTypes.MultipleChoice);
            CharacterCreationCategory characterCreationCategory = characterCreationMenu.AddMenuCategory(null);
            characterCreationCategory.AddCategoryOption(new TextObject(("{=20CHolsH} At your castle"), null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, new CharacterCreationOnCondition(AtCastleOnCondition), new CharacterCreationOnSelect(AtCastleOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS15g3T5AyE} At your newly aquired castle", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject(("{=21CHolsH} Escaping from your captor"), null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, new CharacterCreationOnCondition(EscapingOnCondition), new CharacterCreationOnSelect(EscapingOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS16g3T5AyE} Having just escaped", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject(("{=18CHolsH} Near your home in the city where your journey began"), null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(HomeOnConsequnce), new CharacterCreationApplyFinalEffects(HomeOnAction), new TextObject("{=CS17g3T5AyE} Back to where you started", null), null, 0, 0, 0);

            

            characterCreationCategory.AddCategoryOption(new TextObject("{=6vCHolsH} In a strange new city (Random).", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(RandStartOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS18g3T5AyE} Travelling wide and far you arrive at an unknown city", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject("{=7vCHolsH} In a caravan to the Aserai city of Qasira.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(QasariOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS19g3T5AyE} You leave the caravan right at the gates", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject("{=8vCHolsH} In a caravan to the Battania city of Dunglanys.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(DunglanysOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS20g3T5AyE} You leave the caravan right at the gates", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject("{=9vCHolsH} On a ship to the Empire city of Zeonica.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(ZeonicaOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS21g3T5AyE} You leave the ship and arrive right at the games", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject("{=0vCHolsH} In a caravan to the Sturgia city of Balgard.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(BalgardOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS22g3T5AyE}  You leave the caravan right at the gates", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject("{=4vCHolsH} In a caravan to the Khuzait city of Ortongard.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(OrtongardOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS23g3T5AyE}  You leave the caravan right at the gates", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject("{=11CHolsH} In a river boat to the Vlandia city of Pravend.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(PravendOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CS24g3T5AyE} You arrive right at the gates", null), null, 0, 0, 0);


            characterCreation.AddNewMenu(characterCreationMenu);
        }
        private static void CSDoNothingOnApply(CharacterCreation characterCreation)
        {
        }
        private static void HomeOnConsequnce(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSLocationOption = 0; //Home town
        }
        private static void HomeOnAction(CharacterCreation characterCreation)        
        {
            
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

        private static void AtCastleOnConsequnce(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSLocationOption = 8; //Castle   
        }
        private static void EscapingOnConsequnce(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSLocationOption = 9; //escape
            
        }
        private static void LocationOnInit(CharacterCreation characterCreation)
        {
            


        }
        private static bool EscapingOnCondition()
        {
            if (CSCharCreationOption.CSSelectOption == 10)
            {
                return true;
            }
            return false;
        }
        private static bool AtCastleOnCondition()
        {
            if(CSCharCreationOption.CSSelectOption == 8 || CSCharCreationOption.CSSelectOption == 9)
            {
                return true;
            }
            return false;
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
