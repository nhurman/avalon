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
        public override void setOn()
        {
			Debug.Log("RECEIVED");
            bool auth = notifyGameManager();
            if (auth)
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

        }
        public override void setOff()
        {
            bool auth = notifyGameManager();
            if (auth)
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
