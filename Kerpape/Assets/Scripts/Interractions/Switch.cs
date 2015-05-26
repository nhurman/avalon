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
        public string targetName;
        public bool onButton;
        public bool offButton;
		GameObject Target;
		Element Elem;

	
		private void Start()
		{
			incState = true;
			
			Target = GameObject.Find (targetName);
			Elem = Target ? Target.GetComponent<Element>() : null;
		}
		private void Update() {
		
		}

        private bool incState;
        //Perhaps too heavy, value could be stored
		
		/// <summary>
		/// Gets the instance of the targeted component.
		/// </summary>
        /*public Element Elem
        {
            get
            {
                return Target.GetComponent<Element>();
            }
        }*/
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

		
		/*private void showSideView ()
		{
			switch(targetName)
			{
			case "volet1" :
				cameraAssisteeScript.lookAtShutters();
				break;
			case "Spotlight_cuisine" :
				cameraAssisteeScript.lookAtKitchenLight();
				break;
			}
		}*/

		/// <summary>
		/// Toggle action : call switchOn until isOn = true, then switchOff until isOff = true.
		/// </summary>
        public void toggle()
        {
            bool auth = notifyGameManager();
            if (auth)
            {
				if (Elem.isOn())
                {
                    switchOff();
                    incState = false;
                }
				else if (Elem.isOff())
                {
                    switchOn();
                    incState = true;
                }
                else if (incState)
                {
                    switchOn();
                }
                else
                {
                    switchOff();
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

		[ContextMenu("Debug")]
		private  void ResetName()
		{
			Debug.Log ("Menu");
		}
    }
}
