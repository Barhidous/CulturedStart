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
            CSCharCreationOption csOptions;
            CharacterCreationMenu characterCreationMenu = new CharacterCreationMenu(new TextObject("{=peNBA0WW}Story Background", null), new TextObject("{=jg3T5AyE}Like many families in Calradia, your life was upended by war. Your home was ravaged by the passage of army after army. Eventually, you sold your property and set off .....", null), new CharacterCreationOnInit(BranchsOnInit), CharacterCreationMenu.MenuTypes.MultipleChoice);
            CharacterCreationCategory characterCreationCategory = characterCreationMenu.AddMenuCategory(null);
            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH}with your family.(Default Start)", null), new List<SkillObject> { DefaultSkills.Roguery }, 0,0,0,0, null,new CharacterCreationOnSelect(CSDefaultOnConsequence), new CharacterCreationApplyFinalEffects(CSDefaultOnApply), new TextObject("{=CvfRsafv} With your Father, Mother, Brother and your two younger siblings to a new town you'd heard was safer. But you did not make it", null), null, 0, 0, 0);
            //Skip first quest option CSHistory
            characterCreationCategory.AddCategoryOption(new TextObject("{=5vCHolsH} by yourself and stumbled upon a piece of a strange banner.", null), new List<SkillObject> { DefaultSkills.Roguery }, 0, 0, 0, 0, null, new CharacterCreationOnSelect(CSHistoryOnConsequence), new CharacterCreationApplyFinalEffects(CSHistoryOnApply), new TextObject("{=CvfRsafv} While investigating it you came across a knowledgable historian who identified it for you for small fee, informing you of the rich history of the Neretzes's Folly", null), null, 0, 0, -50);
            characterCreation.AddNewMenu(characterCreationMenu);
        }
        private static void CSDefaultOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.CSWithFamily = true;
            CSCharCreationOption.SkipFolly = false;
        }
        private static void CSDefaultOnApply(CharacterCreation characterCreation)
        {
                       
        }
            
        //use to set flags for alt starts
        private static void CSHistoryOnConsequence(CharacterCreation characterCreation)
        {
            CSCharCreationOption.SkipFolly = true;
            CSCharCreationOption.CSWithFamily = false;
        }
        private static void CSHistoryOnApply(CharacterCreation characterCreation)
        {
           
        }
        //Not using right now investigate if anything has to happen
        private static void BranchsOnInit(CharacterCreation characterCreation)
        {

        }
        
    }

}
