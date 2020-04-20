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
using TaleWorlds.Localization;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper;

namespace zCulturedStart
{
    class CSSkillScreenViewModel : ViewModel
    {
        public CSSkillScreenViewModel(CSScreenData screenData)
        {
            _screenData = screenData;
            this.Skills = new MBBindingList<SkillVM>();

            foreach (SkillObject skill in (from s in SkillObject.All
                                           group s by s.CharacterAttribute.Id).SelectMany((IGrouping<MBGUID, SkillObject> s) => s).ToList<SkillObject>())
            {
                //this.Skills.Add(new SkillVM(skill, this, new Action<PerkVM>(this.OnStartPerkSelection)));
            }

        }
        protected void OnSave()
        {
            this._affirmativeAction();
        }

        protected void OnPrevious()
        {
            this._negativeAction();
        }
        
        private CSScreenData _screenData;

        protected readonly Action _affirmativeAction;

        protected readonly Action _negativeAction;

        [DataSourceProperty]
        public MBBindingList<SkillVM> Skills
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

        private MBBindingList<SkillVM> _skills;
    }
}
