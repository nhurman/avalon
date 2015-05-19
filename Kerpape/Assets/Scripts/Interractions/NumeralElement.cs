using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Modelisation
{
    public abstract class NumeralElement : Element
    {
		//these should be object
        public float Min { get; protected set; }

        public float Max { get; protected set; }

        public float Step;

		public abstract object getObjectProperty();
		public abstract void setObjectProperty(object val);

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
		}
		public override void symbolic_setOn (){
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
    }
}
