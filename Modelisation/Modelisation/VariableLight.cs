using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace Modelisation
{
    public class VariableLight : NumeralElement
    {
        // In Unity, light.intensity go from 0 to 8
        public VariableLight()
        {
            Max = 8;
            Min = 0;
			//ObjectProperty = GetComponent<Light> ().intensity;
        }

		public override void setObjectProperty(object val) {
			GetComponent<Light> ().intensity = ((float)val);
		}

		public override object getObjectProperty(){
			return GetComponent<Light> ().intensity;
		}
		void Start () { 
			Step = 0.5f;
		}
		void Update() {
			setOn ();
		}
        
    }
}
