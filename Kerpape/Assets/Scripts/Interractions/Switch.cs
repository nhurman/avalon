using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;


namespace Modelisation
{
	/// <summary>
	/// Class for switch (objects that activate others).
	/// </summary>
    public class Switch : SceneObject
    {
		/// <summary>
		/// Name of the target.
		/// </summary>
        public string targetName;

		/// <summary>
		/// Define if the swich can be used as an activation button.
		/// </summary>
        public bool onButton;

		/// <summary>
		/// Define if the swich can be used as an deactivation button.
		/// </summary>
        public bool offButton;

		/// <summary>
		/// Instance of the targeted gameObject.
		/// </summary>
		private GameObject Target;

		/// <summary>
		/// Instance of the targeted reaction script.
		/// </summary>
		private Element Elem;

		/// <summary>
		/// Boolean to know if the change was toward on state or off state.
		/// </summary>
		private bool incState;

		private void Start() {
			incState = true;
			
			Target = GameObject.Find (targetName);
			Elem = Target ? Target.GetComponent<Element>() : null;
		}

		private void Update() {
		
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
        public Switch()
        {
            type = "Switch";
        }
    
		/// <summary>
		/// Call setOn on target element if the GameManager allow it.
		/// </summary>
        public void switchOn()
        {
            bool auth = notifyGameManager();
			if (auth && Target != null)
            {
				Elem.setOn();
            } 
        }

		/// <summary>
		/// Call setOff on target element if the GameManager allow it.
		/// </summary>
        public void switchOff()
        {
            bool auth = notifyGameManager();
			if (auth && Target != null)
			{
				Elem.setOff();
			}
        }

		/// <summary>
		/// Toggle action : call switchOn until isOn = true, then switchOff until isOff = true.
		/// </summary>
        public void toggle()
        {
            bool auth = notifyGameManager();
			if (auth && Target != null)
            {
				if (Elem.isOn())
                {
					Elem.setOff();
                    incState = false;
                }
				else if (Elem.isOff())
                {
					Elem.setOn();
                    incState = true;
                }
                else if (incState)
                {
					Elem.setOn();
                }
                else
                {
					Elem.setOff();
                }
            }
        }


		/// <summary>
		/// This method is called by MiddleVR events. It calls switchOn, switchOff or toggle depending on editor options.
		/// </summary>
        public void VRAction()
        {
            if (onButton && offButton)
            {
                toggle();
            }
            else if (onButton)
            {
                switchOn();
            }
            else if (offButton)
            {
                switchOff();
            }
            else
            {
                Debug.Log("Undefined button behaviour");
            }
        }
    }
}
