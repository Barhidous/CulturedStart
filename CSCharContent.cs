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
using StoryMode.CharacterCreationSystem;
using TaleWorlds.Localization;


namespace zCulturedStart
{
    public class CultureStartOptions
    {
        public static void AddStartOption(CharacterCreation characterCreation)
        {
            //Default option CSDefault
            //CSCharCreationOption csOptions;
            CharacterCreationMenu characterCreationMenu = new CharacterCreationMenu(new TextObject("{=peNBA0WW}Story Background", null), new TextObject("{=jg3T5AyE}Like many families in Calradia, your life was upended by war. Your home was ravaged by the passage of army after army. Eventually, you sold your property and set off .....", null), new CharacterCreationOnInit(BranchsOnInit), CharacterCreationMenu.MenuTypes.MultipleChoice);
            CharacterCreationCategory characterCreationCategory = characterCreationMenu.AddMenuCategory(null);
            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH}with your family.(Default Start)", null), new List<SkillObject> { DefaultSkills.Roguery }, 0,0,0,0, null,new CharacterCreationOnSelect(CSDefaultOnConsequence), new CharacterCreationApplyFinalEffects(CSDefaultOnApply), new TextObject("{=CvfRsafv} With your Father, Mother, Brother and your two younger siblings to a new town you'd heard was safer. But you did not make it", null), null, 0, 0, 0);
            //Skip first quest option CSHistory
            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH} by yourself and stumbled upon a piece of a strange banner.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(CSHistoryOnConsequence), new CharacterCreationApplyFinalEffects(CSHistoryOnApply), new TextObject("{=CvfRsafv} While investigating it you came across a knowledgable historian who identified it for you for small fee, informing you of the rich history of the Neretzes's Folly. What little he did not know, speaking with any noble would fill in the gaps.", null), null, 0, 0, -50);
            characterCreation.AddNewMenu(characterCreationMenu);
        }
        private static void CSDefaultOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSSelectOption = 1;
        }
        private static void CSDefaultOnApply(CharacterCreation characterCreation)
        {
                       
        }
            
        //use to set flags for alt starts
        private static void CSHistoryOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSSelectOption = 2;
        }
        private static void CSHistoryOnApply(CharacterCreation characterCreation)
        {
           
        }
        //Not using right now investigate if anything has to happen
        private static void BranchsOnInit(CharacterCreation characterCreation)
        {

        }
        public static void AddStartLocation(CharacterCreation characterCreation)
        {
            //Default option CSDefault
            //CSCharCreationOption csOptions;
            CharacterCreationMenu characterCreationMenu = new CharacterCreationMenu(new TextObject("{=peNBA0WW}Starting Location", null), new TextObject("{=jg3T5AyE} After Escaping from the raiders you found yourself...", null), new CharacterCreationOnInit(BranchsOnInit), CharacterCreationMenu.MenuTypes.MultipleChoice);
            CharacterCreationCategory characterCreationCategory = characterCreationMenu.AddMenuCategory(null);
            characterCreationCategory.AddCategoryOption(new TextObject(("{=5vCHolsH} Near your home in the city where your journey began"), null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(HomeOnConsequnce), new CharacterCreationApplyFinalEffects(HomeOnAction), new TextObject("{=CvfRsafv} Back to where you started", null), null, 0, 0, 0);
            
            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH} In a strange new city (Random).", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(RandStartOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv} Travelling wide and far you arrive at an unknown city", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH} In a caravan to the Aserai city of Qasira.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(QasariOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv} You leave the caravan right at the gates", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH} In a caravan to the Battania city of Dunglanys.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(DunglanysOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv} You leave the caravan right at the gates", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH} On a ship to the Empire city of Zeonica.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(CSHistoryOnConsequence), new CharacterCreationApplyFinalEffects(ZeonicaOnConsequnce), new TextObject("{=CvfRsafv} You leave the ship and arrive right at the games", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH} In a Caravan to the Sturgia city of Balgard.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(BalgardOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv}  You leave the caravan right at the gates", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH} In a Caravan to the Khuzait city of Ortongard.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(OrtongardOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv}  You leave the caravan right at the gates", null), null, 0, 0, 0);

            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH} In a River boat to the Vlandia city of Pravend.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(PravendOnConsequnce), new CharacterCreationApplyFinalEffects(CSDoNothingOnApply), new TextObject("{=CvfRsafv} You arrive right at the gates", null), null, 0, 0, 0);
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
        private static void RandStartOnAction(CharacterCreation characterCreation)
        {

        }
    }

}
