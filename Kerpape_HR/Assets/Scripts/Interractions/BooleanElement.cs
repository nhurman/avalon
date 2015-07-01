using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace Modelisation
{
	/// <summary>
	/// Class for on/off objects : activate or desactivate the target item.
	/// </summary>
    public class BooleanElement : Element
    {
		//private AffichageSymbolique affichageSymbolique;
		
		/// <summary>
		/// Name of the component that can be activated and desactivated.
		/// </summary>
		public Behaviour toDesactivate;

		public override void autonomous_setOn()
        {
            toDesactivate.enabled = true;
				//gameObject.GetComponent (toDesactivate).gameObject.SetActive (true);
        }

		public override void autonomous_setOff()
        {
            toDesactivate.enabled = false;
				//gameObject.GetComponent(toDesactivate).enabled = false;
				//gameObject.GetComponent (toDesactivate).gameObject.SetActive (false);
				//gameObject.SetActive(false);
        }

		public override void symbolic_setOff (){
			//affichageSymbolique.activer ();
		}

		public override void symbolic_setOn (){
			//affichageSymbolique.activer ();
		}

		public override void assisted_setOff (){
		}
		public override void assisted_setOn (){
		}

        public override bool isOn()
        {
           // return gameObject.GetComponent(toDesactivate).gameObject.activeSelf;
            //return gameObject.activeSelf;
            return toDesactivate.enabled;
        }

        public override bool isOff()
        {
            return !isOn();
        }
    }
}
