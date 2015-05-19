using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Modelisation
{
	/// <summary>
	/// Class for numeral element : objects where the value to modify has a range (variable).
	/// </summary>
    public abstract class NumeralElement : Element
    {

		private AffichageSymbolique affichageSymbolique;
		//these should be object
        public float Min { get; protected set; }

        public float Max { get; protected set; }

        public float Step;

		/// <summary>
		/// Get the target object property.
		/// </summary>
		/// <returns>The target object. It should be casted upon return.</returns>
		public abstract object getObjectProperty();

		/// <summary>
		/// Set the object property to the value of val.
		/// </summary>
		/// <param name="val">New value of the object.</param>
		public abstract void setObjectProperty(object val);

		/// <summary>
		/// Set the value of the targeted object property to the value of the parameter. It ask the GameManager authorisation.
		/// </summary>
		/// <param name="value">New value for the object</param>
        public void setValue(double value)
        {
            if (value >= Min && value <= Max)
            {
                bool auth = notifyGameManager();
                if (auth)
                {
                    setObjectProperty(value);
                }
                
            }
        }

		public override void autonomous_setOn()
        {
				float newState = ((float) getObjectProperty()) + Step;
                if (newState <= Max)
                {
					setObjectProperty(newState);
                }
                else
                {
					setObjectProperty(Max);
                }

        }
		public override void autonomous_setOff()
        {
				float newState = ((float) getObjectProperty()) - Step;
                if (newState >= Min)
                {
					setObjectProperty(newState);
                }
                else
                {
					setObjectProperty(Min);
                }
          
        }
		public override void symbolic_setOff (){
			affichageSymbolique.activer ();
		}
		
		public override void symbolic_setOn (){
			affichageSymbolique.activer ();
		}
		public override void assisted_setOff (){
		}
		public override void assisted_setOn (){
		}

        public override bool isOn()
        {
			return ((float) getObjectProperty()) == Max;
        }
        public override bool isOff()
        {
			return ((float) getObjectProperty()) == Min;
        }

		private void Start() {
			affichageSymbolique = gameObject.AddComponent<AffichageSymbolique> ();
		}
		private void Update() {
		
		}
    }
}
