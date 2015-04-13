using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace Modelisation
{
    public class Switch : SceneObject
    {
        public string targetName;
        public bool onButton;
        public bool offButton;
        private bool incState;
        //Perhaps too heavy, value could be stored
        public GameObject Target {
            get
            {
                return GameObject.Find(targetName);
            }
        }
        public Element Elem
        {
            get
            {
                return (Element)Target.GetComponent(typeof(Element));
            }
        }

        public Switch()
        {
            type = "Switch";
        }
    
        public void switchOn()
        {
            bool auth = notifyGameManager();
            if (auth)
            {
                Elem.setOn();
            } 
        }

        public void switchOff()
        {
            bool auth = notifyGameManager();
            if (auth)
            {
                Elem.setOff();
            } 
        }

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
                Debug.Log("Undefined button comportement");
            }
        }
        private void Start()
        {
            incState = true;
        }
    }
}
