using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace Modelisation
{
	/// <summary>
	/// Handle light with variable intensity. In Unity Min and Max are equal to 0 and 8.
	/// </summary>
    public class VariableLight : NumeralElement
    {
        /// <summary>
		/// Default constructor; it purpose is to set Min and Max values of the element.
		/// </summary>
        public VariableLight()
        {
            Max = 8;
            Min = 0;
			//ObjectProperty = GetComponent<Light> ().intensity;
        }
		
		public override void setObjectProperty(object val) {
			float fval = ((float) val);
			if (fval > Max) {
				GetComponent<Light> ().intensity = Max;
			}
			else if (fval < Min) {
				GetComponent<Light> ().intensity = Min;
			}
			else {
				GetComponent<Light> ().intensity = fval;
			}
		}

		public override object getObjectProperty(){
			return GetComponent<Light> ().intensity;
		}
		/*void Start () { 
			Step = 0.5f;
		}
		void Update() {
			setOn ();
		}*/
        
    }
}
