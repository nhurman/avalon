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
        public Mode? modeOverride;

        public ScenarioItem(string name, string description, bool auth = true, Mode? mode = null) 
        {
            elementName = name;
            actionExplanation = description;
            authAll = auth;
            modeOverride = mode;
        }

        public virtual bool isEnd()
        {
            return false;
        }

    }
}
