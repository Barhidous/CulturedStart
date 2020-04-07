using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using StoryMode;
using StoryMode.CharacterCreationSystem;
using TaleWorlds.Localization;
using HarmonyLib;

namespace zCulturedStart
{
    [HarmonyPatch(typeof(CharacterCreationContent), "AddEscapeMenu")]
    class CSEscapePatch
    {private static bool Prefix(CharacterCreation characterCreation)
        {
            TextObject backstory;
            if (CSCharCreationOption.CSWithFamily)
            {
                backstory = new TextObject("{=jg3T5AyE} Along the way, the inn at which you were staying was attacked by raiders. Your parents were slain and your two youngest siblings seized, but you and your brother survived because...", null);
            }
            else
            {
                backstory = new TextObject("{=jg3T5AyE} During your journies you were ambushed by raiders you survived because...", null);
            }
            MBTextManager.SetTextVariable("EXP_VALUE", 10, false);
            CharacterCreationMenu characterCreationMenu = new CharacterCreationMenu(new TextObject("{=peNBA0WW}Story Background", null), backstory, new CharacterCreationOnInit(CSEscapePatch.EscapeOnInit), CharacterCreationMenu.MenuTypes.MultipleChoice);
           
            CharacterCreationCategory characterCreationCategory = characterCreationMenu.AddMenuCategory(null);
            
            
            characterCreationCategory.AddCategoryOption(new TextObject("{=6vCHovVH}you subdued a raider.", null), new List<SkillObject>
            {
                DefaultSkills.OneHanded,
                DefaultSkills.Athletics
            }, CharacterAttributesEnum.Vigor, 1, 10, 1, null, new CharacterCreationOnSelect(EscapeSubdueRaiderOnConsequence), new CharacterCreationApplyFinalEffects(EscapeSubdueRaiderOnApply), new TextObject("{=CvBoRaFv}You were able to grab a knife in the confusion of the attack. You stabbed a raider blocking your way.", null), null, 0, 0, 0);
            characterCreationCategory.AddCategoryOption(new TextObject("{=2XhW49TX}you drove them off with arrows.", null), new List<SkillObject>
            {
                DefaultSkills.Bow,
                DefaultSkills.Tactics
            }, CharacterAttributesEnum.Control, 1, 10, 1, null, new CharacterCreationOnSelect(EscapeDrawArrowsOnConsequence), new CharacterCreationApplyFinalEffects(EscapeDrawArrowsOnApply), new TextObject("{=ccf67J3J}You grabbed a bow and sent a few arrows the raiders' way. They took cover, giving you the opportunity to flee.", null), null, 0, 0, 0);
            characterCreationCategory.AddCategoryOption(new TextObject("{=gOI8lKcl}you rode off on a fast horse.", null), new List<SkillObject>
            {
                DefaultSkills.Riding,
                DefaultSkills.Scouting
            }, CharacterAttributesEnum.Endurance, 1, 10, 1, null, new CharacterCreationOnSelect(EscapeFastHorseOnConsequence), new CharacterCreationApplyFinalEffects(EscapeFastHorseOnApply), new TextObject("{=cepWNzEA}Jumping on the two remaining horses in the inn's burning stable, you broke out of the encircling raiders and rode off.", null), null, 0, 0, 0);
            characterCreationCategory.AddCategoryOption(new TextObject("{=EdUppdLZ}you tricked the raiders.", null), new List<SkillObject>
            {
                DefaultSkills.Roguery,
                DefaultSkills.Tactics
            }, CharacterAttributesEnum.Cunning, 1, 10, 1, null, new CharacterCreationOnSelect(EscapeRoadOffWithBrotherOnConsequence), new CharacterCreationApplyFinalEffects(EscapeRoadOffWithBrotherOnApply), new TextObject("{=ZqOvtLBM}In the confusion of the attack you shouted that someone had found treasure in the back room. You then made your way out of the undefended entrance.", null), null, 0, 0, 0);
            characterCreationCategory.AddCategoryOption(new TextObject("{=qhAhPWdp}you organized the travelers to break out.", null), new List<SkillObject>
            {
                DefaultSkills.Leadership,
                DefaultSkills.Charm
            }, CharacterAttributesEnum.Social, 1, 10, 1, null, new CharacterCreationOnSelect(EscapeOrganizeTravellersOnConsequence), new CharacterCreationApplyFinalEffects(EscapeOrganizeTravellersOnApply), new TextObject("{=Lmfi0cYk}You encouraged the few travellers in the inn to break out in a coordinated fashion. Raiders killed or captured most but you were able to escape.", null), null, 0, 0, 0);
            characterCreation.AddNewMenu(characterCreationMenu);            
            
            return false;
        }
        
        private static void EscapeOnInit(CharacterCreation characterCreation) //Different from Base
        {
            //Use these to get private fields
            FaceGenChar mother = (FaceGenChar) CharacterCreationContent.Instance.GetType().GetField("_mother",BindingFlags.NonPublic | BindingFlags.Instance).GetValue(CharacterCreationContent.Instance);
            FaceGenChar father = (FaceGenChar)CharacterCreationContent.Instance.GetType().GetField("_father", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(CharacterCreationContent.Instance);

            characterCreation.CurrentStage = CharacterCreation.Stages.EscapeMenu;
            characterCreation.ClearFaceGenPrefab();
            ClearCharacters(characterCreation);
            ClearMountEntity(characterCreation);
            Hero @object = MBObjectManager.Instance.GetObject<Hero>("tutorial_npc_brother");
            List<FaceGenChar> list = new List<FaceGenChar>();
            BodyProperties bodyProperties = CharacterObject.PlayerCharacter.GetBodyProperties(CharacterObject.PlayerCharacter.Equipment, -1);
            bodyProperties = FaceGen.GetBodyPropertiesWithAge(ref bodyProperties, 23f);
            BodyProperties randomBodyProperties = BodyProperties.GetRandomBodyProperties(CharacterObject.PlayerCharacter.IsFemale, mother.BodyProperties, father.BodyProperties, 1, Hero.MainHero.Mother.CharacterObject.GetDefaultFaceSeed(1), Hero.MainHero.Father.CharacterObject.HairTags, Hero.MainHero.Father.CharacterObject.BeardTags, Hero.MainHero.Father.CharacterObject.TattooTags);
            randomBodyProperties = new BodyProperties(new DynamicBodyProperties(randomBodyProperties.Age, 0.5f, 0.5f), randomBodyProperties.StaticProperties);
            @object.ModifyPlayersFamilyAppearance(randomBodyProperties.StaticProperties);
            @object.DynamicBodyProperties = randomBodyProperties.DynamicProperties;
            list.Add(new FaceGenChar(bodyProperties, new Equipment(), CharacterObject.PlayerCharacter.IsFemale, "act_childhood_schooled"));
            //
            if (CSCharCreationOption.CSWithFamily) {
                
                list.Add(new FaceGenChar(@object.BodyProperties, new Equipment(), @object.CharacterObject.IsFemale, "act_brotherhood_schooled"));
                characterCreation.ChangeFaceGenChars(list);
                characterCreation.ChangeCharsAnimation(new List<string>
            {
                "act_childhood_schooled",
                "act_brotherhood_schooled"
            });
            }
            else
            {
                characterCreation.ChangeFaceGenChars(list);
                characterCreation.ChangeCharsAnimation(new List<string>
            {
                "act_childhood_schooled",
                });
            }
            
            //CharacterCreationContent.Instance.GetType().GetMethod("ChangeStoryStageEquipments", BindingFlags.NonPublic | BindingFlags.Static).Invoke(CharacterCreationContent.Instance, new object[] { characterCreation }); **Cant invoke due to Hardcoded change brother
            ChangeStoryStageEquipments(characterCreation);
            List<FaceGenMount> list2 = new List<FaceGenMount>();

            if (CharacterObject.PlayerCharacter.HasMount())
            {
                ItemObject item = CharacterObject.PlayerCharacter.Equipment[EquipmentIndex.ArmorItemEndSlot].Item;
                list2.Add(new FaceGenMount(MountCreationKey.FromString(MountCreationKey.GetRandomMountKey(item, 2)), CharacterObject.PlayerCharacter.Equipment[EquipmentIndex.ArmorItemEndSlot].Item, CharacterObject.PlayerCharacter.Equipment[EquipmentIndex.HorseHarness].Item, "act_inventory_idle_start"));
            }
            if (@object.CharacterObject.HasMount())
            {
                ItemObject item2 = @object.CharacterObject.Equipment[EquipmentIndex.ArmorItemEndSlot].Item;
                list2.Add(new FaceGenMount(MountCreationKey.FromString(MountCreationKey.GetRandomMountKey(item2, 2)), @object.CharacterObject.Equipment[EquipmentIndex.ArmorItemEndSlot].Item, @object.CharacterObject.Equipment[EquipmentIndex.HorseHarness].Item, "act_inventory_idle_start"));
            }
        }
        // ***Edited from base game***
        private static void ChangeStoryStageEquipments(CharacterCreation characterCreation)
        {
            if (CSCharCreationOption.CSWithFamily)
            {
                characterCreation.ChangeCharactersEquipment(new List<Equipment>
            {
                CharacterObject.PlayerCharacter.Equipment,
                MBObjectManager.Instance.GetObject<Hero>("tutorial_npc_brother").CharacterObject.Equipment
            });
            }
            else
            {
                characterCreation.ChangeCharactersEquipment(new List<Equipment>
            {
                CharacterObject.PlayerCharacter.Equipment });
            }
        }
        //Lazy just adding all the methods for default patch to make compiler happy
        public static BodyProperties GetRandomBodyProperties(bool isFemale, BodyProperties bodyPropertiesMin, BodyProperties bodyPropertiesMax, int hairCoverType, int seed, string hairTags, string beardTags, string tattooTags)
        {
            return FaceGen.GetRandomBodyProperties(isFemale, bodyPropertiesMin, bodyPropertiesMax, hairCoverType, seed, hairTags, beardTags, tattooTags);
        }
        private static void EscapeSubdueRaiderOnConsequence(CharacterCreation characterCreation)
        {
            List<string> Anims = new List<string>();            
            if (CSCharCreationOption.CSWithFamily)
            {
                Anims.Add("act_childhood_gracious");
                Anims.Add("act_brotherhood_ready");                
            }
            else
            {
                Anims.Add("act_childhood_gracious");
            }
            characterCreation.ChangeCharsAnimation(Anims);
        }
        private static void ClearMountEntity(CharacterCreation characterCreation)
        {
            characterCreation.ClearFaceGenMounts();
        }
        private static void ClearCharacters(CharacterCreation characterCreation)
        {
            characterCreation.ClearFaceGenChars();
        }
        // Token: 0x0600023D RID: 573 RVA: 0x0000D174 File Offset: 0x0000B374
        private static void EscapeDrawArrowsOnConsequence(CharacterCreation characterCreation)
        {
            List<string> Anims = new List<string>();
            if (CSCharCreationOption.CSWithFamily)
            {
                Anims.Add("act_childhood_ready");
                Anims.Add("act_brotherhood_fierce");
            }
            else
            {
                Anims.Add("act_childhood_ready");
            }
            characterCreation.ChangeCharsAnimation(Anims);
            
        }

        // Token: 0x0600023E RID: 574 RVA: 0x0000D197 File Offset: 0x0000B397
        private static void EscapeFastHorseOnConsequence(CharacterCreation characterCreation)
        {
            
            List<string> Anims = new List<string>();
            if (CSCharCreationOption.CSWithFamily)
            {
                Anims.Add("act_childhood_fox");
                Anims.Add("act_brotherhood_gracious");
            }
            else
            {
                Anims.Add("act_childhood_fox");
            }
            characterCreation.ChangeCharsAnimation(Anims);
        }

        // Token: 0x0600023F RID: 575 RVA: 0x0000D1BA File Offset: 0x0000B3BA
        private static void EscapeRoadOffWithBrotherOnConsequence(CharacterCreation characterCreation)
        {
            
            List<string> Anims = new List<string>();
            if (CSCharCreationOption.CSWithFamily)
            {
                Anims.Add("act_childhood_tough");
                Anims.Add("act_brotherhood_clever");
            }
            else
            {
                Anims.Add("act_childhood_tough");
            }
            characterCreation.ChangeCharsAnimation(Anims);
        }

        // Token: 0x06000240 RID: 576 RVA: 0x0000D1DD File Offset: 0x0000B3DD
        private static void EscapeOrganizeTravellersOnConsequence(CharacterCreation characterCreation)
        {
            
            List<string> Anims = new List<string>();
            if (CSCharCreationOption.CSWithFamily)
            {
                Anims.Add("act_childhood_schooled");
                Anims.Add("act_brotherhood_manners");
            }
            else
            {
                Anims.Add("act_childhood_schooled");
            }
            characterCreation.ChangeCharsAnimation(Anims);
        }

        // Token: 0x06000241 RID: 577 RVA: 0x0000D200 File Offset: 0x0000B400
        private static void EscapeSubdueRaiderOnApply(CharacterCreation characterCreation)
        {
        }

        // Token: 0x06000242 RID: 578 RVA: 0x0000D202 File Offset: 0x0000B402
        private static void EscapeDrawArrowsOnApply(CharacterCreation characterCreation)
        {
        }

        // Token: 0x06000243 RID: 579 RVA: 0x0000D204 File Offset: 0x0000B404
        private static void EscapeFastHorseOnApply(CharacterCreation characterCreation)
        {
        }

        // Token: 0x06000244 RID: 580 RVA: 0x0000D206 File Offset: 0x0000B406
        private static void EscapeRoadOffWithBrotherOnApply(CharacterCreation characterCreation)
        {
        }

        // Token: 0x06000245 RID: 581 RVA: 0x0000D208 File Offset: 0x0000B408
        private static void EscapeOrganizeTravellersOnApply(CharacterCreation characterCreation)
        {
        }
    }
}
