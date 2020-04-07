using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using StoryMode.CharacterCreationSystem;

namespace zCulturedStart
{
    public class CSCharCreationOption
    {
        // if true skip quest stages on load
        private static bool _SkipFolly;
        public static bool SkipFolly
        {
            get 
            { 
              return _SkipFolly; 
            }
            set
            {
                _SkipFolly = value;
            }
        }
        private static bool _CSWithFamily;
        public static bool CSWithFamily
        {
            get
            {
                return _CSWithFamily;
            }
            set
            {
                _CSWithFamily = value;
            }
        }

    }
}
