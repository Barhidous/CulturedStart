using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.Library;

namespace zCulturedStart
{
    class CSSkillScreen : ScreenBase
    {
        
        //private MyExampleVM _dataSource;
        private GauntletLayer _gauntletLayer;
        private GauntletMovie _movie;

        public CSSkillScreen(CSScreenData screenData)
        {
            this._screenData = screenData;
        }
        protected override void OnInitialize()
        {
            base.OnInitialize();
            this._datasource = new CSSkillScreenViewModel(this._screenData);
            _gauntletLayer = new GauntletLayer(100)
            {
                IsFocusLayer = true
            };
            AddLayer(_gauntletLayer);
            _gauntletLayer.InputRestrictions.SetInputRestrictions();
            _movie = _gauntletLayer.LoadMovie("CSSkillSelection", this._datasource);
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            ScreenManager.TrySetFocus(_gauntletLayer);
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
            _gauntletLayer.IsFocusLayer = false;
            ScreenManager.TryLoseFocus(_gauntletLayer);
        }

        protected override void OnFinalize()
        {
            base.OnFinalize();
            RemoveLayer(_gauntletLayer);
            //_dataSource = null;
            _gauntletLayer = null;
        }

        private CSScreenData _screenData;

        private CSSkillScreenViewModel _datasource;
    }

}

