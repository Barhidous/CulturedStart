using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zCulturedStart
{
    class CSScreenData
    {
        public CSScreenData(int skillpoints, int maxskill)
        {
            this._skillpoints = skillpoints;
            this._maxskill = skillpoints;
        }

        public int _skillpoints;
        public int _maxskill;
    }
}
