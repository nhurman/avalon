using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace Modelisation
{
    public abstract class Element : SceneObject
    {
        //public object ObjectProperty { get; set; }
        //Should be between 0..1
        public Element()
        {
            type = "Action";
        }
        public void setOn() {
			bool auth = notifyGameManager();
			if (auth) {
				switch(Manager.CurrentMode) {
				case Mode.Auto:
					autonomous_setOn();
					break;
				case Mode.Assisted:
					assisted_setOn();
					break;
				case Mode.Symbolic:
					symbolic_setOn();
					break;
				}
			}
		}

        public void setOff() {
			bool auth = notifyGameManager();
			if (auth) {
				switch(Manager.CurrentMode) {
				case Mode.Auto:
					autonomous_setOff();
					break;
				case Mode.Assisted:
					assisted_setOff();
					break;
				case Mode.Symbolic:
					symbolic_setOff();
					break;
				}
			}
		}
		public abstract void autonomous_setOff ();
		public abstract void autonomous_setOn ();
		public abstract void symbolic_setOff ();
		public abstract void symbolic_setOn ();
		public abstract void assisted_setOff ();
		public abstract void assisted_setOn ();

        public abstract bool isOn();
        public abstract bool isOff();
    }
}
