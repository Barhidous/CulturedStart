using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Library;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;
using TaleWorlds.Localization;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper;

namespace zCulturedStart
{
    class CSSkillScreenVM : ViewModel
    {
        public CSSkillScreenVM(CSScreenData screenData)
        {
            _screenData = screenData;
            this.Skills = new MBBindingList<CSSkillVM>();

            foreach (SkillObject skill in (from s in SkillObject.All
                                           group s by s.CharacterAttribute.Id).SelectMany((IGrouping<MBGUID, SkillObject> s) => s).ToList<SkillObject>())
            {
                //this.Skills.Add(new CSSkillVM(skill, this, new Action<PerkVM>(this.OnStartPerkSelection)));
            }
            this.hero = Hero.MainHero;

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
        private int _unspentCharacterPoints;

        private CSSkillVM _currentSkill;

        private MBBindingList<CSSkillVM> _skills;
        public Hero hero { get; private set; }
    }
}
