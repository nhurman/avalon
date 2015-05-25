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
		public string toDeactivate;

		public override void autonomous_setOn()
        {
			gameObject.GetComponent (toDeactivate).gameObject.SetActive (true);
        }

		public override void autonomous_setOff()
        {
			gameObject.GetComponent (toDeactivate).gameObject.SetActive (false);
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
            return gameObject.GetComponent(toDeactivate).gameObject.activeSelf;
        }

        public override bool isOff()
        {
            return !isOn();
        }

		private void Start() {
			//affichageSymbolique = gameObject.AddComponent<AffichageSymbolique> ();
		}

		private void Update() {
			
		}
    }
}
