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
using TaleWorlds.CampaignSystem.ViewModelCollection;

namespace zCulturedStart
{
    class CSSkillVM : ViewModel
    {
        public CSSkillVM(SkillObject skill, CSSkillScreenVM SkillScreenVM,  CSScreenData screendata)//Action<PerkVM> onStartPerkSelection,
        {
            this.Skill = skill;
            this._skill = skill;
            this._SkillScreenVM = SkillScreenVM;
            this._screendata = screendata;
            this.SkillId = skill.StringId;
            this.Type = (skill.IsPartySkill ? CSSkillVM.SkillType.Party : (skill.IsLeaderSkill ? CSSkillVM.SkillType.Leader : CSSkillVM.SkillType.Default)).ToString();
           
            this._boundAttributeName = CharacterAttributes.GetCharacterAttribute(this.Skill.CharacterAttributeEnum).Name;
            this.LearningRateTooltip = new BasicTooltipViewModel(() => CampaignUIHelper.GetLearningRateTooltip(this._boundAttributeCurrentValue, this.CurrentFocusLevel, this.Level, this._heroLevel, this._boundAttributeName));
            this.LearningLimitTooltip = new BasicTooltipViewModel(() => CampaignUIHelper.GetLearningLimitTooltip(this._boundAttributeCurrentValue, this.CurrentFocusLevel, this._boundAttributeName));
            this.InitializeValues();
            this._focusConceptObj = Concept.All.SingleOrDefault((Concept c) => c.StringId == "str_game_objects_skill_focus");
            this._skillConceptObj = Concept.All.SingleOrDefault((Concept c) => c.StringId == "str_game_objects_skills");
            this.RefreshValues();

        }

        public void RefreshWithCurrentValues()
        {
            float num = Campaign.Current.Models.CharacterDevelopmentModel.CalculateLearningRate(this._boundAttributeCurrentValue, this.CurrentFocusLevel, this.Level, this._heroLevel, this._boundAttributeName, null);
            GameTexts.SetVariable("COUNT", num.ToString("0.00"));
            this.CurrentLearningRateText = GameTexts.FindText("str_learning_rate_COUNT", null).ToString();
            this.CanLearnSkill = (num > 0f);
            this.LearningRate = num;
            this.FullLearningRateLevel = Campaign.Current.Models.CharacterDevelopmentModel.CalculateLearningLimit(this._boundAttributeCurrentValue, this.CurrentFocusLevel, this._boundAttributeName, null);
            int requiredFocusPointsToAddFocusWithCurrentFocus = this._SkillScreenVM.GetRequiredFocusPointsToAddFocusWithCurrentFocus(this._skill);
            GameTexts.SetVariable("COSTAMOUNT", requiredFocusPointsToAddFocusWithCurrentFocus);
            this.FocusCostText = requiredFocusPointsToAddFocusWithCurrentFocus.ToString();
            GameTexts.SetVariable("COUNT", requiredFocusPointsToAddFocusWithCurrentFocus);
            GameTexts.SetVariable("RIGHT", "");
            GameTexts.SetVariable("LEFT", GameTexts.FindText("str_cost_COUNT", null));
            MBTextManager.SetTextVariable("FOCUS_ICON", GameTexts.FindText("str_html_focus_icon", null), false);
            this.NextLevelCostText = GameTexts.FindText("str_sf_text_with_focus_icon", null).ToString();
            this.RefreshCanAddFocus();
        }

        public void InitializeValues()
        {
            if (this._SkillScreenVM.GetCharacterDeveloper() == null)
            {
                this.Level = 0;
            }
            else
            {
                this.Level = this._SkillScreenVM.GetCharacterDeveloper().Hero.GetSkillValue(this.Skill);
                this.NextLevel = this.Level + 1;
                //this._isInSamePartyAsPlayer = (this._SkillScreenVM.hero.PartyBelongedTo != null && this._SkillScreenVM.hero.PartyBelongedTo == MobileParty.MainParty);
                this._SkillScreenVM.GetCharacterDeveloper().GetPropertyValue(this.Skill);
                this.CurrentSkillXP = this._SkillScreenVM.GetCharacterDeveloper().GetSkillXpProgress(this.Skill);
                this.XpRequiredForNextLevel = Campaign.Current.Models.CharacterDevelopmentModel.GetXpRequiredForSkillLevel(this.Level + 1) - Campaign.Current.Models.CharacterDevelopmentModel.GetXpRequiredForSkillLevel(this.Level);
                this.ProgressPercentage = 100.0 * (double)this._currentSkillXP / (double)this.XpRequiredForNextLevel;
                GameTexts.SetVariable("CURRENT_XP", this.CurrentSkillXP.ToString());
                GameTexts.SetVariable("LEVEL_MAX_XP", this.XpRequiredForNextLevel.ToString());
                this.ProgressHint = new HintViewModel(GameTexts.FindText("str_current_xp_over_max", null).ToString(), null);
                this.ProgressText = GameTexts.FindText("str_current_xp_over_max", null).ToString();
                GameTexts.SetVariable("REQUIRED_XP_FOR_NEXT_LEVEL", this.XpRequiredForNextLevel - this.CurrentSkillXP);
                this.SkillXPHint = new HintViewModel(GameTexts.FindText("str_skill_xp_hint", null).ToString(), null);

                this._orgFocusAmount = this._SkillScreenVM.GetCharacterDeveloper().GetFocus(this.Skill);
                this.CurrentFocusLevel = this._orgFocusAmount;
                this.CreateLists();
            }
        }
        public void CreateLists()
        {
            this.SkillEffects.Clear();
            int skillValue = this._SkillScreenVM.GetCharacterDeveloper().Hero.GetSkillValue(this.Skill);
            foreach (SkillEffect effect in from x in DefaultSkillEffects.GetAllSkillEffects()
                                           where x.EffectedSkills.Contains(this.Skill)
                                           select x)
            {
                this.SkillEffects.Add(new BindingListStringItem(CampaignUIHelper.GetSkillEffectText(effect, skillValue)));
            }
        }
        private void ExecuteInspect()
        {
            _SkillScreenVM.SetCurrentSkill(this);
           
        }

        public void RefreshCanAddFocus()
        {
            bool playerHasEnoughPoints = this._SkillScreenVM.UnspentCharacterPoints >= this._SkillScreenVM.GetRequiredFocusPointsToAddFocusWithCurrentFocus(this._skill);
            bool isMaxedSkill = this._currentFocusLevel >= this._screendata._maxFocus;
            this.CanAddFocus = playerHasEnoughPoints && !isMaxedSkill;
        }

        private void ExecuteAddFocus()
        {
            if (this.CanAddFocus)
            {
                this._SkillScreenVM.UnspentCharacterPoints -= this._SkillScreenVM.GetRequiredFocusPointsToAddFocusWithCurrentFocus(this._skill);
                int num = this.CurrentFocusLevel;
                this.CurrentFocusLevel = num + 1;
                //this._SkillScreenVM.RefreshCharacterValues();
                this.RefreshWithCurrentValues();
                InformationManager.HideInformations();
                if (this.Level == 0)
                {
                    num = this.Level;
                    this.Level = num + 1;
                    num = this.NextLevel;
                    this.NextLevel = num + 1;
                }
                Game.Current.EventManager.TriggerEvent<FocusAddedByPlayerEvent>(new FocusAddedByPlayerEvent(this._SkillScreenVM.hero, this._skill));
            }
        }

        private void ExecuteShowFocusConcept()
        {
            if (this._focusConceptObj != null)
            {
                Campaign.Current.EncyclopediaManager.GoToLink(this._focusConceptObj.EncyclopediaLink);
            }
        }

        
        private void ExecuteShowSkillConcept()
        {
            if (this._focusConceptObj != null)
            {
                Campaign.Current.EncyclopediaManager.GoToLink(this._skillConceptObj.EncyclopediaLink);
            }
        }
        [DataSourceProperty]
        public MBBindingList<BindingListStringItem> SkillEffects
        {
            get
            {
                return this._skillEffects;
            }
            set
            {
                if (value != this._skillEffects)
                {
                    this._skillEffects = value;
                    base.OnPropertyChanged("SkillEffects");
                }
            }
        }
        [DataSourceProperty]
        public HintViewModel ProgressHint
        {
            get
            {
                return this._progressHint;
            }
            set
            {
                if (value != this._progressHint)
                {
                    this._progressHint = value;
                    base.OnPropertyChanged("ProgressHint");
                }
            }
        }
        private HintViewModel _progressHint;
        [DataSourceProperty]
        public HintViewModel SkillXPHint
        {
            get
            {
                return this._skillXPHint;
            }
            set
            {
                if (value != this._skillXPHint)
                {
                    this._skillXPHint = value;
                    base.OnPropertyChanged("SkillXPHint");
                }
            }
        }
        private HintViewModel _skillXPHint;
        [DataSourceProperty]
        public string ProgressText
        {
            get
            {
                return this._progressText;
            }
            set
            {
                if (value != this._progressText)
                {
                    this._progressText = value;
                    base.OnPropertyChanged("ProgressText");
                }
            }
        }
        private string _progressText;
        [DataSourceProperty]
        public int CurrentSkillXP
        {
            get
            {
                return this._currentSkillXP;
            }
            set
            {
                if (value != this._currentSkillXP)
                {
                    this._currentSkillXP = value;
                    base.OnPropertyChanged("CurrentSkillXP");
                }
            }
        }
        [DataSourceProperty]
        public double ProgressPercentage
        {
            get
            {
                return this._progressPercentage;
            }
            set
            {
                if (value != this._progressPercentage)
                {
                    this._progressPercentage = value;
                    base.OnPropertyChanged("ProgressPercentage");
                }
            }
        }
        [DataSourceProperty]
        public BasicTooltipViewModel LearningLimitTooltip
        {
            get
            {
                return this._learningLimitTooltip;
            }
            set
            {
                if (value != this._learningLimitTooltip)
                {
                    this._learningLimitTooltip = value;
                    base.OnPropertyChanged("LearningLimitTooltip");
                }
            }
        }
        [DataSourceProperty]
        public int XpRequiredForNextLevel
        {
            get
            {
                return this._xpRequiredForNextLevel;
            }
            set
            {
                if (value != this._xpRequiredForNextLevel)
                {
                    this._xpRequiredForNextLevel = value;
                    base.OnPropertyChanged("XpRequiredForNextLevel");
                }
            }
        }
        private int _xpRequiredForNextLevel;

        [DataSourceProperty]
        public BasicTooltipViewModel LearningRateTooltip
        {
            get
            {
                return this._learningRateTooltip;
            }
            set
            {
                if (value != this._learningRateTooltip)
                {
                    this._learningRateTooltip = value;
                    base.OnPropertyChanged("LearningRateTooltip");
                }
            }
        }
        private int _boundAttributeCurrentValue
        {
            get
            {
                return 1;
                //return this._SkillScreenVM.GetCurrentAttributePoint(this.Skill.CharacterAttributeEnum);
            }
        }
        private int _heroLevel
        {
            get
            {
                return this._SkillScreenVM.hero.CharacterObject.Level;
            }
        }

        [DataSourceProperty]
        public string NextLevelCostText
        {
            get
            {
                return this._nextLevelCostText;
            }
            set
            {
                if (value != this._nextLevelCostText)
                {
                    this._nextLevelCostText = value;
                    base.OnPropertyChanged("NextLevelCostText");
                }
            }
        }
        private string _nextLevelCostText;
        [DataSourceProperty]
        public string FocusCostText
        {
            get
            {
                return this._focusCostText;
            }
            set
            {
                if (value != this._focusCostText)
                {
                    this._focusCostText = value;
                    base.OnPropertyChanged("FocusCostText");
                }
            }
        }
        private string _focusCostText;

        [DataSourceProperty]
        public float LearningRate
        {
            get
            {
                return this._learningRate;
            }
            set
            {
                if (value != this._learningRate)
                {
                    this._learningRate = value;
                    base.OnPropertyChanged("LearningRate");
                }
            }
        }
        private float _learningRate;

        [DataSourceProperty]
        public int FullLearningRateLevel
        {
            get
            {
                return this._fullLearningRateLevel;
            }
            set
            {
                if (value != this._fullLearningRateLevel)
                {
                    this._fullLearningRateLevel = value;
                    base.OnPropertyChanged("FullLearningRateLevel");
                }
            }
        }
        private int _fullLearningRateLevel;

        [DataSourceProperty]
        public string CurrentLearningRateText
        {
            get
            {
                return this._currentLearningRateText;
            }
            set
            {
                if (value != this._currentLearningRateText)
                {
                    this._currentLearningRateText = value;
                    base.OnPropertyChanged("CurrentLearningRateText");
                }
            }
        }
        private string _currentLearningRateText;
        [DataSourceProperty]
        public int Level
        {
            get
            {
                return this._level;
            }
            set
            {
                if (value != this._level)
                {
                    this._level = value;
                    base.OnPropertyChanged("Level");
                }
            }
        }
        [DataSourceProperty]
        public bool CanLearnSkill
        {
            get
            {
                return this._canLearnSkill;
            }
            set
            {
                if (value != this._canLearnSkill)
                {
                    this._canLearnSkill = value;
                    base.OnPropertyChanged("CanLearnSkill");
                }
            }
        }

        private bool _canLearnSkill;
        [DataSourceProperty]
        public string NextLevelLearningRateText
        {
            get
            {
                return this._nextLevelLearningRateText;
            }
            set
            {
                if (value != this._nextLevelLearningRateText)
                {
                    this._nextLevelLearningRateText = value;
                    base.OnPropertyChanged("NextLevelLearningRateText");
                }
            }
        }
        private string _nextLevelLearningRateText;
        [DataSourceProperty]
        public int NextLevel
        {
            get
            {
                return this._nextLevel;
            }
            set
            {
                if (value != this._nextLevel)
                {
                    this._nextLevel = value;
                    base.OnPropertyChanged("NextLevel");
                }
            }
        }

        [DataSourceProperty]
        public int CurrentFocusLevel
        {
            get
            {
                return this._currentFocusLevel;
            }
            set
            {
                if (value != this._currentFocusLevel)
                {
                    this._currentFocusLevel = value;
                    base.OnPropertyChanged("CurrentFocusLevel");
                }
            }
        }
        [DataSourceProperty]
        public bool CanAddFocus
        {
            get
            {
                return this._canAddFocus;
            }
            set
            {
                if (value != this._canAddFocus)
                {
                    this._canAddFocus = value;
                    base.OnPropertyChanged("CanAddFocus");
                }
            }
        }
        [DataSourceProperty]
        public string SkillId
        {
            get
            {
                return this._skillId;
            }
            set
            {
                if (value != this._skillId)
                {
                    this._skillId = value;
                    base.OnPropertyChanged("SkillId");
                }
            }
        }
        [DataSourceProperty]
        public string Type
        {
            get
            {
                return this._type;
            }
            set
            {
                if (value != this._type)
                {
                    this._type = value;
                    base.OnPropertyChanged("Type");
                }
            }
        }

        [DataSourceProperty]
        public bool IsInspected
        {
            get
            {
                return this._isInspected;
            }
            set
            {
                if (value != this._isInspected)
                {
                    this._isInspected = value;
                    base.OnPropertyChanged("IsInspected");
                }
            }
        }
        private int _nextLevel;
        private bool _canAddFocus;
        private int _level;  //Level from selection      
        private int _currentFocusLevel; //Level from selection
        private bool _isInspected;
        private string _type;
        private string _skillId;
        public readonly SkillObject Skill;
        private SkillObject _skill;
        private CSSkillScreenVM _SkillScreenVM;
        private CSScreenData _screendata;
        private TextObject _boundAttributeName;
        private readonly Concept _focusConceptObj;
        private readonly Concept _skillConceptObj;
        private BasicTooltipViewModel _learningLimitTooltip;
        private BasicTooltipViewModel _learningRateTooltip;
        private double _progressPercentage;
        private int _currentSkillXP;
        private MBBindingList<BindingListStringItem> _skillEffects;
        private int _orgFocusAmount;

        private enum SkillType
        {
            // Token: 0x04000BCF RID: 3023
            Default,
            // Token: 0x04000BD0 RID: 3024
            Party,
            // Token: 0x04000BD1 RID: 3025
            Leader
        }
    }
}
