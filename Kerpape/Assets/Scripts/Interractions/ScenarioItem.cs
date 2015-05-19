using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
	/// <summary>
	/// Basic scenario item.
	/// </summary>
    public class ScenarioItem
    {
        public string elementName;
        public string actionExplanation;
        public bool authAll;
        public Mode? modeOverride;
		public ScenarioItem() 
		{
				
		}
        public ScenarioItem(string name, string description, bool auth = true, Mode? mode = null) 
        {
            elementName = name;
            actionExplanation = description;
            authAll = auth;
            modeOverride = mode;
        }


		/// <summary>
		/// Tell if the scenario has reached the end.
		/// </summary>
		/// <returns>Bool : True if this is the end</returns>
        public virtual bool isEnd()
        {
            return false;
        }

    }
}
