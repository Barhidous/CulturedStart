using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Library;
using Helpers;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;
using TaleWorlds.Localization;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.EncyclopediaItems;

namespace zCulturedStart
{
    class CSSkillScreenVM : ViewModel
    {
        public int OrgUnspentAttributePoints { get; private set; }
        public int OrgUnspentFocusPoints { get; private set; }
        public CSSkillScreenVM(CSScreenData screenData)
        {
            _screenData = screenData;
            this.Skills = new MBBindingList<CSSkillVM>();
            this.hero = Hero.MainHero; //*Companion
            this.UnspentCharacterPoints = screenData._focus;
            this.UnspentAttributePoints = screenData._attributes;
            this.OrgUnspentAttributePoints = this.UnspentAttributePoints;
            this.OrgUnspentFocusPoints = this.UnspentCharacterPoints;
            
            

        }
        public override void RefreshValues()
        {
            base.RefreshValues();
            StringHelpers.SetCharacterProperties("HERO", this.hero.CharacterObject, null, null);
            this.HeroNameText = this.hero.CharacterObject.Name.ToString();
            MBTextManager.SetTextVariable("LEVEL", this.hero.CharacterObject.Level, false);
            this.HeroLevelText = GameTexts.FindText("str_level", null).ToString();
            this.HeroInfoText = GameTexts.FindText("str_hero_name_level", null).ToString();
            this.FocusPointsText = GameTexts.FindText("str_focus_points", null).ToString();
            this.InitializeCharacter();
            this.Skills.ApplyActionOnAllItems(delegate (CSSkillVM x)
            {
                x.RefreshValues();
            });
            this.CurrentSkill.RefreshValues();
        }
        private void InitializeCharacter()
        {
            this.HeroCharacter = new HeroViewModel(CharacterViewModel.StanceTypes.None);
            this.Skills = new MBBindingList<CSSkillVM>();
            this.Traits = new MBBindingList<EncyclopediaTraitItemVM>();
            //this.Attributes.Clear();
            this.HeroCharacter.FillFrom(this.hero, -1);
            this.HeroCharacter.SetEquipment(EquipmentIndex.ArmorItemEndSlot, default(EquipmentElement));
            this.HeroCharacter.SetEquipment(EquipmentIndex.HorseHarness, default(EquipmentElement));
            this.HeroCharacter.SetEquipment(EquipmentIndex.NumAllWeaponSlots, default(EquipmentElement));
            foreach (SkillObject skill in (from s in SkillObject.All
                                           group s by s.CharacterAttribute.Id).SelectMany((IGrouping<MBGUID, SkillObject> s) => s).ToList<SkillObject>())
            {
                this.Skills.Add(new CSSkillVM(skill, this, _screenData));
            }
            for (CharacterAttributesEnum characterAttributesEnum = CharacterAttributesEnum.Vigor; characterAttributesEnum < CharacterAttributesEnum.NumCharacterAttributes; characterAttributesEnum++)
            {
                //CharacterAttributeItemVM item = new CharacterAttributeItemVM(this.Hero, characterAttributesEnum, this, new Action<CharacterAttributeItemVM>(this.OnInspectAttribute), new Action<CharacterAttributeItemVM>(this.OnAddAttributePoint));
                //this.Attributes.Add(item);
            }


        }
        public HeroDeveloper GetCharacterDeveloper()
        {
            Hero hero = this.hero;
            if (hero == null)
            {
                return null;
            }
            return hero.HeroDeveloper;
        }

        protected void OnSave()
        {
            this._affirmativeAction();
        }

        protected void OnPrevious()
        {
            this._negativeAction();
        }
        
        public void SetCurrentSkill(CSSkillVM skill)
        {
            this.CurrentSkill = skill;
        }

        public int GetRequiredFocusPointsToAddFocusWithCurrentFocus(SkillObject skill)
        {
            return this.hero.HeroDeveloper.GetRequiredFocusPointsToAddFocus(skill);
        }

        private CSScreenData _screenData;

        protected readonly Action _affirmativeAction;

        protected readonly Action _negativeAction;

        [DataSourceProperty]
        public MBBindingList<EncyclopediaTraitItemVM> Traits
        {
            get
            {
                return this._traits;
            }
            set
            {
                if (value != this._traits)
                {
                    this._traits = value;
                    base.OnPropertyChanged("Traits");
                }
            }
        }
        private MBBindingList<EncyclopediaTraitItemVM> _traits;
        [DataSourceProperty]
        public HeroViewModel HeroCharacter
        {
            get
            {
                return this._heroCharacter;
            }
            set
            {
                if (value != this._heroCharacter)
                {
                    this._heroCharacter = value;
                    base.OnPropertyChanged("HeroCharacter");
                }
            }
        }
        private HeroViewModel _heroCharacter;

        [DataSourceProperty]
        public string HeroInfoText
        {
            get
            {
                return this._heroInfoText;
            }
            set
            {
                if (value != this._heroInfoText)
                {
                    this._heroInfoText = value;
                    base.OnPropertyChanged("HeroInfoText");
                }
            }
        }
        private string _heroInfoText;
        [DataSourceProperty]
        public string FocusPointsText
        {
            get
            {
                return this._focusPointsText;
            }
            set
            {
                if (value != this._focusPointsText)
                {
                    this._focusPointsText = value;
                    base.OnPropertyChanged("FocusPointsText");
                }
            }
        }
        private string _focusPointsText;
        [DataSourceProperty]
        public string HeroLevelText
        {
            get
            {
                return this._heroLevelText;
            }
            set
            {
                if (value != this._heroLevelText)
                {
                    this._heroLevelText = value;
                    base.OnPropertyChanged("HeroLevelText");
                }
            }
        }
        private string _heroLevelText;
        [DataSourceProperty]
        public MBBindingList<CSSkillVM> Skills
        {
            get
            {
                return this._skills;
            }
            set
            {
                if (value != this._skills)
                {
                    this._skills = value;
                    base.OnPropertyChanged("Skills");
                }
            }
        }
        [DataSourceProperty]
        public CSSkillVM CurrentSkill
        {
            get
            {
                return this._currentSkill;
            }
            set
            {
                if (value != this._currentSkill)
                {
                    this._currentSkill = value;
                    base.OnPropertyChanged("CurrentSkill");
                }
            }
        }
        [DataSourceProperty]
        public int UnspentCharacterPoints //This is focus
        {
            get
            {
                return this._unspentCharacterPoints;
            }
            set
            {
                if (value != this._unspentCharacterPoints)
                {
                    this._unspentCharacterPoints = value;
                    base.OnPropertyChanged("UnspentCharacterPoints");
                }
            }
        }
        [DataSourceProperty]
        public string HeroNameText
        {
            get
            {
                return this._heroNameText;
            }
            set
            {
                if (value != this._heroNameText)
                {
                    this._heroNameText = value;
                    base.OnPropertyChanged("HeroNameText");
                }
            }
        }
        private string _heroNameText;

        [DataSourceProperty]
        public int UnspentAttributePoints
        {
            get
            {
                return this._unspentAttributePoints;
            }
            set
            {
                if (value != this._unspentAttributePoints)
                {
                    this._unspentAttributePoints = value;
                    base.OnPropertyChanged("UnspentAttributePoints");
                }
            }
        }
        private int _unspentCharacterPoints;
        private int _unspentAttributePoints;
        private CSSkillVM _currentSkill;

        private MBBindingList<CSSkillVM> _skills;
        public Hero hero { get; private set; }
    }
}
