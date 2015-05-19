using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace Modelisation
{
	/// <summary>
	/// Basic activable object.
	/// </summary>
    public abstract class Element : SceneObject
    {
        //public object ObjectProperty { get; set; }
        //Should be between 0..1
        public Element()
        {
            type = "Action";
        }

		/// <summary>
		/// Activate current object depending on the mode, if it is allowed.
		/// </summary>
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

		/// <summary>
		/// Desactivate current object depending on the mode, if it is allowed.
		/// </summary>
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
		
		/// <summary>
		/// Method called on desactivation on autonomous mode.
		/// </summary>
		public abstract void autonomous_setOff ();
		
		/// <summary>
		/// Method called on activation on autonomous mode.
		/// </summary>
		public abstract void autonomous_setOn ();
		
		/// <summary>
		/// Method called on desactivation on symbolic mode.
		/// </summary>
		public abstract void symbolic_setOff ();
		
		/// <summary>
		/// Method called on activation on symbolic mode.
		/// </summary>
		public abstract void symbolic_setOn ();
		
		/// <summary>
		/// Method called on desactivation on assisted mode.
		/// </summary>
		public abstract void assisted_setOff ();
		/// <summary>
		/// Method called on activation on assisted mode.
		/// </summary>
		public abstract void assisted_setOn ();

		/// <summary>
		/// Poll if the object is activated
		/// </summary>
		/// <returns>True if the object is activated</returns>
        public abstract bool isOn();

		/// <summary>
		/// Poll if the object is desactivated.
		/// </summary>
		/// <returns>True if the object is desactivated</returns>
        public abstract bool isOff();
    }
}
