using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public class ScenarioItem
    {
        public string elementName;
        public string actionExplanation;
        public bool authAll;

        public virtual bool isEnd()
        {
            return false;
        }

    }
}
