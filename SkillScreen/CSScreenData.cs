using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zCulturedStart
{
    class CSScreenData
    {
        public CSScreenData(int skillpoints, int maxskill, int attributes, int maxattributes, int focus, int maxfocus)
        {
            this._skillpoints = skillpoints;
            this._maxskill = skillpoints;
            this._attributes = attributes;
            this._maxattributes = maxattributes;
            this._focus = focus;
            this._maxFocus = maxfocus;
        }

        public int _skillpoints;
        public int _maxskill;
        public int _attributes;
        public int _maxattributes;
        public int _focus;
        public int _maxFocus;
        

    }
}
