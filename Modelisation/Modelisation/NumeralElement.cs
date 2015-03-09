using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public abstract class NumeralElement : Element
    {
        public double Min { get; protected set; }

        public double Max { get; protected set; }

        public double Step;

        public void setValue(double value)
        {
            if (value >= Min && value <= Max)
            {
                bool auth = notifyGameManager();
                if (auth)
                {
                    ObjectProperty = value;
                }
                
            }
        }
        public override void setOn()
        {
            bool auth = notifyGameManager();
            if (auth)
            {
                double newState = ObjectProperty + Step;
                if (newState <= Max)
                {
                    ObjectProperty = newState;
                }
                else
                {
                    ObjectProperty = Max;
                }
            }

        }
        public override void setOff()
        {
            bool auth = notifyGameManager();
            if (auth)
            {
                double newState = ObjectProperty - Step;
                if (newState >= Min)
                {
                    ObjectProperty = newState;
                }
                else
                {
                    ObjectProperty = Min;
                }
            }
        }
        
    }
}
