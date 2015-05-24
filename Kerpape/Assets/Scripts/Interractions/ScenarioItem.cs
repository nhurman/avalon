using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Modelisation
{
	/// <summary>
	/// Basic scenario item.
	/// </summary>
    public class ScenarioItem
    {
		/// <summary>
		/// Name of the element
		/// </summary>
        public string elementName;
		/// <summary>
		/// Explanation of what the action is supposed to achieve.
		/// </summary>
        public string actionExplanation;
		/// <summary>
		/// Define if other actions should be allowed.
		/// </summary>
        public bool authAll;
		/// <summary>
		/// The action will be executed in a different mode than the current mode.
		/// </summary>
        public Mode? modeOverride;
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public ScenarioItem() 
		{
				
		}
		
		/// <summary>
		/// Allow creation of a custom scenario item, with a name, a description and diverse options.
		/// </summary>
		/// <param name="name">The name of the object in the unity scene.</param>
		/// <param name="descrition">Desciption of what the action is supposed to achieve.</param>
		/// <param name="name">Allow the execution of other actions.</param>
		/// <param name="name">Allow this action to be executed in a different mode than the current one.</param>
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


		/// <summary>
		/// Action that will be started when this become the current state.
		/// </summary>
		public virtual void startAction() {

		}

		/// <summary>
		/// Called when the next scenarioItem become current.
		/// </summary>
		public virtual void stopAction() {
			
		}


    }
}
